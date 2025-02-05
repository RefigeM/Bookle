using Microsoft.AspNetCore.Http;

namespace Bookle.BL.Extentions;

public static class FileExtention
{
	public static bool IsValidType(this IFormFile file, string type)
	{
		return file.ContentType.StartsWith(type);
	}
	public static bool IsValidSize(this IFormFile file, int kb)
	{
		return file.Length <= kb * 1024 * 1024;
	}
	public static async Task<string> UploadAsync(this IFormFile file, string directory)
	{
		string path = Path.Combine(directory);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		string newFilename = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
		string fullPath = Path.Combine(path, newFilename);

		using (Stream st = File.Create(fullPath))
		{
			await file.CopyToAsync(st);
		}

		return newFilename;
	}
}
