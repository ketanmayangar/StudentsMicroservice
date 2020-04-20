using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsMicroservice.Models;
using StudentsMicroservice.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Cors;

namespace StudentsMicroservice.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("AllowOrigin")]
    [ApiController]
    public class StudentController : ControllerBase
    {


        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        // GET: api/Student
        [HttpGet]
       // [Microsoft.AspNetCore.Cors.EnableCors("AllowOrigin")]
        public IActionResult Get()
        {
            var students = _studentRepository.GetStudents();
            //var ls=student.
            IEnumerable<string> result = students.Select(item => item.Name);
            return new OkObjectResult(result);
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var student = _studentRepository.GetStudentByID(id);
            return new OkObjectResult(student);
        }

        [Route("GetStudentById/{id}")]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentRepository.GetStudentByID(id);
            return new OkObjectResult(student);
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            using (var scope = new TransactionScope())
            {
                _studentRepository.InsertStudent(student);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
            }
        }

        // PUT: api/Student/5
        [HttpPut]
        public IActionResult Put([FromBody] Student student)
        {
            if (student != null)
            {
                using (var scope = new TransactionScope())
                {
                    _studentRepository.UpdateStudent(student);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentRepository.DeleteStudent(id);
            return new OkResult();
        }

    }
}