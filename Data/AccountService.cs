using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace SmileCare.Data
{
    public class AccountService
    {
        public class AccountBody
        {
            public string authid { get; set; }
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

        private static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// アカウントログイン作業(起動時・通常ログイン時共通)
        /// 起動時だと認証ID生成の手順を吹っ飛ばして通常の認証に飛びまｓ。
        /// </summary>
        /// <param name="requestBody">リクエストで飛ばすやつ</param>
        /// <param name="is_startup">起動時の何かか否か</param>
        public static async Task<LogAccRoot> LoginAccount(dynamic requestBody = null, bool is_startup=false)
        {
            var authid = LoSaSettings("authid");
            if (authid == null && !is_startup)
            {
                HttpContent reqBody = new FormUrlEncodedContent(requestBody);
                HttpResponseMessage resp = await client.PostAsync("https://api.schnetworks.net/v1/auth.php?type=login", reqBody);
                string json = await resp.Content.ReadAsStringAsync();
                var js_l = JsonConvert.DeserializeObject<AuthIDRoot>(json);
                authid = js_l.AuthIDDataBody.authid;
                AccountBody acb = new AccountBody();
                acb.authid = authid;
                if (js_l.RespInfo.Code < 300 && js_l.RespInfo.Code >= 200)
                {
                    LoSaSettings("authid", true,acb);
                    HttpResponseMessage LgResp = await client.GetAsync($"https://api.schnetworks.net/v1/auth.php?type=login&authid={authid}");
                    string Lgjson = await LgResp.Content.ReadAsStringAsync();
                    var Lgjs_l = JsonConvert.DeserializeObject<LogAccRoot>(Lgjson);
                    if (Lgjs_l.RespInfo.Code < 300 && Lgjs_l.RespInfo.Code >= 200)
                    {
                        return Lgjs_l;
                    }
                }
            }
            else if(is_startup && authid != null)
            {
                HttpResponseMessage resp = await client.GetAsync($"https://api.schnetworks.net/v1/auth.php?type=login&authid={authid.authid}");
                string json = await resp.Content.ReadAsStringAsync();
                var js_l = JsonConvert.DeserializeObject<LogAccRoot>(json);
                if (js_l.RespInfo.Code < 300 && js_l.RespInfo.Code >= 200)
                {
                    return js_l;
                }
            }
            return new LogAccRoot();
        }

        public static async Task<String> LogoutAccount(string authid)
        {
            HttpResponseMessage resp = await client.DeleteAsync($"https://api.schnetworks.net/v1/auth.php?type=logout&authid={authid}");
            string json = await resp.Content.ReadAsStringAsync();
            var js_l = JsonConvert.DeserializeObject<AuthIDRoot>(json);
            if (js_l.RespInfo.Code == 200)
            {
                LoSaSettings("auth", true, new AccountBody());
                return "OK";
            }

            return $"Error: {js_l.RespInfo.HTTPStateCode}";
        }

        public static void GetAccountState(string uuid = null)
        {

        }

        /// <summary>
        /// 設定の読み込みと保存をしたりします
        /// </summary>
        /// <param name="w_infotype">読み書きする設定の種類</param>
        /// <param name="is_save">true→保存 | false→読み込み</param>
        /// <returns></returns>
        public static dynamic LoSaSettings(string? w_infotype = null, bool is_save = false, dynamic otherhikisu = null)
        {
            switch (w_infotype)
            {
                case "authid":
                    string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "authid.json");
                    if (is_save)
                    {
                        try
                        {
                            AccountBody aby = otherhikisu;
                            aby.authid = aby.authid;
                            string take = JsonConvert.SerializeObject(aby);
                            File.WriteAllText(filePath, take);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (File.Exists(filePath))
                            {
                                string json = File.ReadAllText(filePath);
                                return JsonConvert.DeserializeObject<AccountBody>(json);
                            }
                        }
                        catch
                        {
                            return -1;
                        }

                        return null;
                    }
                    break;
            }

            return null;
        }
    }
}