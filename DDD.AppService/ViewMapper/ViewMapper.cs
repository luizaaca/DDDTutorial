using System;
using System.Collections.Generic;
using System.Text;
using DDD.AppService.ViewModel;
using DDD.Model.Classes.Entities;

namespace DDD.AppService
{
    public static class ViewMapper
    {
        public static TransactionView CreateTransactionViewFrom(Transaction transaction)
        {
            return new TransactionView
            {
                Deposit = transaction.Deposit.ToString("C"),
                Withdrawal = transaction.Withdrawal.ToString("C"),
                Reference = transaction.Reference,
                Date = transaction.Date
            };
        }
        public static AccountView CreateAccountViewFrom(Account account)
        {
            return new AccountView
            {
                AccountNo = account.AccountId,
                Balance = account.Balance.ToString("C"),
                CustomerRef = account.CustomerRef,
                Transactions = new List<TransactionView>()
            };
        }
    }
}
