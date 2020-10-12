using Electrolux.ShopFloor.Middleware.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Electrolux.ShopFloor.iOS.Services.Providers
{
	public class FileStorageProvider : IFileStorage
	{
		public Task<byte[]> LoadBinaryAsync(string filename)
		{
			var task = new Task<byte[]>(() => 
			                            File.ReadAllBytes(filename)
			                           );
			task.Start();
			return task;
		}

		public Task<string> LoadTextAsync(string filename)
		{
			var task = new Task<string>(() => File.ReadAllText(filename));
			task.Start();
			return task;
		}

		public Task SaveBinaryAsync(string filename, byte[] contents)
		{
			var task = new Task(() => { File.WriteAllBytes(filename, contents); });
			task.Start();
			return task;
		}

		public Task SaveTextAsync(string filename, string contents)
		{
			var task = new Task(() => { File.WriteAllText(filename, contents); });
			task.Start();
			return task;
		}

		public Task CopyFileAsync(string source, string destination)
		{
			var task = new Task(() => { File.Copy(source, destination, true); });
			task.Start();
			return task;
		}

		public Task MoveFileAsync(string source, string destination)
		{
			var task = new Task(() =>
			{
				if (File.Exists(destination))
				{
					File.Delete(destination);
				}
				if (File.Exists(source))
				{
					File.Move(source, destination);
				}
			});
			task.Start();
			return task;
		}

		public Task DeleteFileAsync(string path)
		{
			var task = new Task(() =>
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			});
			task.Start();
			return task;
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public void CreateFile(string path)
		{
			if (!this.FileExists(path))
			{
				var fs = File.Create(path);
				fs.Dispose();
			}
		}

		public bool DirectoryExist(string path)
		{
			return Directory.Exists(path);
		}

		public void DirectoryCreate(string path)
		{
			if (!this.DirectoryExist(path))
			{
				Directory.CreateDirectory(path);
			}
		}


		public string GetUniqueFilenameInFolder(string path, string fileExtension)
		{
			string maskFilename = "{0}." + fileExtension;

			string uniqueFilename = String.Format(maskFilename, Guid.NewGuid());

			while (File.Exists(Path.Combine(path, uniqueFilename)))
			{
				uniqueFilename = String.Format(maskFilename, Guid.NewGuid());
			}

			return Path.Combine(path, uniqueFilename);
		}

		#region ctor

		public static FileStorageProvider Instance { get; } = new FileStorageProvider();

		private FileStorageProvider()
		{
		}

		#endregion
	}
}
