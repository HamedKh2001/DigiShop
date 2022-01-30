using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
	public class SearchParamsDto
	{
		public int? GroupId { get; set; }
		public List<int> BrandIdList { get; set; }
		public bool IsExist { get; set; }
		public string SortType { get; set; }
	}
}
