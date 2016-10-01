namespace Demo.Core
{
    using System.Data.Entity;

    using DomainObjects;

    public class DemoDataContext : DbContext
    {
        public DemoDataContext() : base("SchoolContext")
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
