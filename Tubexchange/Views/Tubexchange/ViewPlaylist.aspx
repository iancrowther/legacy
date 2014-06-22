<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Google.YouTube" %>
<%@ Import Namespace="Google.GData.Client" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    List<PlayListMember> videos = (List<PlayListMember>)ViewData["videos"];
    
    if (videos != null)
    {
        %> <div class='main'><div><%= Html.ActionLink("Back to Playlists", "UserPlaylists", "Tubexchange", new { username = (String)ViewData["username"] }, null)%></div><br /><br /> <%
        
        foreach (PlayListMember item in videos)
        {
            if (item.VideoId != null)
            {
                String id = item.VideoId;
                String url = "http://www.youtube.com/v/" + id;
                %>
                
                <div class='videolistItems'>
                    <object type="application/x-shockwave-flash" style="width:425px; height:350px;" data="<%= url %>">
                    <param name="movie" value="<%= url %>" />
                    </object>     
                </div>
                <%
            }
        }
        %> </div> <%
    }
%>
</asp:Content>
