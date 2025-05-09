using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;

namespace TokeroInterviewIntegrationTests.Helpers
{
  public class BrowserFixture
  {
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    public IBrowserContext? Context { get; set; }
    public IPage? Page { get; set; }

    //[SetUp]
    public async Task SetUp(string browserName)
    {
      _playwright = await Playwright.CreateAsync();
       _browser = browserName switch
      {
        "chrome" =>  await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true }),
        "firefox" =>  await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true }),
        "webkit" =>  await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true }),
        _ => throw new ArgumentException($"Unsupported browser: {browserName}")
      };
      Context = await _browser.NewContextAsync();
      Page = await Context.NewPageAsync();
    }

    //[TearDown]
    public async ValueTask DisposeAsync()
    {
      await Context.CloseAsync();
      await _browser.CloseAsync();
      _playwright?.Dispose();
    }

  }
}
