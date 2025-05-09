using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TokeroInterviewIntegrationTests.Pages
{
  internal class ContactPage
  {
    private readonly IPage _page;

    public ContactPage(IPage page) => _page = page;

    private ILocator ContactFormReason => _page.Locator("#contact-form-reasons");
    private ILocator ContactFormName => _page.Locator("#contact-form-name");
    private ILocator ContactFormEmail => _page.Locator("#contact-form-email");
    private ILocator ContactFormPhone => _page.Locator("#contact-form-phone");
    private ILocator ContactFormMessage => _page.Locator("#contact-form-message");
    private ILocator ContactFormSubmitButton => _page.Locator("#contact-form-submitBtn");
    private ILocator MessageSentSuccessfully => _page.GetByText("Thank you for contacting us.");
    
    public async Task ContactPageIsLoaded()
    {
      await _page.WaitForLoadStateAsync(LoadState.Load);
    }
    public async Task FillContactFormReason()
    {
      const int maxNumberOfRetries = 3;
      int attempt = 0;
      bool success = false;
      int timeBetweenRetries = 20;

      while (attempt < maxNumberOfRetries && !success)
      {
        attempt++;
        Console.WriteLine($"Attempt {attempt} to fill contact form reason...");

        // Wait 20s before each attempt (including first)
        Console.WriteLine($"[Waiting {timeBetweenRetries} seconds before attempt {attempt}...");
        await Task.Delay(TimeSpan.FromSeconds(timeBetweenRetries));

        try
        {
          await ContactFormReason.ClickAsync();
          var firstOption = _page.Locator(".mud-list-item-text").First;
          await firstOption.ClickAsync();

          success = true;

        }
        catch (Exception ex) 
        {
          Console.WriteLine($"Attempt {attempt} failed with message: {ex.Message}");
          
          await _page.ScreenshotAsync(new()
          {
            Path = $"screenshots/fill_reason_attempt{attempt}_{DateTime.Now:yyyyMMdd_HHmmss}.png",
            FullPage = true
          });

          if (attempt < maxNumberOfRetries)
          {
            Console.WriteLine("Refreshing page before retry...");
            await _page.ReloadAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
          }
        }
      }
    }

    public async Task FillContactFormName(string name)
    {
      await ContactFormName.FillAsync(name);
    }

    public async Task FillContactFormEmail(string email)
    { 
      await ContactFormEmail.FillAsync(email);
    }

    public async Task FillContactFormPhone(string phone)
    {
      await ContactFormPhone.FillAsync(phone);
    }

    public async Task FillContactFormMessage(string message)
    {
      await ContactFormMessage.FillAsync(message);
    }
    public async Task SubmitMessageButtonClick()
    {
      await ContactFormSubmitButton.ClickAsync();
    }

    public async Task<bool> MessageSentSuccessfullyIsVisible()
    {
      return await MessageSentSuccessfully.IsVisibleAsync();
    }
    
    public async Task<string> ContactFormPageUrl()
    {
      return _page.Url;
    }
  }
}
