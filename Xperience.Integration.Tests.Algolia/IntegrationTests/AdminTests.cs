using Microsoft.Playwright;

using System.Text.RegularExpressions;

using NUnit.Framework;

namespace IntegrationTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BasicAdminTests : XperienceAdministrationTestBase
{
    public const string SEARCH_MODULE_NAME = "Algolia Search";

    [Test]
    public async Task VerifyAdministrationFunctionality()
    {
        await Login();
        await CheckEventLog();
    }

    [Test]
    public async Task LoadSearchModule()
    {
        await Login();
        await OpenAdminApplication(SEARCH_MODULE_NAME);
        await CheckSearchTableOrNoRecords();

        await CheckEventLog();
    }

    //[Test]
    //public async Task DeleteAllIndexes()
    //{
    //    await Login();
    //    await OpenAdminApplication(SEARCH_MODULE_NAME);

    //    int recordCount = await Page.GetByTestId("table-row")
    //        .CountAsync();

    //    for (int i = 0; i < recordCount; i++)
    //    {
    //        var deleteButton = Page.GetByRole(AriaRole.Listitem).Nth(recordCount - 1);
    //        await Expect(deleteButton).tohave

    //        await deleteButton.ClickAsync();
    //    }

    //    string textContent = await Page.TextContentAsync("body") ?? "";
    //    Assert.IsTrue(textContent.Contains("There are no records to display"));
    //}

    private async Task CreateIndex()
    {
        await Login();
        await OpenAdminApplication(SEARCH_MODULE_NAME);
        var createButton = Page.GetByText(new Regex("Create"));
    }

    private async Task CheckSearchTableOrNoRecords()
    {
        string textContent = await Page.TextContentAsync("body") ?? "";
        bool noRecords = textContent.Contains("There are no records to display");

        var records = await Page.GetByRole(AriaRole.Row)
            .ElementHandlesAsync();
        bool hasRecords = records.Any();

        Assert.IsTrue(noRecords || hasRecords);
    }
}
