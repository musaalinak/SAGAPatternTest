using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    public interface IOrderRecievedEvent
    {
        public Guid CorrelationId { get; set; }

        public int OrderId { get; set; }

        public string OrderCode { get; set; }
    }
}
