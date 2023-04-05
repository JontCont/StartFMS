using isRock.LineNotify;

namespace StartFMS.Extensions.Line
{
    public class LineNotify : IDisposable
    {
        public string ChannelToken { get; set; }

        public void Send(string text)
        {
            Utility.SendNotify(ChannelToken, text);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
