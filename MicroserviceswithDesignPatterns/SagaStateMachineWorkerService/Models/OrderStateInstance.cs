using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateInstance : SagaStateMachineInstance 
    {
        public Guid CorrelationId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } // This is the correlation ID of the saga instance
        public string CurrentState { get; set; }
        public string BuyerId { get; set; }
        public int OrderId { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVV { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public override string ToString() 
        {
            var properties = GetType().GetProperties(); // Get all properties of the class

            var sb = new StringBuilder();

            foreach (var property in properties)
            {
                sb.AppendLine($"{property.Name}: {property.GetValue(this)}"); // Append the property name and value to the string builder
            }
            sb.Append("-----------------");
            return sb.ToString();
        }
    }
}
