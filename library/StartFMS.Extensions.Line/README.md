# StartFMS.Extensions.Line
這是由LineBotSDK擴充出來的工具，減少開發困難度。

## 前置作業
第一步、新創一個類別
```cs
// Helper/LineBot.cs
using StartFMS.Extensions.Line;
namespace StartFMS.Partner.API.Helper
{
    public class LineBot:LineBots
    {
        public override void MessageText()
        {
            var @event = ReceivedMessage.events.FirstOrDefault();
            string message = @event!=null ? @event.message.text:"";
            ReplyMessage(message);
        }
    }
}
```

第二步、 DI 設定
Program.cs 加入以下設定

```cs
// Program.cs 
var lineBots = new LineBot() {
    ChannelToken = config.GetValue<string>("Line:Bots:channelToken"),
    AdminUserID = config.GetValue<string>("Line:Bots:adminUserID")
};
builder.Services.AddSingleton<LineBot>(lineBots);
```

第三步、加入Controllers
```
    [HttpPost("", Name = "Message Reply")]
    public async Task<string> Post() {

        try
        {
            //載入 Line BOT 
            using (var linebot = await _lineBots.LoadAsync(Request.Body))
            {
                //執行內容
                linebot.ExecuteReader(); //改內容修要透過覆寫方式修改內容
            }

            return JsonConvert.SerializeObject(new
            {
                Success = true,
                Message = "",
            });
        }
        catch(Exception ex) {
            return JsonConvert.SerializeObject(new
            {
                Success = false,
                Message = ex.Message,
            });
        }

    }
```



## Behavior method
行為 Function 有 "Join"、"Message"這兩個，使用方式如下。

### a. Join()
加入機器人之後Bot動作行為。
```cs
public override void Join() { }
```

### b.Message()
如果是第一次使用以下 Function 不建議直接修改，本擴充有把 Message 回傳類別特別回傳到指定位置。
```cs
public override void Message() { }
```


## Message method 
```cs
//文字
public override void MessageText() { }

//圖檔
public override void MessageImage() { }

//影片
public override void MessageVideo() { }

//聲音
public override void MessageAudio() { }

//貼圖
public override void MessageSticker() { }

//地圖
public override void MessageLocation() { }
```


## 更新內容
1.0.1 
- 修正 依賴 LineBotSDK問題
