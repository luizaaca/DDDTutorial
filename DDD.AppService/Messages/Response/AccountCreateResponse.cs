using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService.Messages.Response
{
    public class AccountCreateResponse: ResponseBase
    {
        public Guid AccountNo { get; set; }
    }
}
