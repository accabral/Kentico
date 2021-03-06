<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewVersion.aspx.cs" Inherits="CMSModules_Content_CMSPages_Versions_ViewVersion"
    MasterPageFile="~/CMSMasterPages/LiveSite/Dialogs/ModalDialogPage.master" Theme="Default" %>

<%@ Register Src="~/CMSModules/Content/Controls/ViewVersion.ascx" TagName="ViewVersion"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ViewVersion ID="viewVersion" runat="server" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" OnClientClick="window.close(); return false;"
            ResourceString="General.Close" />
    </div>
</asp:Content>
