using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{

	public class FacultyController : ApiController
	{
		readonly SDWebApiDbContext _db = new SDWebApiDbContext();
		
	    [HttpPost]
		[Route("api/faculty/new")]		
        public IHttpActionResult Create(Faculty faculty)
        {
            _db.Faculties.Add(faculty);
            _db.SaveChanges();
            return Ok(faculty);
        }
        
	    [HttpGet]
		[Route("api/faculty/allfaculties")]		
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var faculties = new List<Faculty>();
            if(!string.IsNullOrEmpty(keyword))
            {
               faculties = _db.Faculties
               	.Where(x => x.FirstName.Contains(keyword) || x.SSSNumber.Contains(keyword) )
                    .ToList();
            }
            
            faculties = _db.Faculties.ToList();
            return Ok(faculties);
        }
        
        [HttpGet]
        [Route("api/faculty/find/{Id}")]		
        public IHttpActionResult Get(int Id)
        {       
            var faculty = _db.Faculties.Find(Id);
            if (faculty != null)
                return Ok(faculty);
            else
                return BadRequest("Faculty Id is invalid or not found");
        }
        
        [HttpDelete]
        [Route("api/faculty/remove/{Id}")]		
        public IHttpActionResult Delete(int Id)
        {
            var faculty = _db.Faculties.Find(Id);
            if (faculty != null)
            {
                _db.Faculties.Remove(faculty);
                _db.SaveChanges();
                return Ok("Faculty removed successfully!");
            }
            else
                return BadRequest("Faculty Id is invalid or not found");
        }
        [HttpPut]
        [Route("api/faculty/update")]		
        public IHttpActionResult Update(Faculty facultyUpdate)
        {
            var faculty = _db.Faculties.Find(facultyUpdate.Id);
            if (faculty != null)
            {	
            	faculty.FirstName = facultyUpdate.FirstName;
            	faculty.LastName = facultyUpdate.LastName;
            	faculty.CivilStatus = facultyUpdate.CivilStatus;
            	faculty.SSSNumber = facultyUpdate.SSSNumber;
            	faculty.SuperVisor = facultyUpdate.SuperVisor;
            	faculty.BirthDate = facultyUpdate.BirthDate;
            	faculty.Gender = facultyUpdate.Gender;
                _db.Entry(faculty).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(faculty);
            }
            else
                return BadRequest("Faculty Id is invalid or not found");
        }
	}
}