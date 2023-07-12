using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace webapiasp.Utility
{
    public class StorageServiceImpl : StorageService
    {
        private readonly string _basePath;

        public StorageServiceImpl(IWebHostEnvironment env)
        {
            _basePath = Path.Combine(env.WebRootPath, "imagesfortravel");
        }
        
        public List<string> LoadAll()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_basePath);
            return dirInfo.GetFiles().Select(file => file.Name).ToList();
        }

        public string Store(IFormFile file)
        {
            string originalFileName = file.FileName;
            string ext = Path.GetExtension(originalFileName);
            string fileName = $"{Guid.NewGuid().ToString("N")}{ext}";
            string filePath = Path.Combine(_basePath, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        public IFileInfo Load(string fileName)
        {
            string filePath = Path.Combine(_basePath, fileName);
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return new PhysicalFileInfo(fileInfo);
            }

            return null;
        }

        public void Delete(string fileName)
        {
            string filePath = Path.Combine(_basePath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
