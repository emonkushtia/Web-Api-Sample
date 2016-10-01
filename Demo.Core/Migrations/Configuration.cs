namespace Demo.Core.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using DomainObjects;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoDataContext>
    {
        private readonly List<Student> students  = new List<Student>
            {
                new Student { FirstName = "Jon",  LastName = "Sina", Email = "Von@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Sam",  LastName = "Papa", Email = "Zon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Pang", LastName = "Sang", Email = "Lon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Emon", LastName = "Khan", Email = "Kon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Jon",  LastName = "Sina", Email = "Jon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Lam",  LastName = "Yapa", Email = "Hon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Zang", LastName = "Vang", Email = "Gon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Xmon", LastName = "Bhan", Email = "Fon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Yon",  LastName = "Zina", Email = "Don@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Eam",  LastName = "Xapa", Email = "Son@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Wang", LastName = "Lang", Email = "Aon@yahoo.com", Phone = "71892802093" },
                new Student { FirstName = "Qmon", LastName = "Nhan", Email = "Pon@yahoo.com", Phone = "21892802093" },
                new Student { FirstName = "Mon",  LastName = "Qina", Email = "Oon@yahoo.com", Phone = "01892802093" },
                new Student { FirstName = "Gam",  LastName = "Aapa", Email = "Ion@yahoo.com", Phone = "81892802093" },
                new Student { FirstName = "Pang", LastName = "Tang", Email = "Uon@yahoo.com", Phone = "41892802093" },
                new Student { FirstName = "Smon", LastName = "Fhan", Email = "Yon@yahoo.com", Phone = "701892802093" },
                new Student { FirstName = "Oon",  LastName = "Mina", Email = "Ton@yahoo.com", Phone = "91892802093" },
                new Student { FirstName = "Kam",  LastName = "Rapa", Email = "Ron@yahoo.com", Phone = "41892802093" },
                new Student { FirstName = "Hang", LastName = "Dang", Email = "Eon@yahoo.com", Phone = "31892802093" },
                new Student { FirstName = "Fmon", LastName = "Lhan", Email = "Won@yahoo.com", Phone = "21892802093" },
                new Student { FirstName = "Cmon", LastName = "Phan", Email = "Qon@yahoo.com", Phone = "11892802093" },
            };  
                              
        public Configuration()
        {
            this.AutomaticMigrationsEnabled  = false;
        }

        protected override void Seed(DemoDataContext context)
        {
            context.Students.AddRange(this.students);
        }
    }
}
