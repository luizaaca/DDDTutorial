using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService.ViewModel
{
    public class AccountView
    {
        public Guid AccountNo { get; set; }
        public string Balance { get; set; }
        public string CustomerRef { get; set; }
        public IList<TransactionView> Transactions { get; set; }
    }
}
