using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

using NUnit.Framework;

using System.Globalization;
using System.Text.RegularExpressions;

namespace IntegrationTests;
public class XperienceAdministrationTestBase : PageTest
{
    public const string DANCING_GOAT_ADMIN_TEST_URL = "http://localhost:55120/admin";
    public const string ADMINISTRATION_ADMIN_NAME = "Administrator";
    public const string ADMINISTRATION_ADMIN_PASSWORD = "";
    public string AdminLoginUrl { get; } = DANCING_GOAT_ADMIN_TEST_URL + "/logon";
    public string AdminDashboardUrl { get; } = DANCING_GOAT_ADMIN_TEST_URL + "/dashboard";

    [SetUp]
    public async Task SetUp() => await Page.GotoAsync(AdminLoginUrl);

    protected async Task Login()
    {
        await Page.GotoAsync(AdminLoginUrl);

        await Page.FillAsync("input[name='userName']", ADMINISTRATION_ADMIN_NAME);
        await Page.FillAsync("input[name='password']", ADMINISTRATION_ADMIN_PASSWORD);
        await Page.ClickAsync("button[type='submit']");

        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    private async Task CheckInEventLog(string pageName)
    {
        await Page.FillAsync("input[type='text']", pageName);
        await Page.PressAsync("input[type='text']", "Enter");

        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        var errorRecords = await Page.GetByRole(AriaRole.Row)
            .Filter(new LocatorFilterOptions { HasTextRegex = new Regex("Error") })
            .ElementHandlesAsync();

        bool noErrorsFound = errorRecords.All(record =>
        {
            var timeElement = record.QuerySelectorAsync("div[data-testid='table-cell-EventTime']").Result;
            string? timeText = timeElement?.InnerTextAsync()?.Result ?? "";
            var recordTime = DateTime.Parse(timeText, new CultureInfo("en-US"));
            return recordTime < DateTime.Now.AddMinutes(-1);
        });

        Assert.IsTrue(noErrorsFound);
    }

    protected async Task CheckEventLog()
    {
        await Page.GotoAsync(AdminDashboardUrl);
        await OpenAdminApplication("Event log");

        await CheckInEventLog("Algolia");
        await CheckInEventLog("ListingPage");
        await CheckInEventLog("EditPage");
    }

    protected async Task OpenAdminApplication(string applicationName)
    {
        await Page.ClickAsync($"button[aria-label='{applicationName}']");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}
