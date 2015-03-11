using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class propertyBag : ISerializable
{
    Dictionary<string, string> dict = new Dictionary<string, string>();
    public propertyBag() { }
    public propertyBag(Dictionary<string, string> dic)
    {
        this.dict = dic;
    }
    protected propertyBag(SerializationInfo info, StreamingContext context)
    {
        foreach (var entry in info)
        {
            dict.Add(entry.Name, entry.Value.ToString());
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        foreach (string key in dict.Keys)
        {
            info.AddValue(key, dict[key], dict[key] == null ? typeof(object) : dict[key].GetType());
        }
    }

    public void Add(string key, string value)
    {
        dict.Add(key, value);
    }

    public bool isKey(string key)
    {
        return dict.ContainsKey(key);
    }


    public string getValue(string key)
    {
        return this.dict[key];
    }

    
    public Dictionary<string, string> fromJsonDic()
    {
        return this.dict;
    }

    public static explicit operator propertyBag(Dictionary<string, string> dic)
    {
        return new propertyBag(dic);
    }
}