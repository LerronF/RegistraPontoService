using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace RegistraPontoService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer1;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer(new TimerCallback(timer1_Tick), null, 15000, 60000);
            
            LogRegistraPonto("Iniciando Serviço...");
        }

        protected override void OnStop()
        {
            LogRegistraPonto("Serviço parado...");
        }

        private void timer1_Tick(object sender)
        {
            try
            {
                DateTime dt = DateTime.Now;

                Registra();

                if (dt.DayOfWeek != DayOfWeek.Friday || dt.DayOfWeek != DayOfWeek.Saturday || dt.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0)
                    {
                        Registra();
                        LogRegistraPonto("Inicio Registrado");
                    }
                    else if (DateTime.Now.Hour == 17 && DateTime.Now.Minute == 0)
                    {
                        Registra();
                        LogRegistraPonto("Final Registrado");
                    }
                }

                if (dt.DayOfWeek == DayOfWeek.Friday)
                {
                    if (DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0)
                    {
                        Registra();
                        LogRegistraPonto("Inicio Registrado");
                    }
                    else if (DateTime.Now.Hour == 16 && DateTime.Now.Minute == 0)
                    {
                        Registra();
                        LogRegistraPonto("Final Registrado");
                    }
                }
            }
            catch (Exception resultado)
            {
                LogRegistraPonto("Erro ao iniciar registro");
                LogRegistraPonto(resultado.Message.Trim());
            }

        }

        public void LogRegistraPonto(string descricao)
        {
            StreamWriter vWriter = new StreamWriter(@"C:\PCFCustom\Projetos\RegistraPonto.txt", true);
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
                //options.AddArguments("--headless");
                LogRegistraPonto("Arguments");
                IWebDriver driver = new ChromeDriver(@"C:\PCFCustom\Projetos\RegistraPontoService\RegistraPontoService", options);
                LogRegistraPonto("Instanciando Chrome...");
                driver.Manage().Window.Maximize();
                LogRegistraPonto("Maximize");
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl("https://cliente.apdata.com.br/conecthus/index.html");
                LogRegistraPonto("Navega na URL...");
                Thread.Sleep(3000);

                IWebElement element = null;

                element = driver.FindElement(By.Id("button-1017"));
                element.Click();

                element = driver.FindElement(By.Id("ext-156"));
                element.SendKeys("600855");
                LogRegistraPonto("Matricula Inserida...");
                element = driver.FindElement(By.Id("ext-158"));
                element.SendKeys("P@ssw0rd");
                LogRegistraPonto("Senha Inserida...");
                //element = driver.FindElement(By.Id("ext160"));
                //element.Click();
                Thread.Sleep(5000);
                LogRegistraPonto("Clica Botão...");
            }
            catch (Exception resultado)
            {               
                LogRegistraPonto("Erro ao Registrar Ponto");
                LogRegistraPonto(resultado.Message.Trim());
            }
        }
    }
}
