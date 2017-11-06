using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService.Messages.Request
{
    public class TransferRequest
    {
        public Guid AccountToNo { get; set; }
        public Guid AccountFromNo { get; set; }
        public decimal Amount { get; set; }
    }
}
