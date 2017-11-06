using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService.Messages.Request
{
    public class WithdrawalRequest
    {
        public Guid AccountNo { get; set; }
        public decimal Amount { get; set; }
    }
}
