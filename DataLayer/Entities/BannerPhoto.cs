using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class BannerPhoto:BaseEntity
	{
		public int EventID { get; set; }
		public string PhotoName { get; set; }


		[ForeignKey("EventID")]
		public virtual Event Event { get; set; }
	}
}
