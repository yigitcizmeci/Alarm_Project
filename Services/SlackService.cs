using System.Text;
using Microsoft.Extensions.Options;
using Slack.Webhooks;

namespace Alarm_Project.Services;

public class SlackService(IOptions<SlackOptions> options)
{
    private readonly string _webhookUrl = options.Value.WebhookUrl;

    public async Task<bool> SendSlackMessage(string message)
    {
        using var httpClient = new HttpClient();
        var payload = new { text = message };
        var serializedPayload = System.Text.Json.JsonSerializer.Serialize(payload);
        var content = new StringContent(serializedPayload, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(_webhookUrl, content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Message sent successfully!");
        }
        else
        {
            Console.WriteLine($"Error sending message: {response.StatusCode}");
        }
        return true;
    }
}