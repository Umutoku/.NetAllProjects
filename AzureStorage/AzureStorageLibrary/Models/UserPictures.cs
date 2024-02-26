using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureStorageLibrary.Models
{
    public class UserPictures : ITableEntity
    {
        public string PartitionKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RowKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset? Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ETag ETag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RawPaths { get; set; } // blobların url'lerini tutar
        public string WatermarkRawPaths { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<string> Paths {
            get => JsonConvert.DeserializeObject<List<string>>(RawPaths);
            set => RawPaths = JsonConvert.SerializeObject(value);
        } // blobların url'lerini listeye çevirir
        [System.Text.Json.Serialization.JsonIgnore]
        public List<string> WatermarkPaths
        {
            get => JsonConvert.DeserializeObject<List<string>>(WatermarkRawPaths);
            set => WatermarkRawPaths = JsonConvert.SerializeObject(value);
        }
    }
}
