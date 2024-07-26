using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PleasureCare.Data
{
    public class APIRef
    {
        public class DataBody
        {
            [JsonProperty("食品名")]
            public string foodName { get; set; }
            [JsonProperty("たんぱく質")]
            public double protain { get; set; }
            [JsonProperty("食物繊維総量")]
            public double shokumotsusenni { get; set; }
            [JsonProperty("ビタミンB6")]
            public double vitaminBs { get; set; }
            [JsonProperty("ビタミンB12")]
            public double vitaminBtwe { get; set; }
            [JsonProperty("鉄")]
            public double iron { get; set; }
            [JsonProperty("亜鉛")]
            public double aen { get; set; }
            [JsonProperty("カルシウム")]
            public double carushium { get; set; }
            [JsonProperty("食塩相当量")]
            public double solt { get; set; }
        }

        public class FoodsAPI
        {
            public List<DataBody> DataBody { get; set; }
            public AccountService.RespInfo RespInfo { get; set; }
        }
    }
}
