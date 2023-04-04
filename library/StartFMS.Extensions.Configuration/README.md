# StartFMS.Extensions.Configuration
透過Config 類別呼叫 appsetting.json 或是其他設定檔資料。

## 使用方式
需要用 ```Config.GetConfiguration()``` 進行取得參數內容。


## 範例
透過 Program.cs 傳入參數 
```cs
var config = Config.GetConfiguration(); //加入設定檔

var  ChannelToken = config.GetValue<string>("Line:Bots:channelToken");
var  AdminUserID = config.GetValue<string>("Line:Bots:adminUserID");
```


