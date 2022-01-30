using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public User.UserRole RoleName { get; set; }
		
		
		public DateTime LastLoginTime { get; set; }
	}
}
