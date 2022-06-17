using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RegistraPontoService;
using System;
using System.IO;
using System.Threading;

namespace Registra.Test
{
    [TestClass]
    public class UnitTest1
    {
        DateTime dt = DateTime.Now;

        [TestMethod]
        public void InicioProcesso()
        {
            try
            {
                // Registra();
                LogRegistraPonto("###### Verificando dia da Semana ######");

                if (dt.DayOfWeek == DayOfWeek.Monday)
                {
                    HorarioNormal();
                }
                else if (dt.DayOfWeek == DayOfWeek.Tuesday)
                {
                    HorarioNormal();
                }
                else if (dt.DayOfWeek == DayOfWeek.Wednesday)
                {
                    HorarioNormal();
                }
                else if (dt.DayOfWeek == DayOfWeek.Thursday)
                {
                    HorarioNormal();
                }
                else if (dt.DayOfWeek == DayOfWeek.Friday)
                {
                    HorarioReduzido();
                }
                else if (dt.DayOfWeek == DayOfWeek.Saturday)
                {

                }
                else if (dt.DayOfWeek == DayOfWeek.Sunday)
                {

                }
            }
            catch (Exception resultado)
            {
                LogRegistraPonto("*************************** Erro ao iniciar registro ***************************");
                LogRegistraPonto(resultado.Message.Trim());
            }

        }

        public void LogRegistraPonto(string descricao)
        {
            StreamWriter vWriter = new StreamWriter(@"C:\PCFCustom\Projetos\Teste-RegistraPonto.txt", true);
            vWriter.WriteLine(descricao + " : " + DateTime.Now.ToString());
            vWriter.Flush();
            vWriter.Close();
        }

        public void Registra()
        {
            try
            {
                LogRegistraPonto("Iniciando Registro...");
                ChromeOptions options = new ChromeOptions();
                LogRegistraPonto("Declara Chrome...");
                options.AddArguments("--disable-notifications");
                options.AddArguments("--headless");

                IWebDriver driver = new ChromeDriver(@"C:\PCFCustom\Projetos\RegistraPontoService\RegistraPontoService", options);
                LogRegistraPonto("Instanciando Chrome...");

                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl("https://cliente.apdata.com.br/conecthus/index.html");
                LogRegistraPonto("Navega na URL...");
                Thread.Sleep(20000);

                IWebElement element = null;

                element = driver.FindElement(By.Id("button-1017-btnEl"));
                element.Click();

                element = driver.FindElement(By.Id("ext-156"));
                element.SendKeys("600007");
                LogRegistraPonto("Matricula Inserida...");

                element = driver.FindElement(By.Id("ext-158"));
                element.SendKeys("P@ssw0rd");
                LogRegistraPonto("Senha Inserida...");
                Thread.Sleep(5000);
                element = driver.FindElement(By.CssSelector("#ext-160"));
                element.Click();
                LogRegistraPonto("Clica no Botão de confirmação !!!");

                Thread.Sleep(2000);
                TakeScreenshot(driver);

                driver.Quit();
                SendMail.EnviaEmailResponsavel();
            }
            catch (Exception resultado)
            {
                LogRegistraPonto("Erro ao Registrar Ponto");
                LogRegistraPonto(resultado.Message.Trim());
            }
        }

        public void HorarioNormal()
        {
            if (DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0)
            {
                LogRegistro();
            }
            else if (DateTime.Now.Hour == 17 && DateTime.Now.Minute == 0)
            {
                LogRegistro();
            }
        }

        public void HorarioReduzido()
        {
            if (DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0)
            {
                LogRegistro();
            }
            else if (DateTime.Now.Hour == 16 && DateTime.Now.Minute == 0)
            {
                LogRegistro();
            }
        }
        public void LogRegistro()
        {
            LogRegistraPonto("Verificação - Hoje é " + dt.DayOfWeek);

            Registra();

            LogRegistraPonto("Inicio Registrado");
        }

        public void TakeScreenshot(IWebDriver driver)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(@"C:\PCFCustom\Projetos\ScreenRegistro-" + dt.DayOfWeek + ".png");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
