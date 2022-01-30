using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
	public static class PhotoUtil
	{
		public static bool SizeChecker(IFormFile file, int width, int height)
		{
			Bitmap image = (Bitmap)Bitmap.FromStream(file.OpenReadStream());
			if (width == width && height == image.Height)
				return true;
			return false;
		}
	}
}
