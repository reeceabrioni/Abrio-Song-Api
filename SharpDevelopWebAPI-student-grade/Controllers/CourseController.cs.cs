using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{

	public class CourseController : ApiController
	{
		readonly SDWebApiDbContext _db = new SDWebApiDbContext();
		
	    [HttpPost]
		[Route("api/course/new")]		
        public IHttpActionResult Create(Course course)
        {
            _db.Courses.Add(course);
            _db.SaveChanges();
            return Ok(course);
        }
        
	    [HttpGet]
		[Route("api/course/allcourses")]		
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var courses = new List<Course>();
            if(!string.IsNullOrEmpty(keyword))
            {
               courses = _db.Courses
               	.Where(x => x.Name.Contains(keyword))
                    .ToList();
            }
            
            courses = _db.Courses.ToList();
            return Ok(courses);
        }
        
        [HttpGet]
        [Route("api/course/find/{Id}")]		
        public IHttpActionResult Get(int Id)
        {       
            var course = _db.Courses.Find(Id);
            if (course != null)
                return Ok(course);
            else
                return BadRequest("Course Id is invalid or not found");
        }
        
        [HttpDelete]
        [Route("api/course/remove/{Id}")]		
        public IHttpActionResult Delete(int Id)
        {
            var course = _db.Courses.Find(Id);
            if (course != null)
            {
                _db.Courses.Remove(course);
                _db.SaveChanges();
                return Ok("Course removed successfully!");
            }
            else
                return BadRequest("Course Id is invalid or not found");
        }
        [HttpPut]
        [Route("api/course/update")]		
        public IHttpActionResult Update(Course courseUpdate)
        {
            var course = _db.Courses.Find(courseUpdate.Id);
            if (course != null)
            {	
            	course.Name = courseUpdate.Name;
                _db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(course);
            }
            else
                return BadRequest("Course Id is invalid or not found");
        }
	}
}