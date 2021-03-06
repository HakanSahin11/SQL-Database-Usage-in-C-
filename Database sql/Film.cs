namespace Database_sql
{
    using System;
    using System.Collections.Generic;
    
    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            this.ActorRoles = new HashSet<ActorRole>();
            this.Users = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public string Titel { get; set; }
        public byte[] Picture { get; set; }
        public string Genre { get; set; }
        public Nullable<double> Rating { get; set; }
        public Nullable<System.DateTime> Release { get; set; }
        public Nullable<System.DateTime> Expired { get; set; }
        public Nullable<int> FilmDetailsId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActorRole> ActorRoles { get; set; }
        public virtual FilmDetail FilmDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
