using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDD.AppService;
using DDD.AppService.Messages.Response;
using DDD.AppService.Messages.Request;

namespace DDD.UI.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ShowAllAccounts();
        }

        private void ShowAllAccounts()
        {
            ddlAccounts.Items.Clear();

            var response = new ApplicationAccountService().GetAllAccounts();
            ddlAccounts.Items.Add(new ListItem("Select an account", ""));

            foreach (var accView in response.AccountsViews)
            {
                ddlAccounts.Items.Add(new ListItem(accView.CustomerRef, accView.AccountNo.ToString()));
            }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            var request = new AccountCreateRequest();

            request.CustomerName = this.txtCustomerRef.Text;

            new ApplicationAccountService().CreateAccount(request);

            ShowAllAccounts();
        }

        protected void ddlAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedAccount();
        }

        private void DisplaySelectedAccount()
        {
            if (ddlAccounts.SelectedValue.ToString() != "")
            {
                var service = new ApplicationAccountService();
                var response = service.GetAccountBy(new Guid(ddlAccounts.SelectedValue.ToString()));
                var accountView = response.AccountView;

                lblAccountNo.Text = accountView.AccountNo.ToString();
                lblBalance.Text = accountView.Balance.ToString();
                lblCustomerRef.Text = accountView.CustomerRef;

                rptTransactions.DataSource = accountView.Transactions;
                rptTransactions.DataBind();

                var allAccountsResponse = service.GetAllAccounts();

                ddlAccountsToTransferTo.Items.Clear();

                foreach (var accView in allAccountsResponse.AccountsViews)
                {
                    if (!accView.AccountNo.Equals(accountView.AccountNo))
                        ddlAccountsToTransferTo.Items.Add(new ListItem(accView.CustomerRef, accView.AccountNo.ToString()));
                }
            }
        }

        protected void btnWithdrawal_Click(object sender, EventArgs e)
        {
            var request = new WithdrawalRequest();
            var accountNo = new Guid(ddlAccounts.SelectedValue);

            request.AccountNo = accountNo;
            request.Amount = decimal.Parse(txtAmount.Text);

            new ApplicationAccountService().Whitdrawal(request);
            DisplaySelectedAccount();
        }

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            var request = new DepositRequest();
            var accountNo = new Guid(ddlAccounts.SelectedValue);

            request.AccountNo = accountNo;
            request.Amount = decimal.Parse(txtAmount.Text);

            new ApplicationAccountService().Deposit(request);
            DisplaySelectedAccount();
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            var request = new TransferRequest();
            request.AccountFromNo = new Guid(ddlAccounts.SelectedValue);
            request.AccountToNo = new Guid(ddlAccountsToTransferTo.SelectedValue);
            request.Amount = decimal.Parse(txtAmountToTransfer.Text);

            var response = new ApplicationAccountService().Transfer(request);
            DisplaySelectedAccount();
        }
    }
}