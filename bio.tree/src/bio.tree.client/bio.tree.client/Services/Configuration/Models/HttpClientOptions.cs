namespace bio.tree.client.Services.Configuration.Models;

internal sealed record HttpClientOptions
{
    public string Url { get; init; }
    public TimeSpan Timeout { get; set; }
}