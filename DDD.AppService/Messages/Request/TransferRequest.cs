using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService.Messages.Request
{
    public class TransferRequest
    {
        public Guid AccountNoTo { get; set; }
        public Guid AccountNoFrom { get; set; }
        public decimal Amount { get; set; }
    }
}
