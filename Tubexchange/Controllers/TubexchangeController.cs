using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Google.YouTube;
using Google.GData.Client;
using Google.GData.YouTube;
using System.Text.RegularExpressions;

namespace Tubexchange.Controllers
{
    public class TubexchangeController : Controller
    {
        public YouTubeRequest request = new YouTubeRequest(new YouTubeRequestSettings("Tubexchange", "ytapi-IanCrowther-Tubexchange-fn6h5kpl-0", "AI39si7e8Dygrq4lO5c8TONmg3ld3m5vMjTDhSFgH7LX6wHhFF_pP04o00Ox6F3X4ZzZAzVmLLa_5KI0nZXzyyW14RpcYKbwMg"));

        public ActionResult Index()
        {
            return View();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitUsername(string username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                Feed<Playlist> userPlaylists = request.GetPlaylistsFeed(username);

                try
                {
                    if (userPlaylists.TotalResults > 1)
                    {
                        string[] x = new string[userPlaylists.TotalResults];
                        int i = 0;
                        foreach (Playlist p in userPlaylists.Entries)
                        {
                            x[i] = p.Title;
                            i++;
                        }

                        var data = new { username = username, playlists = x, feedback = "All is well, Yay!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { username = username, playlists = "", feedback = "Hmmm! I could not find any playlists, they may have none!" };
                        return Json(data);
                    }
                }
                catch
                {
                    var data = new { username = username, playlists = "", feedback = "Hmmm! I could not find any users by that name!" };
                    return Json(data);
                }
            }
            return View();
        }

       /* public ActionResult UserPlaylists(string username)
        {
            Feed<Playlist> userPlaylists = request.GetPlaylistsFeed(username);
            ViewData["playlists"] = userPlaylists;
            ViewData["username"] = username;
            return View();
        }
        */
        public ActionResult ViewPlaylist(string username, string title)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(title))
            {
                Feed<Playlist> userPlaylists = request.GetPlaylistsFeed(username);
                foreach (Playlist p in userPlaylists.Entries)
                {
                    if (p.Title == title)
                    {
                        Feed<PlayListMember> playlistMembersFeed = request.GetPlaylist(p);
                        List<PlayListMember> playlistMembers = new List<PlayListMember>(playlistMembersFeed.Entries);

                        string[] x = new string[playlistMembers.Count];
                        int i = 0;
                        foreach (PlayListMember m in playlistMembers)
                        {
                            x[i] = m.VideoId;
                            i++;
                        }

                        var playlist = new { username = username, videos = x, feedback = "All is well, Yay!" };
                        return Json(playlist);
                    }
                }
            }
            return View();
        }

        public ActionResult CopyPlaylist(string title, string username)
        {
            Session["title"] = title;
            Session["username"] = username;
            string url = "http://" + Request.ServerVariables["SERVER_NAME"] + "/Tubexchange/Auth/";  //+ ":" + Request.ServerVariables["SERVER_PORT"]  
            ViewData["HREF"] = AuthSubUtil.getRequestUrl(url, "http://gdata.youtube.com", false, true);
            return View();
        }

        public ActionResult Auth(string token)
        {
            Session["AuthToken"] = AuthSubUtil.exchangeForSessionToken(token, null).ToString();
            GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory("youtube", "Tubexchange");
            authFactory.Token = (string)Session["AuthToken"];
            YouTubeService service = new YouTubeService(authFactory.ApplicationName, "ytapi-IanCrowther-Tubexchange-fn6h5kpl-0", "AI39si7e8Dygrq4lO5c8TONmg3ld3m5vMjTDhSFgH7LX6wHhFF_pP04o00Ox6F3X4ZzZAzVmLLa_5KI0nZXzyyW14RpcYKbwMg");
            service.RequestFactory = authFactory;

            YouTubeRequestSettings settings = new YouTubeRequestSettings("Tubexchange", "ytapi-IanCrowther-Tubexchange-fn6h5kpl-0", "AI39si7e8Dygrq4lO5c8TONmg3ld3m5vMjTDhSFgH7LX6wHhFF_pP04o00Ox6F3X4ZzZAzVmLLa_5KI0nZXzyyW14RpcYKbwMg", (string)Session["AuthToken"]);
            YouTubeRequest request = new YouTubeRequest(settings);

            string username = (string)Session["username"];
            string title = (string)Session["title"];
            
            Feed<Playlist> userPlaylists = request.GetPlaylistsFeed(username);
            
            foreach (Playlist p in userPlaylists.Entries)
            {
                if (p.Title == title)
                {
                    Playlist createdPlaylist = request.Insert(new Uri("http://gdata.youtube.com/feeds/api/users/default/playlists"), p);
                    
                    Feed<PlayListMember> playlistMembersFeed = request.GetPlaylist(p);
                    List<PlayListMember> playlistMembers = new List<PlayListMember>(playlistMembersFeed.Entries);

                    foreach (PlayListMember member in playlistMembers)
                    {
                        PlayListMember pm = new PlayListMember();
                        pm.Id = member.PlaylistEntry.VideoId;

                        request.AddToPlaylist(createdPlaylist, pm);
                    }
                }
            }
            return RedirectToAction("UserPlaylists", new { username = username });
        }
    }
}
