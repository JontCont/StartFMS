using isRock.LineLoginV1;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFMS.Extensions.Line
{
    public class LineLogin : IDisposable
    {
        public string ChannelToken { get; set; }
        public string AdminUserID { get; set; }
        public UrlRequest urlRequest { get; set; }

        public class UrlRequest
        {
            public string url { set; get; }
            public string response_type { get; set; }
            public string client_id { get; set; }
            public string redirect_uri { get; set; }
            public string scope { get; set; }
            public string state { get; set; }
        }

        public string GetLoginUrl()
        {
            string resultUrl = urlRequest.url;
            List<string> resultData = new();
            var props = typeof(UrlRequest).GetProperties();
            foreach (var prop in props)
            {
                string paramName = prop.Name;
                var paramValue = prop.GetValue(urlRequest);
                if (paramValue == null && paramName == "url") { continue; }
                resultData.Add(paramName + "=" + paramValue.ToString());
            }
            return resultUrl + "?" + string.Join("&", resultData);
        }

        public isRock.LineLoginV21.GetTokenFromCodeResult? GetTokenFromCode(string code)
        {
            var token = isRock.LineLoginV21.Utility.GetTokenFromCode(code,
              urlRequest.client_id,
              ChannelToken,
              urlRequest.redirect_uri);
            return token;
        }

        public isRock.LineLoginV21.Profile? GetUserProfile(string accessToken)
        {
            var profile = isRock.LineLoginV21.Utility.GetUserProfile(accessToken);
            return profile;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
