using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.ExternalEntities.ComicEntities
{
    public class ComicVolumeExt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public string Image { get; set; }
    }
}
