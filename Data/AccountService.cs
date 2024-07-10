namespace SmileCare.Data
{
    public class AccountService
    {
        public class AccountBody
        {
            public static string mail { get; set; }
            public static string pass { get; set; }
        }
        
        public class DataBody
        {
            public string uuid { get; set; }
            public string mail { get; set; }
            public string name { get; set; }
            public object linktoken { get; set; }
        }

        public class RespInfo
        {
            public string HTTPStateCode { get; set; }
            public int Code { get; set; }
            public string State { get; set; }
        }

        public class Root
        {
            public List<DataBody> DataBody { get; set; }
            public RespInfo RespInfo { get; set; }
        }
        
        public void LoginAccount()
        {
            string a = "a";
        }

        public void LogoutAccount()
        {

        }

        public void GetAccountState(string uuid = null)
        {

        }
    }
}