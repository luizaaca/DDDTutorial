using DDD.Model.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using DDD.Model.Classes.Entities;
using System.Data;
using System.Linq;

namespace DDD.Repository.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public void Add(Account account)
        {
            var query = "INSERT INTO BankAccount (AccountId, Balance, CustomerRef) VALUES (@AccountId, @Balance, @CustomerRef)";

            using (var db = ConnectionFactory.GetConnection())
            {
                using (var transaction = db.BeginTransaction())
                {
                    db.Execute(query, account);
                    UpdateTransactionsFor(db, account);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Account> FindAll()
        {
            var accounts = new List<Account>();
            var queryAll = "SELECT * FROM BankAccount A INNER JOIN Transactions T ON T.AccountId = A.AccountId ORDER BY A.AccountId";

            using (var db = ConnectionFactory.GetConnection())
            {
                var accountDictionary = new Dictionary<Guid, List<dynamic>>();

                var savedAccounts = db.Query<dynamic>(queryAll).ToList();

                foreach (var item in savedAccounts)
                {
                    List<dynamic> transactions;

                    var key = new Guid(item.AccountId);

                    if (!accountDictionary.TryGetValue(key, out transactions))
                    {
                        transactions = new List<dynamic>();
                        accountDictionary.Add(key, transactions);
                    }

                    transactions.Add(item);
                }

                foreach (var item in accountDictionary)
                {
                    var transactions = item.Value.Select(t => new Transaction(t.Deposit, t.Withdrawal, t.Reference, t.Date)).ToList();
                    var account = new Account(item.Key, item.Value[0].Balance, transactions, item.Value[0].CustomerRef);
                    accounts.Add(account);
                }
            }

            return accounts;
        }

        public Account FindBy(Guid accountId)
        {
            Account account;
            var query = @"SELECT * FROM BankAccount A INNER JOIN Transactions T ON T.AccountId = A.AccountId 
                            WHERE A.AccountId = @accountId";

            using (var db = ConnectionFactory.GetConnection())
            {
                var savedAccount = db.Query<dynamic>(query, accountId).ToList();

                var transactions = savedAccount.Select(t => new Transaction(t.Deposit, t.Withdrawal, t.Reference, t.Date)).ToList();

                account = new Account(accountId, savedAccount[0].Balance, transactions, savedAccount[0].CustomerRef);
            }

            return account;
        }

        public void Save(Account account)
        {
            var query = "UPDATE BankAccount SET Balance = @Balance, CustomerRef = @CustomerRef WHERE AccountId = @AccountId";

            using (var db = ConnectionFactory.GetConnection())
            {
                using (var transaction = db.BeginTransaction())
                {
                    db.Execute(query, account);
                    UpdateTransactionsFor(db, account);
                    transaction.Commit();
                }
            }
        }

        private void UpdateTransactionsFor(IDbConnection db, Account account)
        {
            var queryLastTransactionTime = "SELECT TOP 1 [Date] FROM Transactions WHERE AccountId = @AccountId ORDER BY [Date] DESC";
            var lastTransactionTime = db.QuerySingle<DateTime>(queryLastTransactionTime, account);
            var transactions = account.GetTransactions().Where(t => t.Date > lastTransactionTime).OrderBy(t => t.Date)
                .Select(t => new
                {
                    AccountId = account.AccountId,
                    Deposit = t.Deposit,
                    Withdrawal = t.Withdrawal,
                    Reference = t.Reference,
                    Date = t.Date
                }).ToList();

            var queryInsert = "insert into Transactions (AccountId, Deposit, Withdrawal, Reference, [Date]) VALUES (@AccountId, @Deposit, @Withdrawal, @Reference, @Date)";
            db.Execute(queryInsert, transactions);
        }
    }
}
