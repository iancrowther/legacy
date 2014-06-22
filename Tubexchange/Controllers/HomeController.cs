using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;
using System.Net;

namespace Tubexchange.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string title, string username)
        {
            Session["title"] = title;
            Session["username"] = username;
            ViewData["HREF"] = AuthSubUtil.getRequestUrl("http://localhost:58395/Home/Auth/", "http://gdata.youtube.com", false, true);
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
            
            Feed<Playlist> userPlaylists = request.GetPlaylistsFeed(username);
            
            foreach (Playlist p in userPlaylists.Entries)
            {
                if (p.Title == (string)Session["title"])
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
            return RedirectToAction("ViewPlaylist",
        }
    }
}
