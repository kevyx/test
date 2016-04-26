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

            string content = "1,2,3,\"hello, world\",4,3,2\",5";
            var list = GetContent(content);
            var list1 = SplitStringWithComma(content);
        }

        static string[] GetContent(string content)
        {
            var list = content.Split(new string[] { "," }, StringSplitOptions.None);



            return list;
        }

        private static string[] SplitStringWithComma(string splitStr)
        {
            var newstr = string.Empty;
            List<string> sList = new List<string>();

            bool isSplice = false;
            string[] array = splitStr.Split(new char[] { ',' });
            foreach (var str in array)
            {
                if (!string.IsNullOrEmpty(str) && str.IndexOf('"') > -1)
                {
                    var firstchar = str.Substring(0, 1);
                    var lastchar = string.Empty;
                    if (str.Length > 0)
                    {
                        lastchar = str.Substring(str.Length - 1, 1);
                    }
                    if (firstchar.Equals("\"") && !lastchar.Equals("\""))
                    {
                        isSplice = true;
                    }
                    if (lastchar.Equals("\""))
                    {
                        if (!isSplice)
                            newstr += str;
                        else
                            newstr = newstr + "," + str;

                        isSplice = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                }

                if (isSplice)
                {
                    //添加因拆分时丢失的逗号
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                    else
                        newstr = newstr + "," + str;
                }
                else
                {
                    sList.Add(newstr.Replace("\"", "").Trim());//去除字符中的双引号和首尾空格
                    newstr = string.Empty;
                }
            }
            return sList.ToArray();
        }
    }

    public class FilePathEntity
    {
        [JsonProperty(PropertyName = "url")]
        public string FilePath { get; set; }

        [JsonProperty(PropertyName = "file")]
        public bool file { get; set; }

        [JsonProperty(PropertyName = "modifyTime")]
        public long ModifyTime { get; set; }

        public DateTime UpdateTime { get { return DateTime.Parse("1970-01-01").AddMilliseconds(this.ModifyTime); } }
    }

    public class FilePathCollection
    {
        public List<FilePathEntity> PathAndFileBean { get; set; }
    }
}
