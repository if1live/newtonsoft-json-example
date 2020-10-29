using System;
using Newtonsoft.Json;
using NUnit.Framework;

[Serializable]
class ValueData<T> where T : struct
{
    public T x;
}

[Serializable]
class NullableValueData<T> where T : struct
{
    public T? x;
}

public class ValueTests<T> where T : struct
{
    class Data : ValueData<T> { }
    class NullableData : NullableValueData<T> { }

    protected string json_exist = "";
    protected T json_value = default(T);

    string json_null = @"{'x': null}";
    string json_undefined = @"{}";

    [Test]
    public void Test_NonNullable_Exists()
    {
        var data = JsonConvert.DeserializeObject<Data>(json_exist);
        Assert.AreEqual(data.x, json_value);
    }

    [Test]
    public void Test_Nullable_Exists()
    {
        var data = JsonConvert.DeserializeObject<NullableData>(json_exist);
        Assert.AreEqual(data.x.HasValue, true);
        Assert.AreEqual(data.x.Value, json_value);
    }

    [Test]
    public void Test_NonNullable_Null()
    {
        Assert.Throws<JsonSerializationException>(delegate
        {
            JsonConvert.DeserializeObject<Data>(json_null);
        });
    }

    [Test]
    public void Test_Nullable_Null()
    {
        var data = JsonConvert.DeserializeObject<NullableData>(json_null);
        Assert.AreEqual(data.x.HasValue, false);
    }

    [Test]
    public void Test_Nullable_Undefiend()
    {
        var data = JsonConvert.DeserializeObject<Data>(json_undefined);
        Assert.AreEqual(data.x, default(T));
    }

    [Test]
    public void Test_NonNullable_Undefiend()
    {
        var data = JsonConvert.DeserializeObject<NullableData>(json_undefined);
        Assert.AreEqual(data.x.HasValue, false);
    }
}

public class IntTests : ValueTests<int>
{
    [SetUp]
    public void Init()
    {
        this.json_exist = @"{'x': 1}";
        this.json_value = 1;
    }
}

public class LongTests : ValueTests<long>
{
    [SetUp]
    public void Init()
    {
        this.json_exist = @"{'x': 1234567890123456789}";
        this.json_value = 1234567890123456789;
    }
}

public class BoolTests : ValueTests<bool>
{
    [SetUp]
    public void Init()
    {
        this.json_exist = @"{'x': true}";
        this.json_value = true;
    }
}

public class DateTests : ValueTests<DateTime>
{
    [SetUp]
    public void Init()
    {
        this.json_exist = @"{'x': '2009-02-15T00:00:00Z'}";
        this.json_value = new DateTime(2009, 2, 15, 0, 0, 0, DateTimeKind.Utc);
    }
}