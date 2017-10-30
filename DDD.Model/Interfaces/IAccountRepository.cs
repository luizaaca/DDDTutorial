using DDD.Model.Classes.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Model.Interfaces
{
    public interface IAccountRepository
    {
        void Add(Account account);
        void Save(Account account);
        IEnumerable<Account> FindAll();
        Account FindBy(Guid accountId);
    }
}
