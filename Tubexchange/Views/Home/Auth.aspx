<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Playlist
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var transaction = YAHOO.util.Connect.asyncRequest('GET', "http://localhost:58395/Home/Auth", function() { alert('yo'); }, null);
</script>
</asp:Content>