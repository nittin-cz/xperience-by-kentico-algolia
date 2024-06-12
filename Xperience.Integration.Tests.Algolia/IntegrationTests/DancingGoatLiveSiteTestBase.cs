using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace IntegrationTests;
public class DancingGoatLiveSiteTestBase : PageTest
{
    public const string DANCING_GOAT_TEST_URL = "http://localhost:55120/";

    [SetUp]
    public async Task SetUp() => await Page.GotoAsync(DANCING_GOAT_TEST_URL);
}
