using Microsoft.Extensions.FileProviders;

namespace webapiasp.Utility
{
    
    public interface StorageService
    {
        List<string> LoadAll();
        string Store(IFormFile file);
        IFileInfo Load(string fileName);
        void Delete(string fileName);
    }
}
