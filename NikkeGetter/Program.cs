using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Data.Common;
using System.Net;
using System.Text.RegularExpressions;
using TABBotApp.Libs.Utils;

var baseUrl = "https://wiki3.jp/nikke/page/14";
var htmlText = await HttpHelper.GetRequestAsync(baseUrl);


var regex = MyRegex();
var lines = htmlText.Split('\r', '\n').Where(e=> e.Contains("_Icon.png")).Select(e => regex.Replace(e, "$1"));

File.WriteAllLines("tmp.txt", lines);

var images = new List<string>();

foreach (var url in lines)
{
    var charHtmlText = await HttpHelper.GetRequestAsync(url);
    var charRegex = MyRegex1();
    var matches = charRegex.Matches(charHtmlText);
    images.AddRange(matches.Select(e=> ("https:"+e.Value).Replace(@"https://img.wiki3.jp/nikke/", string.Empty)));
}


var imageList = images.ToHashSet().OrderBy(e => e).ToArray();

File.WriteAllLines("tmp2.txt", imageList);

var dst = Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "out"));

foreach(var url in imageList)
{
    try
    {
        var name = Path.GetFileName(url);
        var savePath = Path.Combine(dst.FullName, name);
        var encodedUrl = Uri.EscapeDataString(url);
        var data = await HttpHelper.GetRequestByteArrayAsync(@"https://img.wiki3.jp/nikke/" + encodedUrl);
        File.WriteAllBytes(savePath, data);
        Console.WriteLine($"saved. [{savePath}]");
    }
    catch(Exception ex) 
    {
        Console.WriteLine(ex.Message);
    }
}


Console.WriteLine("press any key to exit . . .");
Console.ReadKey(true);
Environment.Exit(0);

partial class Program
{
    [GeneratedRegex(@"^.*?href=""(.*?)"".*$")]
    private static partial Regex MyRegex();
}

partial class Program
{
    [GeneratedRegex(@"//img\.wiki3\.jp/nikke/.*?\.png")]
    private static partial Regex MyRegex1();
}