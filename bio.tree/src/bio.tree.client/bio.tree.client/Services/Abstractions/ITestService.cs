using bio.tree.shared;

namespace bio.tree.client.Services.Abstractions;

public interface ITestService
{
    Task<TestDto> GetTest();
}