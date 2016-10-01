namespace Demo.Core.Repository
{
    using System.Linq;
    using System.Linq.Dynamic;

    using DomainObjects;
    using Infrastructure;
    using Interfaces;

    public class StudentRepositoty : IRepository<Student>
    {
        private readonly DemoDataContext dataContext;

        public StudentRepositoty()
        {
            this.dataContext = new DemoDataContext();
        }

        public Student Get(int id)
        {
           return this.dataContext.Students.FirstOrDefault(x => x.Id == id);
        }

        public Student Save(Student obj)
        {
            this.dataContext.Students.Add(obj);
            this.dataContext.SaveChanges();
            return obj;
        }

        public void Update(Student obj)
        {
            var student = this.dataContext.Students.FirstOrDefault(x => x.Id == obj.Id);
            if (student == null)
            {
                return;
            }

            student.Email = obj.Email;
            student.FirstName = obj.FirstName;
            student.LastName = obj.LastName;
            student.Phone = obj.Phone;

            this.dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = this.dataContext.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return;
            }

            this.dataContext.Students.Remove(student);
            this.dataContext.SaveChanges();
        }

        public Student Clone(int id)
        {
            var student = this.dataContext.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
               return null;
            }

            var newStudent = new Student
            {
                FirstName = student.FirstName + " Cloned",
                LastName = student.LastName + " Cloned",
                Email = student.Email,
                Phone = student.Phone
            };
            this.dataContext.Students.Add(newStudent);
            this.dataContext.SaveChanges();
            return newStudent;
        }

        public PagedListResult<Student> GetPagedList(PageableListQueryCommand<Student> command)
        {
            var orderBy = command.GetOrderByClause();

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = "firstName desc";
            }

            var students = this.dataContext.Students
                    .OrderBy(orderBy)
                    .Skip(command.Offset.GetValueOrDefault())
                    .Take(command.Limit)
                    .ToList();
            return new PagedListResult<Student>(students, this.dataContext.Students.Count());
        }
    }
}