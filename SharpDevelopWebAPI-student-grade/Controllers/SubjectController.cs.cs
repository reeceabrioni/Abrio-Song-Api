using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{

	public class SubjectController : ApiController
	{
		readonly SDWebApiDbContext _db = new SDWebApiDbContext();
		
	    [HttpPost]
		[Route("api/subject/new")]		
        public IHttpActionResult Create(Subject subject)
        {
            _db.Subjects.Add(subject);
            _db.SaveChanges();
            return Ok(subject);
        }
        
	    [HttpGet]
		[Route("api/subject/allsubjects")]		
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var subjects = new List<Subject>();
            if(!string.IsNullOrEmpty(keyword))
            {
               subjects = _db.Subjects
               	.Where(x => x.DescriptiveTitle.Contains(keyword) || x.Code.Contains(keyword) )
                    .ToList();
            }
            
            subjects = _db.Subjects.ToList();
            return Ok(subjects);
        }
        
        [HttpGet]
        [Route("api/subject/find/{Id}")]		
        public IHttpActionResult Get(int Id)
        {       
            var subject = _db.Subjects.Find(Id);
            if (subject != null)
                return Ok(subject);
            else
                return BadRequest("Subject Id is invalid or not found");
        }
        
        [HttpDelete]
        [Route("api/subject/remove/{Id}")]		
        public IHttpActionResult Delete(int Id)
        {
            var subject = _db.Subjects.Find(Id);
            if (subject != null)
            {
                _db.Subjects.Remove(subject);
                _db.SaveChanges();
                return Ok("Subject removed successfully!");
            }
            else
                return BadRequest("Subject Id is invalid or not found");
        }
        [HttpPut]
        [Route("api/subject/update")]		
        public IHttpActionResult Update(Subject subjectUpdate)
        {
            var subject = _db.Subjects.Find(subjectUpdate.Id);
            if (subject != null)
            {	
            	subject.Code = subjectUpdate.Code;
            	subject.DescriptiveTitle = subjectUpdate.DescriptiveTitle;
                _db.Entry(subject).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(subject);
            }
            else
                return BadRequest("Subject Id is invalid or not found");
        }
	}
}