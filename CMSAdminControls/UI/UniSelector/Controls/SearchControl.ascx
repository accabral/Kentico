<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_UniSelector_Controls_SearchControl" CodeFile="SearchControl.ascx.cs" %>
<asp:Panel CssClass="Filter" runat="server" ID="pnlSearch">
    <asp:DropDownList ID="drpCondition" runat="server" />
    <cms:CMSTextBox ID="txtSearch" runat="server" /><cms:LocalizedButton runat="server"
        ID="btnSelect" OnClick="btnSelect_Click" EnableViewState="false" ResourceString="general.search"
        CssClass="ContentButton" />
</asp:Panel>
