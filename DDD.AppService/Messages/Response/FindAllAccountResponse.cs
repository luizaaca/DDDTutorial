using DDD.AppService.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService.Messages.Response
{
    public class FindAllAccountResponse : ResponseBase
    {
        public IList<AccountView> AccountsViews { get; set; }
    }
}
