using game.renderer;
using game.renderer.DotLang;

// using game.renderer.DotLang;

using System;
using System.IO;
using System.Text.Json;

namespace game
{
    public class WindowConfig 
    {
        public required string Title { get; set; }
        public required int Width { get; set; }
        public required int Height { get; set; }
    }
    internal static class Program
    {
        static void Main(string?[] args) 
        {
            Console.WriteLine("Running ZEA Multiplayer");

            string? serverIp = null;
            string windowConfigPath = "./window_config.json";
            string windowConfigString = File.ReadAllText(windowConfigPath);

            WindowConfig configData = JsonSerializer.Deserialize<WindowConfig>(windowConfigString)
            ?? throw new InvalidOperationException("Failed to deserialize window configuration.");

            // int serverPort = 0;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] != "--server" || i + 1 >= args.Length) continue;
                serverIp = args[i + 1];
                Console.WriteLine($"Connecting to {serverIp} IP");
            }

            if (serverIp != null)
            {
                GetMenu(serverIp).Wait(); 
            }

            //using (Game game = new())
            //{
            //    game.createGame(configData.Width, configData.Height, configData.Title);
            //E}
        }

        static async Task GetMenu(string? serverIp) 
        {
            using (HttpClient client = new())
            {
                try
                {
                    Compiler compiler = new();

                    string url = $"http://{serverIp}/getMenu";
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody);

                    _ = Task.Run(() => compiler.CreateMenuGUI(responseBody));
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"\nException Caught! Server connection error: {e.Message}");
                }
            }
        }
    }
}