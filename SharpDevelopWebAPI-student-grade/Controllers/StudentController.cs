using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{

	public class StudentController : ApiController
	{
		readonly SDWebApiDbContext _db = new SDWebApiDbContext();
		
	    [HttpPost]
		[Route("api/student/new")]		
        public IHttpActionResult Create(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
            return Ok(student);
        }
        
	    [HttpGet]
		[Route("api/student/allstudents")]		
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var students = new List<Student>();
            if(!string.IsNullOrEmpty(keyword))
            {
               students = _db.Students
                    .Where(x => x.LastName.Contains(keyword) || x.FirstName.Contains(keyword))
                    .ToList();
            }
            
            students = _db.Students.ToList();
            return Ok(students);
        }
        
        [HttpGet]
        [Route("api/student/find/{Id}")]		
        public IHttpActionResult Get(int Id)
        {       
            var student = _db.Students.Find(Id);
            if (student != null)
                return Ok(student);
            else
                return BadRequest("Student Id is invalid or not found");
        }
        
        [HttpDelete]
        [Route("api/student/remove/{Id}")]		
        public IHttpActionResult Delete(int Id)
        {
            var student = _db.Students.Find(Id);
            if (student != null)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                return Ok("Student removed successfully!");
            }
            else
                return BadRequest("Student Id is invalid or not found");
        }
        [HttpPut]
        [Route("api/student/update")]		
        public IHttpActionResult Update(Student studentUpdate)
        {
            var student = _db.Students.Find(studentUpdate.Id);
            if (student != null)
            {	
            	student.FirstName = studentUpdate.FirstName;
            	student.LastName = studentUpdate.LastName;
            	student.SchoolLastAttended = studentUpdate.SchoolLastAttended;
            	student.Gender = studentUpdate.Gender;
            	student.CivilStatus = studentUpdate.CivilStatus;
            	student.Course = studentUpdate.Course;
            	
                _db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(student);
            }
            else
                return BadRequest("Student Id is invalid or not found");
        }
	}
}