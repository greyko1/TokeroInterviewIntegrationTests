using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokeroInterviewIntegrationTests.Pages
{
  internal class CorporatePage
  {
    private readonly IPage _page;
    public CorporatePage(IPage page) => _page = page;
    private ILocator BenefitsList => _page.Locator(".list-disc");

    public async Task<string> FindCorporatePageHeaderText(string headerText)
    {
      var text = await _page.Locator($"h2:text('{headerText}')").First.InnerTextAsync();
      return text;
    }

    public async Task<List<string>> FindCorporatePageBenefitsListText()
    {
      var texts = new List<string>();

      for (int i = 0; i < await BenefitsList.First.Locator("li").CountAsync(); i++)
      {
        var text = await BenefitsList.Locator("li").Nth(i).InnerTextAsync();
        texts.Add(text);
      }
      return texts;
    }
  }
}
