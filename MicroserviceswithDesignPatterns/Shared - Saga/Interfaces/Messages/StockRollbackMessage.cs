using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSaga.Interfaces.Messages
{
    public class StockRollbackMessage : IStockRollbackMessage
    {
        public List<OrderItemMessage> OrderItemMessages { get; set; }
    }
}
