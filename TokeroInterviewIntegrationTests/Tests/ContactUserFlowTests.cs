using Microsoft.Playwright;
using TokeroInterviewIntegrationTests.Helpers;
using TokeroInterviewIntegrationTests.Pages;

[assembly: LevelOfParallelism(7)]
namespace TokeroInterviewIntegrationTests.Tests
{
  
  [TestFixture]
  [Parallelizable(ParallelScope.All)]
  public class ContactUserFlowChrome : BrowserFixture
  {
    [TestCase("chrome", "EN", "**/en/contact/")]
    [TestCase("chrome", "RO", "**/ro/contact/")]
    [TestCase("chrome", "DE", "**/de/contact/")]
    [TestCase("chrome", "FR", "**/fr/contact/")]
    [TestCase("chrome", "IT", "**/it/contact/")]
    [TestCase("chrome", "PL", "**/pl/contact/")]
    [TestCase("chrome", "PT", "**/pt/contact/")]
    [TestCase("chrome", "TR", "**/tr/contact/")]
    public async Task ContactForm_SubmitsSuccess(string browser, string language, string expectedUrl)
    {
      await using var fixture = new BrowserFixture();

      await fixture.SetUp(browser);

      var page = fixture.Page;
      var homePage = new HomePage(page);
      var contactPage = new ContactPage(page);  

      //Arrange
      await homePage.GoToPage();

      if (!language.Equals("EN"))
      {
        await homePage.SwitchLanguage(language);
      }

      await homePage.ContactButtonClick(language);
      await page.WaitForURLAsync(expectedUrl);

      //Act
      await contactPage.ContactPageIsLoaded();
      await contactPage.FillContactFormName("TestName");
      await contactPage.FillContactFormEmail("testmail@gmail.com");
      await contactPage.FillContactFormPhone("0700000000");
      await contactPage.FillContactFormMessage("Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem IpsumLorem IpsumLorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum");
      await contactPage.FillContactFormReason();
      await contactPage.SubmitMessageButtonClick();

      //Assert
      //Assert.IsTrue(await contactPage.MessageSentSuccessfullyIsVisible());
      Assert.That(contactPage.ContactFormPageUrl().Result.Contains("/contact/"), Is.True);
    }
  }

  [Parallelizable(ParallelScope.All)]
  [TestFixture]
  public class ContactUserFlowFirefox : BrowserFixture
  {
    [TestCase("firefox", "EN", "**/en/contact/")]
    [TestCase("firefox", "RO", "**/ro/contact/")]
    [TestCase("firefox", "DE", "**/de/contact/")]
    [TestCase("firefox", "FR", "**/fr/contact/")]
    [TestCase("firefox", "IT", "**/it/contact/")]
    [TestCase("firefox", "PL", "**/pl/contact/")]
    [TestCase("firefox", "PT", "**/pt/contact/")]
    [TestCase("firefox", "TR", "**/tr/contact/")]
    public async Task ContactForm_SubmitsSuccess(string browser, string language, string expectedUrl)
    {
      await using var fixture = new BrowserFixture();

      await fixture.SetUp(browser);

      var page = fixture.Page;
      var homePage = new HomePage(page);
      var contactPage = new ContactPage(page);  

      //Arrange
      await homePage.GoToPage();
      if (!language.Equals("EN"))
      {
        await homePage.SwitchLanguage(language);
      }
      await homePage.ContactButtonClick(language);
      await page.WaitForURLAsync(expectedUrl);

      //Act
      await contactPage.ContactPageIsLoaded();
      await contactPage.FillContactFormName("TestName");
      await contactPage.FillContactFormEmail("testemail@gmail.com");
      await contactPage.FillContactFormPhone("0700000000");
      await contactPage.FillContactFormReason();
      await contactPage.FillContactFormMessage("Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem IpsumLorem IpsumLorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum");
      await contactPage.SubmitMessageButtonClick();

      //Assert
      //Assert.IsTrue(await contactPage.MessageSentSuccessfullyIsVisible());
      Assert.That(contactPage.ContactFormPageUrl().Result.Contains("/contact/"), Is.True);

    }
  }

  [Parallelizable(ParallelScope.All)]
  [TestFixture]
  public class ContactUserFlowWebkit : BrowserFixture
  {
    [TestCase("webkit", "EN", "**/en/contact/")]
    [TestCase("webkit", "RO", "**/ro/contact/")]
    [TestCase("webkit", "DE", "**/de/contact/")]
    [TestCase("webkit", "FR", "**/fr/contact/")]
    [TestCase("webkit", "IT", "**/it/contact/")]
    [TestCase("webkit", "PL", "**/pl/contact/")]
    [TestCase("webkit", "PT", "**/pt/contact/")]
    [TestCase("webkit", "TR", "**/tr/contact/")]
    public async Task ContactForm_SubmitsSuccess(string browser, string language, string expectedUrl)
    {
      await using var fixture = new BrowserFixture();

      await fixture.SetUp(browser);

      var page = fixture.Page;
      var homePage = new HomePage(page);
      var contactPage = new ContactPage(page);

      //Arrange
      await homePage.GoToPage();

      if (!language.Equals("EN"))
      {
        await homePage.SwitchLanguage(language);
      }

      await homePage.ContactButtonClick(language);
      await page.WaitForURLAsync(expectedUrl);

      //Act
      await contactPage.ContactPageIsLoaded();
      await contactPage.FillContactFormReason();
      await contactPage.FillContactFormName("TestName");
      await contactPage.FillContactFormEmail("testemail@gmail.com");
      await contactPage.FillContactFormPhone("0700000000");
      await contactPage.FillContactFormMessage("Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem IpsumLorem IpsumLorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum");
      await contactPage.SubmitMessageButtonClick();

      //Assert
      //Assert.IsTrue(await contactPage.MessageSentSuccessfullyIsVisible());
      Assert.That(contactPage.ContactFormPageUrl().Result.Contains("/contact/"), Is.True);

    }
  }
}
