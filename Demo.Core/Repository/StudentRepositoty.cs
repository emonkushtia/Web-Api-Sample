using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Demo.Core.DomainObjects;
using Demo.Core.Infrastructure;
using Demo.Core.Interfaces;

namespace Demo.Core.Repository
{
    public class StudentRepositoty : IRepository<Student>
    {
        private List<Student> students=new List<Student>
            {
                new Student { Id= 1,  FirstName= "Jon",  LastName= "Sina", Email= "Von@yahoo.com", Phone= "01892802093" },
                new Student { Id= 2,  FirstName= "Sam",  LastName= "Papa", Email= "Zon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 3,  FirstName= "Pang", LastName= "Sang", Email= "Lon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 4,  FirstName= "Emon", LastName= "Khan", Email= "Kon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 5,  FirstName= "Jon",  LastName= "Sina", Email= "Jon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 6,  FirstName= "Lam",  LastName= "Yapa", Email= "Hon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 7,  FirstName= "Zang", LastName= "Vang", Email= "Gon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 8,  FirstName= "Xmon", LastName= "Bhan", Email= "Fon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 9,  FirstName= "Yon",  LastName= "Zina", Email= "Don@yahoo.com", Phone= "01892802093" },
                new Student { Id= 10, FirstName= "Eam",  LastName= "Xapa", Email= "Son@yahoo.com", Phone= "01892802093" },
                new Student { Id= 11, FirstName= "Wang", LastName= "Lang", Email= "Aon@yahoo.com", Phone= "71892802093" },
                new Student { Id= 12, FirstName= "Qmon", LastName= "Nhan", Email= "Pon@yahoo.com", Phone= "21892802093" },
                new Student { Id= 13, FirstName= "Mon",  LastName= "Qina", Email= "Oon@yahoo.com", Phone= "01892802093" },
                new Student { Id= 14, FirstName= "Gam",  LastName= "Aapa", Email= "Ion@yahoo.com", Phone= "81892802093" },
                new Student { Id= 15, FirstName= "Pang", LastName= "Tang", Email= "Uon@yahoo.com", Phone= "41892802093" },
                new Student { Id= 16, FirstName= "Smon", LastName= "Fhan", Email= "Yon@yahoo.com", Phone= "701892802093" },
                new Student { Id= 17, FirstName= "Oon",  LastName= "Mina", Email= "Ton@yahoo.com", Phone= "91892802093" },
                new Student { Id= 18, FirstName= "Kam",  LastName= "Rapa", Email= "Ron@yahoo.com", Phone= "41892802093" },
                new Student { Id= 19, FirstName= "Hang", LastName= "Dang", Email= "Eon@yahoo.com", Phone= "31892802093" },
                new Student { Id= 20, FirstName= "Fmon", LastName= "Lhan", Email= "Won@yahoo.com", Phone= "21892802093" },
                new Student { Id= 21, FirstName= "Cmon", LastName= "Phan", Email= "Qon@yahoo.com", Phone= "11892802093" },
            };
        public StudentRepositoty()
        {
            if (!HttpContext.Current.Application.AllKeys.Contains("Students"))
            {
                HttpContext.Current.Application["Students"] = students;
            }
            else students = (List<Student>) HttpContext.Current.Application["Students"];

        }

        private void UpdateApplicationState()
        {
            HttpContext.Current.Application["Students"] = students;
        }

        private IQueryable<Student> _students;

        public IQueryable<Student> Students
        {
            get { return students.AsQueryable(); }
        }

        private int NewStudentId {
            get { return this.Students.Count() + 1; }
        }
        public Student Get(int id)
        {
           return  this.Students.FirstOrDefault(x => x.Id == id);
        }
        public Student Save(Student obj)
        {
            obj.Id = this.NewStudentId;
            students.Add(obj);
            UpdateApplicationState();
            return obj;
        }

        public void Update(Student obj)
        {
            var student = students.FirstOrDefault(x => x.Id == obj.Id);
            students.Remove(student);
            students.Add(obj);
            UpdateApplicationState();
        }

        public void Delete(int id)
        {
            var student = students.FirstOrDefault(x => x.Id == id); 
            if (student == null) 
                return;
            students.Remove(student);
            UpdateApplicationState();
        }

        public Student Clone(int id)
        {
            var student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
               throw new Exception("Student not found");
            var newStudent = new Student
            {
                Id = this.NewStudentId,
                FirstName = student.FirstName + " Cloned",
                LastName = student.LastName + " Cloned",
                Email = student.Email,
                Phone = student.Phone
            };
            students.Add(newStudent);
            UpdateApplicationState();
            return newStudent;
        }
        public PagedListResult<Student> GetPagedList(PageableListQueryCommand<Student> command)
        {
            var orderBy = command.GetOrderByClause();

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = "firstName desc";
            }
            var students = this.Students
                    .OrderBy(orderBy)
                    .Skip(command.Offset.GetValueOrDefault())
                    .Take(command.Limit)
                    .ToList();
            return new PagedListResult<Student>(students,this.Students.Count());
        }
    }
}