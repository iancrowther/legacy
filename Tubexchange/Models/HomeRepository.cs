using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;

namespace Tubexchange.Models
{
    public class HomeRepository : IHomeRepository
    {
        public HomeRepository(String ClientId, String DeveloperKey)
        {
            YouTubeRequestSettings settings = new YouTubeRequestSettings("Tubexchange", ClientId, DeveloperKey);
            YouTubeRequest request = new YouTubeRequest(settings);
        }

        public Playlist GetPlaylist(String UserName)
        {
            Playlist list = new Playlist();
                list.UserName = UserName;
            return list;
        }
    }
}
