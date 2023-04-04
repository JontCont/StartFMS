using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartFMS.Extensions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFMS.Extensions.Data.Tests;

public class TestClass
{
    public int number { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public byte[] data { get; set; }
    public float score { get; set; }
    public decimal age { get; set; }
    public DateTime date { get; set; }
}

[TestClass()]
public class ModelsTests
{
    [TestMethod()]
    public void Test_InitValue()
    {
        var data = new TestClass().InitValue();
        if (data == null)
        {
            throw new ArgumentException("Error : 初始化呈現微 Null");
        }
    }

    [TestMethod()]
    public void Test_SetValue()
    {
        var init = new TestClass() { 
            name = "XXX",
            age= 1,
            date = DateTime.Now,
            number = 11,
        };
        var data = new TestClass().SetValue(init);
        Assert.ReferenceEquals(data, init);
    }

    [TestMethod()]
    public void Test_轉換整數()
    {
        var result = "100".ToInt();
        Assert.AreEqual(result, 100);

    }
}
