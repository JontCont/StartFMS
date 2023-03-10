# StartFMS.Extensions.Data
StartFMS.Extensions.Data 目的字串轉換資料型態、Model 預設值以及傳值所用，支持 .NET 系列。

## 字串轉換
提供 int 、Double、float、datetime等轉換資料型態，範例如下

```cs
int Number = "100800".ToInt();
DateTime dt = "2023/3/10".ToDateTime();
```

## 千分位、小數點字串
```cs
//千分位轉換
string thousandths = "10000".ToThousandths();

// 小數點
string places = "10000".ToDecimalPlaces(6);
```

## 自動累加數字
```cs
string num = "0000".ToAutoNumber();
string num1 = "0001".ToNumber(5); //output : 00001
```

## Model 預設值
```cs
//預設 class 內屬性預設值
var mods = new Class1(){}.ToDefaultValue();

//傳入 mods 值
var mods2 = new Class1(){}.ToValue(mods);
```
