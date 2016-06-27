using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTSDataManager.DB
{
    [MetadataType(typeof(MusicGroupMetadataType))]
    public partial class MusicGroup
    {
        public string JoinedGenres
        {
            get { return string.Join(", ", MusicGroupGenres.Select(x => x.Genre.Name).Distinct()); }
        }
    }

    public class MusicGroupMetadataType
    {
        [DisplayName("Name")]
        public int Name { get; set; }

        [DisplayName("Genres")]
        public string JoinedGenres { get; }

        [DisplayName("Poster")]
        public string PosterURL { get; }
    }
}
