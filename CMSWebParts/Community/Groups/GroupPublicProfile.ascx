<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Community_Groups_GroupPublicProfile" CodeFile="~/CMSWebParts/Community/Groups/GroupPublicProfile.ascx.cs" %>
    
<asp:Label ID="lblError" CssClass="ErrorLabel" runat="server" Visible="false" EnableViewState="false" />
<asp:PlaceHolder ID="plcContent" runat="server">
    <cms:DataForm ID="formElem" runat="server" IsLiveSite="true" />
    <asp:Label ID="lblNoProfile" runat="Server" CssClass="NoProfile" Visible="false"
        EnableViewState="false" />
</asp:PlaceHolder>
