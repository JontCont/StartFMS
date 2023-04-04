using isRock.LineBot;
using StartFMS.Extensions.Data;
using System.Linq;

namespace StartFMS.Extensions.Line;
public class LineBots :IDisposable
{
    // Public 設定檔
    public string ChannelToken { get;set; }
    public string AdminUserID { get; set; }
    public string ReplyUserID { get;set; }
    public Stream STREAM { get; private set; }
    public LineReceived LineReceived { get; set; }

    // Private 設定檔
    private Bot LINE_BOT { get; set; }
    private string ADMIN_TOKEN_ID { get; set; }

    public LineBots Load()
    {
        LINE_BOT = new Bot(ChannelToken);
        ADMIN_TOKEN_ID = AdminUserID;
        return this;
    }

    public async Task<LineBots> LoadAsync(Stream stream)
    {
        LINE_BOT = new Bot(ChannelToken);
        ADMIN_TOKEN_ID = AdminUserID;
        STREAM = stream;

        // 確認 Post 內容
        try
        {
            //取得 http Post 
            using (StreamReader reader = new(STREAM, System.Text.Encoding.UTF8))
            {
                string strBody = await reader.ReadToEndAsync();
                if (reader == null || string.IsNullOrEmpty(strBody))
                    throw new ArgumentNullException("Mandatory parameter", nameof(strBody)); ;
                LineReceived = (LineReceived)Utility.Parsing(strBody);
                ReplyUserID = LineReceived.events.FirstOrDefault()!=null?
                    LineReceived.events.FirstOrDefault().replyToken : "";
            }
        }
        catch (Exception ex)
        {
            LINE_BOT.PushMessage(ADMIN_TOKEN_ID, ex.Message);
        }
        return this;
    }

    public virtual void ExecuteReader()
    {
        if (LineReceived == null)
        {
            throw new ArgumentNullException("Error : No correct response yet. (LineReceived is undefined)");
        }
        var LineEvent = LineReceived.events.FirstOrDefault();
        LineType value;
        if (LineEvent == null || !Enum.TryParse(LineEvent.type.ToCapitalizeFirstLetter(), out value))
        {
            throw new ArgumentNullException("Error : Null value or incorrectly passed value.");
        }

        switch (value)
        {
            case LineType.Join:
                Join();
                break;
            case LineType.Message:
                Message(LineEvent.message.type);
                break;
        }
    }

    public virtual void Join() { }

    public virtual void Message(string messageType)
    {
        LineMessageType value;
        if (string.IsNullOrEmpty(messageType) || !Enum.TryParse(messageType.ToCapitalizeFirstLetter(), out value))
        {
            throw new ArgumentNullException("Error : Null value or incorrectly passed value.");
        }

        switch (value)
        {
            case LineMessageType.Text:
                MessageText(); break;
            case LineMessageType.Image:
                MessageImage(); break;
            case LineMessageType.Video:
                MessageVideo(); break;
            case LineMessageType.Audio:
                MessageAudio(); break;
            case LineMessageType.Sticker:
                MessageSticker(); break;
            case LineMessageType.Location:
                MessageLocation(); break;
        }
    }

    public virtual void MessageText() { }

    public virtual void MessageImage() { }

    public virtual void MessageVideo() { }

    public virtual void MessageAudio() { }

    public virtual void MessageSticker() { }

    public virtual void MessageLocation() { }


    //-------------  Push Message ------------------//
    /// <summary>
    /// 推送訊息 (請參閱 LineAPI 規則)
    /// </summary>
    /// <param name="message"></param>
    public void PushMessage(string message)
    {
        var result = LINE_BOT.PushMessage(ADMIN_TOKEN_ID, message);
    }

    /// <summary>
    /// 推送訊息 (請參閱 LineAPI 規則)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tokenId"></param>
    public void PushMessage(string message, string tokenId)
    {
        var result = LINE_BOT.PushMessage(tokenId, message);
    }

    /// <summary>
    /// 推送訊息 (請參閱 LineAPI 規則)
    /// </summary>
    /// <param name="message"></param>
    public void PushMessage(TextMessage message)
    {
        var result = LINE_BOT.PushMessage(ADMIN_TOKEN_ID, message);
    }

    /// <summary>
    /// 推送訊息 (請參閱 LineAPI 規則)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tokenId"></param>
    public void PushMessage(TextMessage message, string tokenId)
    {
        var result = LINE_BOT.PushMessage(tokenId, message);
    }

    // --------------- Reply Message  ------------------//
    /// <summary>
    /// 回覆訊息 (請參閱 LineAPI 規則)
    /// </summary>
    /// <param name="message"></param>
    public void ReplyMessage(string message)
    {
        var result = LINE_BOT.ReplyMessage(ReplyUserID, message);
    }

    /// <summary>
    /// 回覆訊息 (請參閱 LineAPI 規則)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tokenId"></param>
    public void ReplyMessage(string message, string tokenId)
    {
        var result = LINE_BOT.ReplyMessage(tokenId, message);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}// LineConnection
