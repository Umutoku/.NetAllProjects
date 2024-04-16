using SharedSaga.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSaga
{
    public class PaymentFailedEvent : IPaymentFailedEvent
    {
        public PaymentFailedEvent(Guid collerationId)
        {
             CorrelationId = collerationId;
        }

        public List<OrderItemMessage> OrderItemMessages { get; set; }
        public string Reason { get; set; }

        public Guid CorrelationId { get; }
    }
}
