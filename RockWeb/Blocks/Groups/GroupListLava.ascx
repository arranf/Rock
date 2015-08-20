<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupListLava.ascx.cs" Inherits="RockWeb.Blocks.Groups.GroupListLava" %>

<asp:UpdatePanel ID="upnlGroupList" runat="server">
    <ContentTemplate>
        
        <asp:Literal ID="lContent" runat="server"></asp:Literal>

        <asp:Literal ID="lDebug" runat="server"></asp:Literal>

    </ContentTemplate>
</asp:UpdatePanel>
