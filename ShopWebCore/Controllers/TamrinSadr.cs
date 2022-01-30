using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopWebCore.Controllers
{

	public class TamrinSadr : Controller
	{
		private readonly ShopContext _context;
		public TamrinSadr(ShopContext context)
		{
			_context = context;
		}
		public IActionResult Event()
		{
			return View();
		}

		public IActionResult BeforeClick()
		{
			return View("Click", "This is BeforeClick");
		}

		public IActionResult Click()
		{
			return View("Click", "This is Click");
		}

		public IActionResult AfterClick()
		{
			return View("Click", "This is AfterClick");
		}
		public IActionResult Timer()
		{
			return View("Timer", DateTime.Now.ToLongTimeString().ToString());
		}
		public IActionResult SetClock()
		{
			var time = DateTime.Now.ToLongTimeString();
			return new JsonResult(time);
		}
		public IActionResult Cheshmack()
		{
			return View("Cheshmack", DateTime.Now.ToLongTimeString().ToString());
		}
		public IActionResult XML()
		{
			var product = _context.Products.FirstOrDefault(p => p.Id == 2021);
			XmlSerializer xs = new XmlSerializer(typeof(Product));
			StreamWriter stream = new StreamWriter(Directory.GetCurrentDirectory() + FileRoots.LogsRoot + "\\" + "product.xml", true);
			
			try
			{
				xs.Serialize(stream, product);
			}
			catch (Exception ex)
			{
				stream.WriteLine(ex.Message);
			}
			stream.Close();
			StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + FileRoots.LogsRoot + "\\" + "product.xml");
			var res = xs.Deserialize(streamReader);
			streamReader.Close();
			return View("Timer", res);

		}
	}
}
