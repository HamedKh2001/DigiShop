using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
	public class Event : BaseEntity
	{
		public string ItemName { get; set; }
		public string Description { get; set; }
		public string Link { get; set; } = "#";
		public AdvertisingType TypeOfAdvertise { get; set; }
		public bool IsDeleted { get; set; } = false;

		[Bindable(false)]
		public virtual BannerPhoto BannerPhotos { get; set; }

		public enum AdvertisingType
		{
			[Display(Name ="بنر سربرگ")]
			header_banner,
			small_widget_banner,
			medium_widget_banner,
			large_widget_banner,
			side_banner
		}
	}

}
