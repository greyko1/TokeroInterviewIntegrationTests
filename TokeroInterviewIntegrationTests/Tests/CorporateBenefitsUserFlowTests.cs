using TokeroInterviewIntegrationTests.Helpers;
using TokeroInterviewIntegrationTests.Pages;

namespace TokeroInterviewIntegrationTests.Tests
{
  [TestFixture]
  [Parallelizable(ParallelScope.All)]
  public class CorporateBenefitsUserFlowTestsChrome : BrowserFixture
  {
    public static readonly Dictionary<string, Dictionary<string, (string BenefitHeader, List<string> BenefitsList)>> CorporateBenefitsText = new()
    {
      ["Benefits"] = new()
      {
        ["EN"] = ("Benefits", new()
            {
                "Speed in opening a business account through the efficient verification and validation of documents using dedicated tools (a few hours compared to a few months on foreign platforms)",
                "Dedicated technical and accounting support",
                "Fixed fees regardless of the volume traded so that you enjoy a premium service right from the start"
            }),
        ["RO"] = ("Beneficii", new()
            {
                "Rapiditate în deschiderea unui cont business prin verificarea și validarea eficientă a documentelor prin tool-uri dedicate (câteva ore comparativ cu câteva luni la platformele din străinătate)",
                "Suport tehnic și contabil dedicat",
                "Fee-uri fixe indiferent de volumul tranzacționat astfel încât să te bucuri de un serviciu premium chiar de la început"
            }),
        ["DE"] = ("Vorteile", new()
            {
                "Schnelle Eröffnung eines Geschäftskontos durch effiziente Überprüfung und Validierung von Dokumenten mit Hilfe spezieller Tools (einige Stunden im Vergleich zu einigen Monaten auf ausländischen Plattformen)",
                "Engagierte technische und buchhalterische Unterstützung",
                "Feste Gebühren unabhängig vom gehandelten Volumen, so dass Sie von Anfang an einen erstklassigen Service genießen"
            }),
        ["FR"] = ("Bénéfices", new()
            {
                "Rapidité d'ouverture d'un compte business grâce à la vérification et à la validation efficaces des documents à l'aide d'outils dédiés (quelques heures contre quelques mois sur les plateformes étrangères)",
                "Assistance technique et comptable dédiée",
                "Des frais fixes, quel que soit le volume échangé, afin que tu puisses bénéficier d'un service haut de gamme dès le départ"
            }),
        ["IT"] = ("Benefici", new()
            {
                "Velocità nell'apertura di un account aziendale attraverso l'efficace verifica e validazione dei documenti attraverso strumenti dedicati (poche ore rispetto a pochi mesi su piattaforme estere)",
                "Supporto tecnico e contabile dedicato",
                "Commissioni fisse indipendentemente dal volume scambiato, per usufruire di un servizio premium fin dall'inizio"
            }),
        ["PL"] = ("Benefits", new()
            {
                "Speed in opening a business account through the efficient verification and validation of documents using dedicated tools (a few hours compared to a few months on foreign platforms)",
                "Dedicated technical and accounting support",
                "Fixed fees regardless of the volume traded so that you enjoy a premium service right from the start"
            }),
        ["PT"] = ("Benefícios", new()
            {
                "Rapidez na abertura duma conta business através da verificação e validação eficientes dos documentos por meio das aplicações específicas (algumas horas em comparação com alguns meses nas plataformas estrangeiras)",
                "Apoio técnico ao cliente dedicado",
                "Taxas fixas sem importar o volume trocado para que você goze dum serviço premium desde o início"
            }),
        ["TR"] = ("Faydalar", new()
            {
                "Özel araçlar kullanılarak belgelerin verimli bir şekilde doğrulanması ve onaylanması yoluyla işletme hesabı açma hızı (yabancı platformlarda birkaç ay ile karşılaştırıldığında birkaç saat)",
                "Özel teknik ve muhasebe desteği",
                "En başından itibaren premium hizmetten yararlanmanız için işlem hacmine bakılmaksızın sabit ücretler"
            })
      }
    };

    [TestCase("chrome", "EN", "**/en/corporate/")]
    [TestCase("chrome", "RO", "**/ro/corporate/")]
    [TestCase("chrome", "DE", "**/de/corporate/")]
    [TestCase("chrome", "FR", "**/fr/corporate/")]
    [TestCase("chrome", "IT", "**/it/corporate/")]
    [TestCase("chrome", "PL", "**/pl/corporate/")]
    [TestCase("chrome", "PT", "**/pt/corporate/")]
    [TestCase("chrome", "TR", "**/tr/corporate/")]
    [TestCase("firefox", "EN", "**/en/corporate/")]
    [TestCase("firefox", "RO", "**/ro/corporate/")]
    [TestCase("firefox", "DE", "**/de/corporate/")]
    [TestCase("firefox", "FR", "**/fr/corporate/")]
    [TestCase("firefox", "IT", "**/it/corporate/")]
    [TestCase("firefox", "PL", "**/pl/corporate/")]
    [TestCase("firefox", "PT", "**/pt/corporate/")]
    [TestCase("firefox", "TR", "**/tr/corporate/")]
    [TestCase("webkit", "EN", "**/en/corporate/")]
    [TestCase("webkit", "RO", "**/ro/corporate/")]
    [TestCase("webkit", "DE", "**/de/corporate/")]
    [TestCase("webkit", "FR", "**/fr/corporate/")]
    [TestCase("webkit", "IT", "**/it/corporate/")]
    [TestCase("webkit", "PL", "**/pl/corporate/")]
    [TestCase("webkit", "PT", "**/pt/corporate/")]
    [TestCase("webkit", "TR", "**/tr/corporate/")]
    public async Task CorporateBenefits_TranslatedSuccess(string browser, string language, string expectedUrl)
    {
      await using var fixture = new BrowserFixture();

      await fixture.SetUp(browser);

      var page = fixture.Page;
      var homePage = new HomePage(page);
      var corporatePage = new CorporatePage(page);

      //Arrange
      await homePage.GoToPage();

      if (!language.Equals("EN"))
      {
        await homePage.SwitchLanguage(language);
      }

      await homePage.CorporateButtonClick(language);
      await page.WaitForURLAsync(expectedUrl);
      var expected = CorporateBenefitsText["Benefits"][language];

      var actualHeader = await corporatePage.FindCorporatePageHeaderText(expected.BenefitHeader);

      var actualbenefits = await corporatePage.FindCorporatePageBenefitsListText();

      Assert.That(expected.BenefitHeader,Is.EqualTo(actualHeader));
      Assert.That(expected.BenefitsList, Is.EquivalentTo(actualbenefits));
    }
  }
}
