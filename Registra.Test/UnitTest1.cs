using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RegistraPontoService;
using RegistraPontoService.Infra.Data;
using System;
using System.IO;
using System.Threading;

namespace Registra.Test
{
    [TestClass]
    public class UnitTest1
    {
        DateTime dt = DateTime.Now;
        ServiceContext _PCFContext = LoadSettings.CarregaJson();

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
                Log.LogRegistraPonto("#********* Iniciando Registro *********#");
                ChromeOptions options = new ChromeOptions();
                Log.LogRegistraPonto("1 - Declara Chrome.");
                options.AddArguments("--disable-notifications");
                //options.AddArguments("--headless");

                IWebDriver driver = new ChromeDriver(@"C:\PCFCustom\Projetos\RegistraPontoService\RegistraPontoService", options);
                Log.LogRegistraPonto("2 - Instanciando Chrome.");

                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl("https://cliente.apdata.com.br/conecthus/index.html");
                Log.LogRegistraPonto("3 - Navega na URL.");
                Thread.Sleep(20000);

                IWebElement element = null;

                element = driver.FindElement(By.Id("button-1017-btnEl"));
                element.Click();

                element = driver.FindElement(By.Id("ext-156"));
                element.SendKeys("600007");
                Log.LogRegistraPonto("4 - Matricula Inserida.");

                element = driver.FindElement(By.Id("ext-158"));
                element.SendKeys(_PCFContext.SenhaMatricula);
                Log.LogRegistraPonto("5 - Senha Inserida.");
                Thread.Sleep(5000);
                element = driver.FindElement(By.CssSelector("#ext-160"));
                element.Click();
                Log.LogRegistraPonto("6 - Clica no Botão de confirmação !!!");
                Log.LogRegistraPonto("7 - Ponto Registrado com Sucesso !");

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
