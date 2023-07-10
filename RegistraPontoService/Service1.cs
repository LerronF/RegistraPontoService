using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RegistraPontoService.Infra.Data;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace RegistraPontoService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer1;
        DateTime dt = DateTime.Now;
        ServiceContext _PCFContext;

        public Service1()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _PCFContext = LoadSettings.CarregaJson(); 

                timer1 = new Timer(new TimerCallback(timer1_Tick), null, 15000, _PCFContext.IntervaloMiliSecond);

                Log.LogRegistraPonto("#------------------------------ Iniciando Serviço ------------------------------#" + _PCFContext.Matricula);
            }
            catch (Exception ex)
            {
                Log.LogRegistraPonto(ex.Message.Trim());
            }            
        }

        protected override void OnStop()
        {
            Log.LogRegistraPonto("#------------------------------ Serviço Parado ------------------------------#");
        }

        private void timer1_Tick(object sender)
        {
            try
            {
                dt = DateTime.Now;

                // Registra();
                // Log.LogRegistraPonto("###### Verificando dia da Semana ######");

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
                Log.LogRegistraPonto("*************************** Erro ao iniciar registro ***************************");
                Log.LogRegistraPonto(resultado.Message.Trim());
            }

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

                IWebDriver driver = new ChromeDriver(@"D:\Projetos\RegistraPontoService\RegistraPontoService", options);
                Log.LogRegistraPonto("2 - Instanciando Chrome.");

                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl("https://cliente.apdata.com.br/tectoy/");
                Log.LogRegistraPonto("3 - Navega na URL.");
                Thread.Sleep(20000);

                IWebElement element = null;

                element = driver.FindElement(By.Id("button-1017-btnEl"));
                element.Click();

                element = driver.FindElement(By.Id("ext-156"));
                element.SendKeys(_PCFContext.Matricula);
                Log.LogRegistraPonto("4 - Matricula Inserida.");

                element = driver.FindElement(By.Id("ext-158"));
                element.SendKeys(_PCFContext.SenhaMatricula);
                Log.LogRegistraPonto("5 - Senha Inserida.");
                Thread.Sleep(5000);
                element = driver.FindElement(By.CssSelector("#ext-160"));
                element.Click();
                Log.LogRegistraPonto("6 - Clica no Botão de confirmação !!!");
                Log.LogRegistraPonto("7 - Ponto Registrado com Sucesso !");

                Thread.Sleep(4000);
                TakeScreenshot(driver);

                driver.Quit();
                SendMail.EnviaEmailResponsavel();
            }
            catch (Exception resultado)
            {
                Log.LogRegistraPonto("Erro ao Registrar Ponto");
                Log.LogRegistraPonto(resultado.Message.Trim());
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
            dt = DateTime.Now;

            Log.LogRegistraPonto("Verificação - Hoje é " + dt.DayOfWeek);

            Registra();

            Log.LogRegistraPonto("#********* Registro realizado com sucesso !!! *********#");
        }

        public void TakeScreenshot(IWebDriver driver)
        {
            try
            {
                dt = DateTime.Now;

                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(@"C:\PCFCustom\Projetos\ScreenRegistro-"+ dt.DayOfWeek + ".png");
            }
            catch (Exception e)
            {
                Log.LogRegistraPonto(e.Message.Trim());
            }
        }
    }
}
