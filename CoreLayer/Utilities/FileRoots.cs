  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
	public static class FileRoots
	{
		public static string SliderRoot
		{
			get { return "wwwroot/Photos/ProductPhotos/"; }
		}
		public static string ReadProductphotoRoot
		{
			get { return "/Photos/ProductPhotos/"; }
		}
		public static string GetStreamRoot
		{
			get { return $"\\wwwroot\\Photos\\ProductPhotos\\"; }
		}
		public static string GroupProductRoot
		{
			get { return "Photos/GroupProductPhotos/"; }
		}
		public static string GroupProductsaveRoot
		{
			get { return "/wwwroot/Photos/GroupProductPhotos/"; }
		}
		public static string LogsRoot
		{
			get { return "\\wwwroot\\Logstxt\\"; }
		}
		public static string BannerRoot
		{
			get { return "wwwroot/Photos/BannerPhotos/"; }
		}
		public static string ReadBannerRoot
		{
			get { return "/Photos/BannerPhotos/"; }
		}
	}
}
