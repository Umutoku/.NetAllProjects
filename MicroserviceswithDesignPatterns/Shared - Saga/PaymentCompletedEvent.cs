using SharedSaga.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSaga
{
    public class PaymentCompletedEvent : IPaymentCompletedEvent
    {
        public PaymentCompletedEvent(Guid collerationId)
        {
            CorrelationId = collerationId;
        }

        public Guid CorrelationId { get; }
    }
}
