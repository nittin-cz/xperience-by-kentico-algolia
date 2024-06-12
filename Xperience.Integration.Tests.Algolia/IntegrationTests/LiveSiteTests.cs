using System.Text.RegularExpressions;

using NUnit.Framework;

namespace IntegrationTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BasicTests : DancingGoatLiveSiteTestBase
{
    [Test]
    public async Task DancingGoatHomePageWorks()
    {
        await Expect(Page).ToHaveTitleAsync(new Regex("Home - Dancing Goat"));

        var articleButton = Page.GetByText(new Regex("Articles"));

        await Expect(articleButton).ToHaveAttributeAsync("href", "/articles");

        await articleButton.ClickAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*articles"));
    }
}
