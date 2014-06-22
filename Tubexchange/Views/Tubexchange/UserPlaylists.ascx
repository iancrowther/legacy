<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Google.YouTube" %>
<%@ Import Namespace="Google.GData.Client" %>
<%
    Feed<Playlist> playlists = (Feed<Playlist>)ViewData["playlists"];

    if (playlists != null)
    { 
        foreach (Playlist item in playlists.Entries)
        {
        %>
            
            <p>
            <%= Html.ActionLink(item.Title.ToString(), "ViewPlaylist", "Tubexchange", new { title = item.Title, username = (String)ViewData["username"] }, null)%>
            <%= Html.ActionLink("Copy " + item.Title.ToString(), "CopyPlaylist", "Tubexchange", new { title = item.Title, username = (String)ViewData["username"] }, null)%>
            </p>  
        <%
        }
    }
%>