using System;
using System.ComponentModel.DataAnnotations;

namespace SharpDevelopWebApi.Models
{
	public class Course
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set;}
	}
}
