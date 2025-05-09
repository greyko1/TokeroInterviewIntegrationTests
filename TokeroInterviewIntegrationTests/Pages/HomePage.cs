using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokeroInterviewIntegrationTests.Pages
{
  public class HomePage
  {
    private readonly IPage _page;

    public HomePage(IPage page) => _page = page;

    public ILocator LanguageDropDown => _page.Locator(".dropdown").First;
    public async Task GoToPage()
    {
      await _page.GotoAsync("https://tokero.dev/en/");
      await AcceptCookies();
    }

    public async Task SwitchLanguage(string language)
    {
      await LanguageDropDown.ClickAsync();
      // await _page.GetByText(language).First.ClickAsync();
      var link = _page.Locator($"a[href='/{language.ToLowerInvariant()}/']").First;
      await link.ClickAsync();
    }

     public async Task AcceptCookies()
     {
      await _page.GetByRole(AriaRole.Button, new() { NameString = "Accept all cookies"}).ClickAsync();

     }

    public async Task ContactButtonClick(string language)
    {
      await _page.Locator($"a[href='/{language.ToLowerInvariant()}/contact/']").First.ClickAsync();
    }

    public async Task CorporateButtonClick(string langauage)
    {
      await _page.Locator($"a[href='/{langauage.ToLowerInvariant()}/corporate/']").First.ClickAsync();

    }
  }
}
