using System.Text.RegularExpressions;
using game.renderer;

namespace game.renderer.DotLang;

public class Compiler
{
    private int __default__width;
    private int __default__height;

    public void CreateMenuGUI(string code)
    {
        string[] lines = code.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            Process(line.Trim());
        }
    }

    void Process(string code)
    {
        Game game = new();

        __default__width = 800;
        __default__height = 600;

        if (code.Contains("init"))
        {
            if (code.Contains("menu"))
            {
                game.initializeMenu(__default__width, __default__height, "ZEA Multiplayer");
            }  
        }
        else if (code.Contains("create"))
        {
            if (code.Contains("text"))
            {
                if (code.Contains("title"))
                {
                    if (code.Contains("default"))
                    {
                        Console.WriteLine("Default text");
                    }
                    else
                    {
                        string titleText = ExtractCustomText(code, "title");

                        if (!string.IsNullOrEmpty(titleText))
                        {
                            Console.WriteLine($"Title text: {titleText}");
                        }
                    }
                }
            }
            else if (code.Contains("input"))
            {
                if (code.Contains("email"))
                {

                }
                else if (code.Contains("password"))
                {

                }
            }
        }
        else if (code.Contains("set"))
        {
            if (code.Contains("text"))
            {
                if (code.Contains("title"))
                {

                }
            }
            else if (code.Contains("input"))
            {
                if (code.Contains("email"))
                {

                }
                else if (code.Contains("password"))
                {

                }
            }
        }
    }

    private string ExtractCustomText(string code, string keyword)
    {
        var match = Regex.Match(code, $@"(?<={keyword}\s)(\S+)");
        return match.Success ? match.Value : string.Empty;
    }
}