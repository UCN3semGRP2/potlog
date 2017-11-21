namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Model;

    public class DALContext : DbContext
    {
        public DALContext()
            : base("name=DALContext")
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
    }
}