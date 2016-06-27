using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QTSDataManager.DB
{
    [MetadataType(typeof(ConcertMetadataType))]
    public partial class Concert
    {
         
    }

    public class ConcertMetadataType
    {
        [DisplayName("Title")]
        public int Name { get; set; }

        [DisplayName("Poster")]
        public string PosterURL { get; }

        [DisplayName("Type of place")]
        public string TypeOfPlace { get; }
    }
}