using DDD.Model.Classes.Services;
using DDD.Model.Interfaces;
using DDD.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.AppService
{
    public class ApplicationAccountService
    {
        private AccountService _accountService;
        private IAccountRepository _accountRepository;

        public ApplicationAccountService():
            this(new AccountRepository(), new AccountService(new AccountRepository()))
        { }

        public ApplicationAccountService(IAccountRepository accountRepository, AccountService accountService)
        {
            _accountRepository = accountRepository;
            _accountService = accountService;
        }
    }
}
