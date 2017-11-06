using DDD.Model.Classes.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Model.Classes.Entities
{
    public class Account
    {
        private decimal _balance;
        private Guid _accountId;
        private string _customerRef;
        private IList<Transaction> _transactions;

        public Account() : this(Guid.NewGuid(), 0m, new List<Transaction>(), "")
        {
            _transactions.Add(new Transaction(0m, 0m, "account created", DateTime.Now));
        }

        public Account(Guid accountId, decimal balance, IList<Transaction> transactions, string customerRef)
        {
            _accountId = accountId;
            _balance = balance;
            _transactions = transactions;
            _customerRef = customerRef;
        }

        public Guid AccountId
        {
            get { return _accountId; }
            internal set { _accountId = value; }
        }

        public decimal Balance
        {
            get { return _balance; }
            internal set { _balance = value; }
        }

        public string CustomerRef
        {
            get { return _customerRef; }
            set { _customerRef = value; }
        }

        public bool CanWithdraw(decimal amount)
        {
            return (Balance >= amount);
        }

        public void Withdraw(decimal amount, string reference)
        {
            if (!CanWithdraw(amount))
                throw new InsufficientFundsException();

            Balance -= amount;
            _transactions.Add(new Transaction(0m, amount, reference, DateTime.Now));
        }

        public void Deposit(decimal amount, string reference)
        {
            Balance += amount;
            _transactions.Add(new Transaction(amount, 0m, reference, DateTime.Now));
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactions;
        }
    }
}
