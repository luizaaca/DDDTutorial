using DDD.Model.Classes.CustomExceptions;
using DDD.Model.Classes.Entities;
using DDD.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Model.Classes.Services
{
    public class AccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Transfer(Guid accountToNo, Guid accountFromNo, decimal amount)
        {
            Account to = _accountRepository.FindBy(accountToNo);
            Account from = _accountRepository.FindBy(accountFromNo);

            if (from.CanWithdraw(amount))
            {
                to.Deposit(amount, $"From {from.CustomerRef}");
                from.Withdraw(amount, $"Transfer to {to.CustomerRef}");
            }
            else
            {
                throw new InsufficientFundsException();
            }
        }
    }
}
