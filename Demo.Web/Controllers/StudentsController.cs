namespace Demo.Web.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Core.DomainObjects;
    using Core.Infrastructure;
    using Core.Interfaces;
    using Core.Repository;

    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly IRepository<Student> studentRepositoty;

        public StudentsController()
        {
            this.studentRepositoty = new StudentRepositoty();
        }

        [Route("")]
        public HttpResponseMessage Get([FromUri] PageableListQueryCommand<Student> command)
        {
            var students = this.studentRepositoty.GetPagedList(command);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                students);
        }

        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var student = this.studentRepositoty.Get(id);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                student);
        }

        [Route("")]
        public HttpResponseMessage Post(Student student)
        {
            var stu = this.studentRepositoty.Save(student);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                stu);
        }

        [Route("")]
        public HttpResponseMessage Put(Student student)
        {
            this.studentRepositoty.Update(student);
            return this.Request.CreateResponse(
                HttpStatusCode.NoContent,
                student);
        }

        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            this.studentRepositoty.Delete(id);
            return this.Request.CreateResponse(
                HttpStatusCode.NoContent);
        }

        [Route("{id:int}/clone")]
        public HttpResponseMessage Put(int id)
        {
           var student = this.studentRepositoty.Clone(id);
            return this.Request.CreateResponse(
                HttpStatusCode.OK, student);
        }
    }
}
