using DDD.Model.Classes.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Model.Classes.Entities
{
    public class Account
    {
        private decimal _balance;
        private Guid _accountNo;
        private string _customerRef;
        private IList<Transaction> _transactions;

        public Account() : this(Guid.NewGuid(), 0m, new List<Transaction>(), "")
        {
            _transactions.Add(new Transaction(0m, 0m, "account created", DateTime.Now));
        }

        public Account(Guid Id, decimal balance, IList<Transaction> transactions, string customerRef)
        {
            _accountNo = Id;
            _balance = balance;
            _transactions = transactions;
            _customerRef = customerRef;
        }

        public Guid AccountNo
        {
            get { return _accountNo; }
            internal set { _accountNo = value; }
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
