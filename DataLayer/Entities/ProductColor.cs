using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
	public class ProductColor
	{
		public int Id { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public string ColorCode { get; set; }
		[Required]
		public int Quantity { get; set; }


		#region Relations
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
		#endregion

	}
}
