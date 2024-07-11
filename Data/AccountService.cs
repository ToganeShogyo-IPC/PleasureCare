using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SmileCare.Data
{
    public class AccountService
    {
        public class AccountBody
        {
            public static string mail { get; set; }
            public static string pass { get; set; }
        }
        
        /// <summary>
        /// ログイン用アカウントデータボディ
        /// </summary>
        public class LogAccDataBody
        {
            public string? uuid { get; set; }
            public string? mail { get; set; }
            public string? name { get; set; }
            public string? linktoken { get; set; }
        }
        /// <summary>
        /// レスポンス情報
        /// </summary>
        public class RespInfo
        {
            public string HTTPStateCode { get; set; }
            public int Code { get; set; }
            public string State { get; set; }
        }
        /// <summary>
        /// ログイン用要素ルート 
        /// </summary>
        public class LogAccRoot
        {
            [JsonPropertyName("DataBody")]
            public List<LogAccDataBody> LogAccDataBody { get; set; }
            public RespInfo RespInfo { get; set; }
        }
        /// <summary>
        /// 認証ID返却要素ボディ
        /// </summary>
        public class AuthIDDataBody
        {
            public string linktoken { get; set; }
            public string authid { get; set; }
        }
        /// <summary>
        /// 認証ID作成用要素ルート
        /// </summary>
        public class AuthIDRoot
        {
            [JsonPropertyName("DataBody")]
            public AuthIDDataBody AuthIDDataBody { get; set; }
            public RespInfo RespInfo { get; set; }
        }

        private readonly HttpClient client = new HttpClient();
        /// <summary>
        /// アカウントログイン作業(起動時・通常ログイン時共通)
        /// 起動時だと認証ID生成の手順を吹っ飛ばして通常の認証に飛びまｓ。
        /// </summary>
        /// <param name="requestBody">リクエストで飛ばすやつ</param>
        /// <param name="is_startup">起動時の何かか否か</param>
        public async void LoginAccount(dynamic requestBody, bool is_startup)
        {
            string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "userinfo.json");
            string authid = "";
            if (!is_startup)
            {
                HttpContent reqBody = new FormUrlEncodedContent(requestBody);
                HttpResponseMessage _resp = await client.PostAsync("https://api.schnetworks.net/v1/auth.php?type=login", reqBody);
                string json = await _resp.Content.ReadAsStringAsync();
                var js_l = JsonConvert.DeserializeObject<AuthIDRoot>(json);
                authid = js_l.AuthIDDataBody.authid;
            }
            var resp = await client.GetStringAsync($"https://api.schnetworks.net/v1/auth.php?type=login&authid={authid}");
            var json1 = JsonConvert.DeserializeObject<LogAccRoot>(resp);
            if (json1.RespInfo.Code == 200)
            {
                
            }
        }

        public async void LogoutAccount(string authid)
        {
            var resp = await client.DeleteAsync($"https://api.schnetworks.net/v1/auth.php?type=logout&authid={authid}");
        }

        public void GetAccountState(string uuid = null)
        {

        }
    }
}