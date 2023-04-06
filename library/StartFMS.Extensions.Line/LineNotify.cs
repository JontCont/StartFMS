using isRock.LineNotify;

namespace StartFMS.Extensions.Line
{
    public class LineNotify : IDisposable
    {
        public string ChannelToken { get; set; }
        public string DeveloperToken { get; set; }
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

        public string GetNotifyUrl()
        {
            string resultUrl = urlRequest.url;
            List<string> resultData = new();
            var props = typeof(UrlRequest).GetProperties();
            foreach (var prop in props)
            {
                string paramName = prop.Name;
                var paramValue = prop.GetValue(urlRequest);
                if (paramValue == null || paramName == "url") { continue; }
                resultData.Add(paramName + "=" + paramValue.ToString());
            }
            return resultUrl + "?" + string.Join("&", resultData);
        }

        public void DeveloperSend(string text)
        {
            Utility.SendNotify(DeveloperToken, text);
        }

        public void DeveloperSend(string text, Uri imageThumbnail, Uri imageFullsize, bool notificationDisabled = false)
        {
            Utility.SendNotify(DeveloperToken, text, imageThumbnail, imageFullsize, 0 , 0 ,notificationDisabled);
        }

        public void DeveloperSend(string text, int stickerPackageId, int stickerId, bool notificationDisabled = false)
        {
            Utility.SendNotify(DeveloperToken, text,null,null, stickerPackageId, stickerId, notificationDisabled);
        }


        public isRock.LineNotify.GetTokenFromCodeResult? GetTokenFromCode(string code)
        {
            var token = isRock.LineNotify.Utility.GetTokenFromCode(code,
              urlRequest.client_id,
              ChannelToken,
              urlRequest.redirect_uri);
            return token;
        }

        public void Send(string text,string accessToken)
        {
            Utility.SendNotify(accessToken, text);
        }

        public void Send(string text, string accessToken, Uri imageThumbnail, Uri imageFullsize, bool notificationDisabled = false)
        {
            Utility.SendNotify(accessToken, text, imageThumbnail, imageFullsize, 0, 0, notificationDisabled);
        }

        public void Send(string text, string accessToken, int stickerPackageId, int stickerId, bool notificationDisabled = false)
        {
            Utility.SendNotify(accessToken, text, null, null, stickerPackageId, stickerId, notificationDisabled);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
