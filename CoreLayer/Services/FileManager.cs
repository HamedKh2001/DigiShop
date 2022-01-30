using CORETest.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace CoreLayer.Services.FileManager
{
	public class FileManager : IFileManager
	{
		public string SaveFile(IFormFile File, string SavePath)
		{
			var FileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
			if (SavePath.StartsWith('/'))
			{
				SavePath = SavePath.Substring(1, SavePath.Length-1);
			}
			var folderpath = Path.Combine(Directory.GetCurrentDirectory(), SavePath.Replace("/", "\\"));
			if (!Directory.Exists(folderpath))
			{
				Directory.CreateDirectory(folderpath);
			}
			var fullpath = Path.Combine(folderpath, FileName);
			using var stream = new FileStream(fullpath, FileMode.Create);
			File.CopyTo(stream);
			return FileName;
		}
		public List<string> SaveListFile(List<IFormFile> Files, string SavePath)

		{
			List<string> filenames = new List<string>();
			foreach (var File in Files)
			{
				var FileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
				var folderpath = Path.Combine(Directory.GetCurrentDirectory(), SavePath.Replace("/", "\\"));
				if (!Directory.Exists(folderpath))
				{
					Directory.CreateDirectory(folderpath);
				}
				var fullpath = Path.Combine(folderpath, FileName);
				using var stream = new FileStream(fullpath, FileMode.Create);
				File.CopyTo(stream);
				filenames.Add(FileName);
			}
			return filenames;
		}
		public FormFile GetFormFile(string fileName, string filePath)
		{
			using (var stream = File.OpenRead(filePath))
			{
				return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
			}
		}
		public OperationResault DeleteFile(string filePath, string fileName)
		{
			try
			{
				File.Delete(filePath + fileName);
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
	}
}
