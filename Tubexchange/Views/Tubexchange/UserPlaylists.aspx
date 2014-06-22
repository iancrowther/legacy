<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Google.YouTube" %>
<%@ Import Namespace="Google.GData.Client" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    Feed<Playlist> playlists = (Feed<Playlist>)ViewData["playlists"];

    if (playlists != null)
    { 
        %> <div class='main'> <%
        
        foreach (Playlist item in playlists.Entries)
        {
        %>
            <div class='listItems'>
                <span class='left'><%= Html.ActionLink("View " + item.Title.ToString(), "ViewPlaylist", "Tubexchange", new { title = item.Title, username = (String)ViewData["username"] }, null)%></span>
                <span class='right'><%= Html.ActionLink("Copy " + item.Title.ToString(), "CopyPlaylist", "Tubexchange", new { title = item.Title, username = (String)ViewData["username"] }, null)%></span>
            </div>
        <%
        }
        %> </div> <%
    }
%>
</asp:Content>
