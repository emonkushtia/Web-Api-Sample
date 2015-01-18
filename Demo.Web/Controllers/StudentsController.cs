using System.Net;
using System.Net.Http;
using System.Web.Http;
using Demo.Core.DomainObjects;
using Demo.Core.Infrastructure;
using Demo.Core.Interfaces;
using Demo.Core.Repository;
namespace Demo.Web.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private IRepository<Student> studentRepositoty = null;

        public StudentsController()
        {
            studentRepositoty=new StudentRepositoty();
        }
        [Route("")]
        public HttpResponseMessage Get([FromUri]  PageableListQueryCommand<Student> command)
        {
            var students = studentRepositoty.GetPagedList(command);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                students);
        }

        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var student = studentRepositoty.Get(id);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                student);
        }
        [Route("")]
        public HttpResponseMessage Post(Student student)
        {
            var stu = studentRepositoty.Save(student);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                stu);
        }
        [Route("")]
        public HttpResponseMessage Put(Student student )
        {
            studentRepositoty.Update(student);
            return this.Request.CreateResponse(
                HttpStatusCode.NoContent,
                student);
        }

        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
           studentRepositoty.Delete(id);
            return this.Request.CreateResponse(
                HttpStatusCode.NoContent);
        }
        [Route("{id:int}/clone")]
        public HttpResponseMessage Put(int id)
        {
           var student= studentRepositoty.Clone(id);
            return this.Request.CreateResponse(
                HttpStatusCode.OK,student);
        }

    }
}
