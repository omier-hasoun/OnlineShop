
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Application.Common.Dtos;

namespace Infrastructure.Services.Email.Maileroo;

internal sealed class MailerooMailService : IEmailService
{
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,

    };

    public string NoReplyInfoEmail { get; init; }
    public string ServiceEmail { get; init; }

    public MailerooMailService(IHttpClientFactory factory, string noReplyInfoEmail, string serviceEmail)
    {
        _client = factory.CreateClient("Maileroo");
        NoReplyInfoEmail = noReplyInfoEmail;
        ServiceEmail = serviceEmail;
    }

    public async Task SendEmailAsync(EmailMessageRequest emailRequest)
    {

        var request = new
        {
            from = new MailerooEmailObject
            {
                Address = emailRequest.Sender,
                DisplayName = emailRequest.SenderDisplayName
            },
            to = new MailerooEmailObject
            {
                Address = emailRequest.Recipient
            },
            subject = emailRequest.Subject,
            html = emailRequest.IsHtml ? emailRequest.Body : null,
            plain = !emailRequest.IsHtml ? emailRequest.Body : null,
            tracking = false
        };

        using var content = JsonContent.Create(request, options: _jsonOptions);

        var response = await _client.PostAsync("emails", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException(
                $"Maileroo request failed ({(int)response.StatusCode}): {errorBody}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<MailerooResponse>(
                                                responseBody.AsSpan(),
                                                _jsonOptions);

        if (result is null || !result.Success)
            throw new InvalidOperationException("Resposne deserialization failed");
            
    }

    private sealed class MailerooResponse
    {
        public bool Success { get; init; }
    }
}
