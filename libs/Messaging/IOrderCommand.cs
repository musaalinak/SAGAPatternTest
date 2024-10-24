using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    public interface IOrderCommand
    {
        public int OrderID { get; set; }
        public string OrderCode { get; set; }
    }
}
