using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
	public class EditProductDto: ProductDto
	{
		public GroupProduct Group { get; set; }
		public string UserName { get; set; }
		public Company Company { get; set; }
		
	}
}
