using MassTransit;
using SharedSaga;
using SharedSaga.Events;
using SharedSaga.Interfaces;
using SharedSaga.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
    {
        public Event<IOrderCreatedRequestEvent> OrderCreatedRequestEvent { get; set; }
        public Event<IStockReservedEvent> StockReservedEvent { get; set; }
        public Event<IStockNotReservedEvent> StockNotReservedEvent { get; set; }
        public Event<IPaymentCompletedEvent> PaymentCompletedEvent { get; set; }
        public Event<IPaymentFailedEvent> PaymentFailedEvent { get; set; }
        public State OrderCreated { get; private set; }
        public State StockReserved { get; private set; }
        public State StockNotReserved { get; private set; }
        public State PaymentCompleted { get; private set; }
        public State PaymentFailed { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState); // This is the state property of the saga instance

            Event(() => OrderCreatedRequestEvent, x =>
            {
                x.CorrelateBy<int>(State => State.OrderId, context => context.Message.OrderId).SelectId(context => Guid.NewGuid()); // This is the correlation property of the saga instance, this is the property that will be used to match the incoming message with the saga instance
            });


            Event(() => StockReservedEvent, x =>
            {
                x.CorrelateById(y => y.Message.CorrelationId);
            }); // This is the correlation property of the saga instance, this is the property that will be used to match the incoming message with the saga instance

            Event(() => StockNotReservedEvent, x =>
            {
                x.CorrelateById(y => y.Message.CorrelationId);
            }); // This is the correlation property of the saga instance, this is the property that will be used to match the incoming message with the saga instance

            Event(() => PaymentCompletedEvent, x =>
            {
                x.CorrelateById(y => y.Message.CorrelationId);
            }); // This is the correlation property of the saga instance, this is the property that will be used to match the incoming message with the saga instance

            Initially(
                When(OrderCreatedRequestEvent) // bu kod ile OrderCreatedRequestEvent eventi geldiğinde yapılacak işlemleri belirtiyoruz
                    .Then(context =>
                    {
                        context.Instance.OrderId = context.Data.OrderId;
                        context.Instance.BuyerId = context.Data.BuyerId;
                        context.Instance.CreatedDate = DateTime.Now;
                        context.Instance.CardName = context.Data.Payment.CardName;
                        context.Instance.CardNumber = context.Data.Payment.CardNumber;
                        context.Instance.ExpiryMonth = context.Data.Payment.ExpiryMonth;
                        context.Instance.ExpiryYear = context.Data.Payment.ExpiryYear;
                        context.Instance.CVV = context.Data.Payment.CVV;
                        context.Instance.TotalPrice = context.Data.TotalPrice;
                    }).Then(context => { Console.WriteLine($"OrderCreatedRequestEvent before: {context.Instance}"); })
                    .Publish(context => new OrderCreatedEvent(context.Instance.CorrelationId) { OrderItemMessages = context.Data.OrderItems })
                    .TransitionTo(OrderCreated).Then(context => { Console.WriteLine($"OrderCreatedRequestEvent after: {context.Instance}"); })
            // bu kod ile OrderCreated state'ine geçiş yapılıyor
            );

            During(OrderCreated,
                When(StockReservedEvent) // bu kod ile StockReservedEvent eventi geldiğinde yapılacak işlemleri belirtiyoruz
                    .TransitionTo(StockReserved)
                    .Send(new Uri($"queue:{RabbitMQSettings.PaymentStockReservedRequestQueueName}"), context => new StockReservedRequestPayment(context.Instance.CorrelationId)
                    { Payment = new PaymentMessage { CardName = context.Instance.CardName, CardNumber = context.Instance.CardNumber, CVV = context.Instance.CVV, ExpiryMonth = context.Instance.ExpiryMonth, ExpiryYear = context.Instance.ExpiryYear, TotalPrice = context.Instance.TotalPrice } }
                    ).Then(context => { Console.WriteLine($"StockReservedEvent after: {context.Instance}"); }), When(StockNotReservedEvent).TransitionTo(StockNotReserved).Publish(context => new OrderRequestFailedEvent() { OrderId = context.Instance.OrderId,Reason = context.Data.Reason }).Then(context => { Console.WriteLine($"StockNotReservedEvent after: {context.Instance}"); }) // bu kod ile saga'nın sonlandırılacağını belirtiyoruz
            );

            During(StockReserved,
                When(PaymentCompletedEvent) // bu kod ile PaymentCompletedEvent eventi geldiğinde yapılacak işlemleri belirtiyoruz
                    .TransitionTo(PaymentCompleted)
                    .Publish(context => new OrderRequestCompletedEvent() { OrderId = context.Instance.OrderId })
                    .Then(context => { Console.WriteLine($"PaymentCompletedEvent after: {context.Instance}"); }).Finalize(),  // bu kod ile saga'nın sonlandırılacağını belirtiyoruz
                    When(PaymentFailedEvent).Send(new Uri($"queue:{RabbitMQSettings.StockRollbackMessageQueueName}"), context => new StockRollbackMessage() { OrderItemMessages = context.Data.OrderItemMessages }).Then(context => { Console.WriteLine($"PaymentFailedEvent after: {context.Instance}"); }).TransitionTo(PaymentFailed)
            );

            SetCompletedWhenFinalized(); // bu kod ile saga'nın sonlandırılacağını belirtiyoruz
        }
    }
}
