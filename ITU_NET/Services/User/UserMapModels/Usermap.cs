using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ITU_NET
{
    public partial class Usermap {

        [JsonProperty("nickname")] public string Nickname { get; set; }

        [JsonProperty("password")] public string Password { get; set; }

        [JsonProperty("desc")] public string Desc { get; set; }

        [JsonProperty("finished")] public List<string> Finished { get; set; }

        [JsonProperty("favourite")] public List<string> Favourite { get; set; }
    }

    public partial class Usermap
    {
        public static Usermap FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Usermap>(json, Converter.Settings);
        }
        public Usermap(){

        }
        public Usermap(Credentials c) {
            Nickname = c.Nickname.Substring(1);
            Password = c.Password;
            Desc = "";
            Finished = new List<string>();
            Favourite = new List<string>();
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }
    }
}