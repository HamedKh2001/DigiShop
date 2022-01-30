using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
	public static class ExtentionLoger
	{
		public static void Log(this Exception ex)
		{
			string filename = DateTime.Now.ToShamsi().Replace("/", "-") + ".txt";
			//string text = ex.Message + DateTime.Now.ToShortTimeString() + "\n-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_\n";
			//File.AppendAllText(Directory.GetCurrentDirectory() + FileRoots.LogsRoot + "\\" + filename, text);
			//StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + FileRoots.LogsRoot + "\\" + filename);
			//string per = streamReader.ReadToEnd();
			//streamReader.Close();
			StreamWriter treamWriter = new StreamWriter(Directory.GetCurrentDirectory() + FileRoots.LogsRoot + "\\" + filename, true);
			treamWriter.WriteLine(ex.Message + $"\t({DateTime.Now.ToShortTimeString()})");
			treamWriter.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
			treamWriter.Close();
		}
	}
}
