using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageLibrary
{
    public enum EContainerName
    {
        Pictures,
        Documents,
        Log,
        Watermark
    }
    public interface IBlobStorage
    {
        public string BlobUrl { get;} 
        Task UploadAsync(Stream fileStream, string name, EContainerName eContainerName);
        Task<Stream> DownloadAsync(string name, EContainerName eContainerName);
        Task DeleteAsync(string name, EContainerName eContainerName);
        Task SetLogAsync(string text, string name);
        Task<List<string>> GetLogAsync(string name);
        Task<List<string>> GetName(EContainerName eContainerName);
    }
}
