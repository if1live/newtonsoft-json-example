using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;

[Serializable]
class ReferenceData<T>
{
    public T x;
}

public class ReferenceTests<T>
{
    class Data : ReferenceData<T> { }

    protected string json_exist = "";
    protected T json_value = default(T);

    string json_null = @"{'x': null}";
    string json_undefined = @"{}";

    [Test]
    public void Test_Exists()
    {
        var data = JsonConvert.DeserializeObject<Data>(json_exist);
        Assert.AreEqual(data.x, json_value);
    }

    [Test]
    public void Test_Null()
    {
        var data = JsonConvert.DeserializeObject<Data>(json_null);
        Assert.AreEqual(data.x, null);
    }

    [Test]
    public void Test_Undefiend()
    {
        var data = JsonConvert.DeserializeObject<Data>(json_undefined);
        Assert.AreEqual(data.x, null);
    }
}

public class StringTests : ReferenceTests<string>
{
    [SetUp]
    public void Init()
    {
        this.json_exist = @"{'x': 'hello'}";
        this.json_value = "hello";
    }
}

public class ListTests : ReferenceTests<List<string>>
{
    [SetUp]
    public void Init()
    {
        this.json_exist = @"{'x': ['a', 'b']}";
        this.json_value = new List<string>() { "a", "b" };
    }
}
