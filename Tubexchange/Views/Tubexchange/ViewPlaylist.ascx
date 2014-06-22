<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Google.YouTube" %>
<%@ Import Namespace="Google.GData.Client" %>

<%
    List<PlayListMember> videos = (List<PlayListMember>)ViewData["videos"];

    if (videos != null)
    {
        %> <div class='main'> <%
        
        foreach (PlayListMember item in videos)
        {
            if (item.VideoId != null)
            {
                String id = item.VideoId;
                String url = "http://www.youtube.com/v/" + id;
                %>
                
                <div class='listItems'>
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

