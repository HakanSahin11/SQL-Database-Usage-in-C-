namespace Database_sql
{
    using System;
    using System.Collections.Generic;
    
    public partial class FilmDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FilmDetail()
        {
            this.Films = new HashSet<Film>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> FilmId { get; set; }
        public Nullable<int> Playtime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Film> Films { get; set; }
    }
}
