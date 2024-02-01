using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shared.Events
{
    public record OrderCreatedEvent
    {
        // Eğer header göndermeyen bir message broker kullanılıyor ise bu property kullanılabilir.
        //public Dictionary<string,string> Header { get; set; }

        public string OrderCode { get; set; } = null!;
    }
}
