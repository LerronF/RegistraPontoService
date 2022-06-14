using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RegistraPontoService;
using System.Threading;

namespace Registra.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegistraTeste()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("--disable-notifications");
            //options.AddArguments("--headless");

            IWebDriver driver = new ChromeDriver(@"C:\PCFCustom\Projetos\RegistraPontoService\Registra.Test", options);

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://cliente.apdata.com.br/conecthus/index.html");

            IWebElement element = null;
            Thread.Sleep(9000);
            element = driver.FindElement(By.Id("button-1017-btnEl"));
            element.Click();

            element = driver.FindElement(By.Id("ext-156"));
            element.SendKeys("600855");

            element = driver.FindElement(By.Id("ext-158"));
            element.SendKeys("P@ssw0rd");

            //element = driver.FindElement(By.Id("ext160"));
            //element.Click();           

            driver.Quit();
            SendMail.EnviaEmailResponsavel();
        }
    }
}
