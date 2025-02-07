using Microsoft.AspNetCore.Http;

namespace Bookle.BL.Extentions;

public static class FileExtention
{
	public static bool IsValidType(this IFormFile file, string type)
	   => file.ContentType.StartsWith(type);

	public static bool IsValidSize(this IFormFile file, int kb)
	{
		return file.Length <= kb * 1024;
	}

	public static async Task<string> UploadAsync(this IFormFile file, params string[]  paths)
	{
		string uploadPath = Path.Combine(paths);
		if (!Path.Exists(uploadPath))
		{
			Directory.CreateDirectory(uploadPath);
		}
		string newFilename = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
		string fullPath = Path.Combine(uploadPath, newFilename);

		using (Stream st = File.Create(fullPath))
		{
			await file.CopyToAsync(st);
		}

		return newFilename;
	}
}
