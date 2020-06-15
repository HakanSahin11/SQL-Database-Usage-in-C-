namespace Database_sql
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActorRole
    {
        public int Id { get; set; }
        public Nullable<int> ActorId { get; set; }
        public Nullable<int> FilmId { get; set; }
        public string RoleName { get; set; }
    
        public virtual Actor Actor { get; set; }
        public virtual Film Film { get; set; }
    }
}
