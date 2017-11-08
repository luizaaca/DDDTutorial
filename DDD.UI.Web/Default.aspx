<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DDD.UI.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div>
            <fieldset>
                <legend>Create New Account</legend>
                <p>
                    Customer Ref:
                   <asp:TextBox ID="txtCustomerRef" runat="server" />
                    <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" OnClick="btnCreateAccount_Click" />
                </p>
            </fieldset>

            <fieldset>
                <legend>Account Detail</legend>
                <p>
                    <asp:DropDownList AutoPostBack="true" ID="ddlAccounts" runat="server" OnSelectedIndexChanged="ddlAccounts_SelectedIndexChanged" />
                </p>
                <p>
                    Account No:
                   <asp:Label ID="lblAccountNo" runat="server" />
                </p>
                <p>
                    Customer Ref:
                   <asp:Label ID="lblCustomerRef" runat="server" />
                </p>
                <p>
                    Balance:
                   <asp:Label ID="lblBalance" runat="server" />
                </p>
                <p>
                    Amount:
                   <asp:TextBox ID="txtAmount" runat="server" />
                    &nbsp;
                   <asp:Button ID="btnWithdrawal" runat="server" Text="Withdrawal" OnClick="btnWithdrawal_Click" />
                    &nbsp;
                   <asp:Button ID="btnDeposit" runat="server" Text="Deposit" OnClick="btnDeposit_Click" />
                </p>
                <p>
                    Transfer
                    <asp:TextBox ID="txtAmountToTransfer" runat="server" />
                    &nbsp;to
                    <asp:DropDownList AutoPostBack="true" ID="ddlAccountsToTransferTo" runat="server" />
                    &nbsp;
                    <asp:Button ID="btnTransfer" runat="server" Text="Commit" OnClick="btnTransfer_Click" />
                </p>
                <p>Transactions</p>
                <asp:Repeater ID="rptTransactions" runat="server">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>deposit</td>
                                <td>withdrawal</td>
                                <td>reference</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Deposit") %></td>
                            <td><%# Eval("Withdrawal") %></td>
                            <td><%# Eval("Reference") %></td>
                            <td><%# Eval("Date") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

            </fieldset>
        </div>


</asp:Content>
