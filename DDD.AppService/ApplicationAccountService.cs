using DDD.AppService.Messages.Request;
using DDD.AppService.Messages.Response;
using DDD.AppService.ViewModel;
using DDD.Model.Classes.CustomExceptions;
using DDD.Model.Classes.Entities;
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

        public ApplicationAccountService() :
            this(new AccountRepository(), new AccountService(new AccountRepository()))
        { }

        public ApplicationAccountService(IAccountRepository accountRepository, AccountService accountService)
        {
            _accountRepository = accountRepository;
            _accountService = accountService;
        }

        public AccountCreateResponse CreateAccount(AccountCreateRequest request)
        {
            var response = new AccountCreateResponse();
            response.Success = true;

            try
            {
                var account = new Account
                {
                    CustomerRef = request.CustomerName
                };

                _accountRepository.Add(account);
            }
            catch
            {
                response.Success = false;
                response.Message = "Unexpected error";
            }

            return response;
        }

        public void Deposit(DepositRequest request)
        {
            var account = _accountRepository.FindBy(request.AccountNo);

            account.Deposit(request.Amount, string.Empty);

            _accountRepository.Save(account);
        }

        public void Whitdrawal(WithdrawalRequest request)
        {
            var account = _accountRepository.FindBy(request.AccountNo);

            account.Withdraw(request.Amount, string.Empty);

            _accountRepository.Save(account);
        }

        public TransferResponse Transfer(TransferRequest request)
        {
            var response = new TransferResponse();
            response.Success = true;

            try
            {
                _accountService.Transfer(request.AccountToNo, request.AccountFromNo, request.Amount);
            }
            catch (InsufficientFundsException)
            {
                response.Success = false;
                response.Message = $"Insufficient funds in account number {request.AccountFromNo}";
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = "Unexpected error";
            }

            return response;
        }

        public FindAllAccountResponse GetAllAccounts()
        {
            var response = new FindAllAccountResponse();
            response.Success = true;

            try
            {
                var accountViews = new List<AccountView>();

                response.AccountsViews = accountViews;

                foreach (var account in _accountRepository.FindAll())
                {
                    accountViews.Add(ViewMapper.CreateAccountViewFrom(account));
                }
            }
            catch
            {
                response.Message = "Unexpected error";
                response.Success = false;
            }

            return response;
        }

        public FindAccountResponse GetAccountBy(Guid accountNo)
        {
            var response = new FindAccountResponse();
            response.Success = true;

            try
            {
                var account = _accountRepository.FindBy(accountNo);
                var accountView = ViewMapper.CreateAccountViewFrom(account);

                foreach (var transaction in account.GetTransactions())
                {
                    accountView.Transactions.Add(ViewMapper.CreateTransactionViewFrom(transaction));
                }

                response.AccountView = accountView;
            }
            catch
            {
                response.Success = false;
                response.Message = "Unexpected error";
            }

            return response;
        }
    }
}
