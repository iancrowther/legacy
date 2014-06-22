using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tubexchange.Models;

namespace Tubexchange.Models
{
    public interface IHomeRepository
    {
        Playlist GetPlaylist(String UserName);
        
    }
}
