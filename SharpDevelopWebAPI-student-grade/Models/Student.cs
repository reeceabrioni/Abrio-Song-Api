using System;

namespace SharpDevelopWebApi.Models
{
	public class Student : Person
	{
		public string Course { get; set; }
		public string SchoolLastAttended { get; set; }

	}
}
