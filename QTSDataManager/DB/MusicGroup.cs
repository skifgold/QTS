//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QTSDataManager.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class MusicGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MusicGroup()
        {
            this.ConcertGroups = new HashSet<ConcertGroup>();
            this.MusicGroupGenres = new HashSet<MusicGroupGenre>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Annotation { get; set; }
        public string PosterURL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConcertGroup> ConcertGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MusicGroupGenre> MusicGroupGenres { get; set; }
    }
}
