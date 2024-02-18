using bio.tree.client.Services.Abstractions;
using bio.tree.shared;

namespace bio.tree.client.Services;

internal sealed class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;
    private readonly HttpClient _httpClient;
    
    public TestService(ILogger<TestService> logger,
        HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }
    
    public async Task<TestDto> GetTest() 
        => await _httpClient.GetFromJsonAsync<TestDto>("/test");
        
}