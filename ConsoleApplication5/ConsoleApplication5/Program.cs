using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.OpenText("Data.txt");
            var value = file.ReadToEnd();
            JsonSerializerSettings settings = new JsonSerializerSettings();

            


            var obj = JsonConvert.DeserializeObject<FilePathCollection>(value);


        }
    }

    public class FilePathEntity
    {
        [JsonProperty(PropertyName = "url")]
        public string FilePath { get; set; }

        [JsonProperty(PropertyName = "file")]
        public bool file { get; set; }

        //[JsonProperty(PropertyName = "modifyTime")]
        //public DateTime UpdateTime { get; set; }
    }

    public class FilePathCollection
    {
        public List<FilePathEntity> PathAndFileBean { get; set; }
    }
}
