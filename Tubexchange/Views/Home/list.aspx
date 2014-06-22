<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	list
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Google.GData.Client.Feed<Google.YouTube.PlayListMember> list = (Google.GData.Client.Feed<Google.YouTube.PlayListMember>)ViewData["member"]; %>
<% foreach (Google.YouTube.Video v in list.Entries) 
   { %>
       
       <%= v.Title %><br />
       <div id="ytapiplayer">
            You need Flash player 8+ and JavaScript enabled to view this video.
        </div>
       <script type="text/javascript">
           var params = { allowScriptAccess: "always" };
           var atts = { id: "myytplayer" };
           swfobject.embedSWF("http://www.youtube.com/v/<%= v.AtomEntry.AlternateUri.Content.Substring(31, 11) %>&enablejsapi=1&playerapiid=ytplayer",
                                "ytapiplayer", "425", "356", "8", null, null, params, atts);
       </script>
       <br />
<% } %> 

  





<script type="text/javascript">
    
    swfobject.embedSWF("http://www.youtube.com/v/m-Iq5sOIKAo&enablejsapi=1&playerapiid=ytplayer", "myContent", "300", "120", "9.0.0");
</script>
</asp:Content>
