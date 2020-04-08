using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public static class JsonConfig<T>
{
    public static List<T> LoadConfig(string fileName)
    {
        using (var r = new StreamReader(fileName))
        {
            var json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }

}
