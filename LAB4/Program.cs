using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = "https://api.github.com/orgs/dotnet/repos";

        // Skapa en HttpClient
        using HttpClient client = new HttpClient();

        // Ställ in nödvändiga headers
        client.DefaultRequestHeaders.Add("User-Agent", "C# App");

        try
        {
            // Gör HTTP GET-förfrågan
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialisera JSON till C#-objekt
            var repositories = JsonSerializer.Deserialize<List<Repository>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Skriv ut resultat
            if (repositories != null)
            {
                foreach (var repo in repositories)
                {
                    Console.WriteLine($"Name: {repo.Name}");
                    Console.WriteLine($"Description: {repo.Description}");
                    Console.WriteLine($"URL: {repo.HtmlUrl}");
                    Console.WriteLine($"Homepage: {repo.Homepage}");
                    Console.WriteLine($"Watchers: {repo.Watchers}");
                    Console.WriteLine($"Last Pushed: {repo.PushedAt}");
                    Console.WriteLine(new string('-', 50));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

public class Repository
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("homepage")]
    public string Homepage { get; set; }

    [JsonPropertyName("watchers")]
    public int Watchers { get; set; }

    [JsonPropertyName("pushed_at")]
    public DateTime PushedAt { get; set; }
}
