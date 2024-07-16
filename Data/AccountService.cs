using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace SmileCare.Data
{
    public class ShareValues
    {
        public AccountService.LogAccDataBody value { get; set;}
        public int ErrorStatus { get; set; }

        public void BrokeSession() => value = new AccountService.LogAccDataBody();
    }
    
    public class AccountService
    {
        public class DelAccountAuthCode
        {
            public List<string> DataBody { get; set; }
            public RespInfo RespInfo { get; set; }
        }

        public class AccountLoginBody
        {
            public string mail { get; set; }
            public string pass { get; set; }
        }
        
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
            [JsonProperty("DataBody")]
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
            [JsonProperty("DataBody")]
            public required AuthIDDataBody AuthIDDataBody { get; set; }
            public required RespInfo RespInfo { get; set; }
        }

        private static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// アカウントログイン作業(起動時・通常ログイン時共通)
        /// 起動時だと認証ID生成の手順を吹っ飛ばして通常の認証に飛びまｓ。
        /// </summary>
        /// <param name="requestBody">リクエストで飛ばすやつ</param>
        /// <param name="is_startup">起動時の何かか否か</param>
        public static async Task<LogAccRoot>? LoginAccount(dynamic requestBody = null, bool is_startup=false)
        {
            var authid = LoSaSettings("authid");
            if (authid == null && !is_startup) // authidがnull かつ 起動時処理ではない
            {
                Dictionary<string, string> requestbodies = new Dictionary<string, string>()
                {
                    {"mail", requestBody.mail},
                    {"pass", requestBody.pass}
                };
                HttpContent reqBody = new FormUrlEncodedContent(requestbodies);
                HttpResponseMessage resp = await client.PostAsync("https://api.schnetworks.net/v1/auth.php?type=login", reqBody).ConfigureAwait(false);
                string json = await resp.Content.ReadAsStringAsync();
                var js_l = JsonConvert.DeserializeObject<AuthIDRoot>(json);
                try {
                    authid = js_l.AuthIDDataBody.authid; // 認証用id生成終了
                    AccountBody acb = new AccountBody(); // ファイル書き込み用クラス生成
                    acb.authid = authid;
                    if (js_l.RespInfo.Code < 300 && js_l.RespInfo.Code >= 200) //200番台の場合
                    {
                        LoSaSettings("authid", true, acb); // 保存処理
                        HttpResponseMessage LgResp = await client.GetAsync($"https://api.schnetworks.net/v1/auth.php?type=login&authid={authid}").ConfigureAwait(false);
                        string Lgjson = await LgResp.Content.ReadAsStringAsync();
                        var Lgjs_l = JsonConvert.DeserializeObject<LogAccRoot>(Lgjson);
                        if (Lgjs_l.RespInfo.Code < 300 && Lgjs_l.RespInfo.Code >= 200)
                        {
                            return Lgjs_l;
                        }
                    }
                    else if (((int)resp.StatusCode) < 500 && ((int)resp.StatusCode) >= 400)
                    {
                        return null;
                    }
                }
                catch
                {
                    if((int)resp.StatusCode == 404)
                    {
                        LogAccRoot lg = new LogAccRoot();
                        RespInfo respInfo = new RespInfo()
                        {
                            Code = (int)resp.StatusCode,
                        };
                        lg.RespInfo= respInfo;
                        return lg;
                    }
                    if((int)resp.StatusCode == 409)
                    {
                        LogAccRoot lg = new LogAccRoot();
                        RespInfo respInfo = new RespInfo()
                        {
                            Code = (int)resp.StatusCode,
                        };
                        lg.RespInfo = respInfo;
                        return lg;
                    }
                }
            }
            else if (is_startup && authid != null)
            {
                HttpResponseMessage resp = await client.GetAsync($"https://api.schnetworks.net/v1/auth.php?type=login&authid={authid.authid}").ConfigureAwait(false);
                string json = await resp.Content.ReadAsStringAsync();
                var js_l = JsonConvert.DeserializeObject<LogAccRoot>(json);
                if (js_l.RespInfo.Code < 300 && js_l.RespInfo.Code >= 200)
                {
                    return js_l;
                }
            }
            return new LogAccRoot();
        }

        public static async Task<String> LogoutAccount(string authid = null,bool is_local=false)
        {
            if (!is_local)
            {
                HttpResponseMessage resp = await client.DeleteAsync($"https://api.schnetworks.net/v1/auth.php?type=logout&authid={authid}").ConfigureAwait(false);
                string json = await resp.Content.ReadAsStringAsync();
                DelAccountAuthCode? js_l = JsonConvert.DeserializeObject<DelAccountAuthCode>(json);
                Console.WriteLine(js_l.RespInfo.Code);
                if (js_l.RespInfo.Code == 200)
                {
                    LoSaSettings("authid", true, null);
                    return "OK";
                }
                return $"Error: {js_l.RespInfo.HTTPStateCode}";
            }
            else if (is_local)
            {
                LoSaSettings("authid", true, null);
                return null;
            }
            return null;
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
                            if(otherhikisu == null)
                            {
                                File.Delete(filePath);
                            }
                            else {
                                AccountBody aby = otherhikisu;
                                aby.authid = aby.authid;
                                string take = JsonConvert.SerializeObject(aby);
                                File.WriteAllText(filePath, take);
                            }
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
                            return null;
                        }

                        return null;
                    }
                    break;
            }

            return null;
        }
    }
}