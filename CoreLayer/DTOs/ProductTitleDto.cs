using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
	public class ProductTitleDto
	{
		public int id { get; set; }
		public int Groupid { get; set; }
		public int CompanyId { get; set; }
		public string ProductName { get; set; }
        public int Price { get; set; }
		public string SliderPhoto { get; set; }
		public List<string> ProductPhotos { get; set; }
        public int Off { get; set; }
        public string ShortDescription { get; set; }
		public string Slug { get; set; }
		public string ShopName { get; set; }
	}
}
