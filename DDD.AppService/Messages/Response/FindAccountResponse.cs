using DDD.AppService.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService.Messages.Response
{
    public class FindAccountResponse : ResponseBase
    {
        public AccountView Account { get; set; }
    }
}
