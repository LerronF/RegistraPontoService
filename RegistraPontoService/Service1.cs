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
                Log.LogRegistraPonto("###### Verificando dia da Semana e Horario "+ DateTime.Now.ToString() +"######");

                if (dt.DayOfWeek == DayOfWeek.Monday)
                {
                    HorarioNormal();
                    Horario_Intervalo_Almoco();
                }
                else if (dt.DayOfWeek == DayOfWeek.Tuesday)
                {
                    HorarioNormal();
                    Horario_Intervalo_Almoco();
                }
                else if (dt.DayOfWeek == DayOfWeek.Wednesday)
                {
                    HorarioNormal();
                    Horario_Intervalo_Almoco();
                }
                else if (dt.DayOfWeek == DayOfWeek.Thursday)
                {
                    HorarioNormal();
                    Horario_Intervalo_Almoco();
                }
                else if (dt.DayOfWeek == DayOfWeek.Friday)
                {
                    HorarioReduzido();
                    Horario_Intervalo_Almoco();
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

                IWebDriver driver = new ChromeDriver(@"C:\PCFCustom\Projetos\RegistraPontoService\RegistraPontoService", options);
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
            if (DateTime.Now.Hour == _PCFContext.Hora_Entrada && DateTime.Now.Minute == _PCFContext.Minuto_Entrada)
            {
                LogRegistro();
            }
            else if (DateTime.Now.Hour == _PCFContext.Hora_Saida && DateTime.Now.Minute == _PCFContext.Minuto_Saida)
            {
                LogRegistro();
            }
        }

        public void HorarioReduzido()
        {
            if (DateTime.Now.Hour == _PCFContext.Hora_Entrada && DateTime.Now.Minute == _PCFContext.Minuto_Entrada)
            {
                LogRegistro();
            }
            else if (DateTime.Now.Hour == _PCFContext.Hora_Saida_Reduzido && DateTime.Now.Minute == _PCFContext.Minuto_Saida_Reduzido)
            {
                LogRegistro();
            }
        }

        public void Horario_Intervalo_Almoco()
        {
            if (DateTime.Now.Hour == _PCFContext.Hora_Intervalo_Saida && DateTime.Now.Minute == _PCFContext.Minuto_Intervalo_Saida)
            {
                LogRegistro();
            }
            else if (DateTime.Now.Hour == _PCFContext.Hora_Intervalo_Entrada && DateTime.Now.Minute == _PCFContext.Minuto_Intervalo_Entrada)
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
