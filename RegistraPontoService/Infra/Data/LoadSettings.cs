using RegistraPontoService.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RegistraPontoService.Infra.Data
{
    public class LoadSettings
    {
        public static ServiceContext CarregaJson()
        {
            ServiceContext Context;
            AppSettings LocalSettings;
            AppParameters primaryParameters;
            //string arqConexao;

            try
            {
                string path = @"C:\PCFCustom\Projetos";

                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                string fileName = @"C:\PCFCustom\Projetos\Config.json";
                string strJson;
                if (File.Exists(fileName))
                {
                    strJson = File.ReadAllText(fileName);
                    LocalSettings = JsonSerializer.Deserialize<AppSettings>(strJson);

                    if (LocalSettings.Settings == null || LocalSettings.Settings.Count == 0)
                    {
                        LocalSettings.Settings = new List<AppParameters>();
                        LocalSettings.Settings.Add(new AppParameters()
                        {
                            Host = "smtp.office365.com",
                            Porta = 587,
                            EmailEnvio = "lerron.jesus@tectoy.com",
                            EmailRecebimento = "lerron.jesus@tectoy.com",
                            SenhaEmail = "xxxxxxxxxxxx",
                            IntervaloMiliSecond = 60000,
                            Matricula= "xxxxxxx",
                            SenhaMatricula= "xxxxxxxx",
                            IsPrimary = true,
                            Hora_Entrada = 7,
                            Minuto_Entrada = 0,
                            Hora_Intervalo_Saida=11,
                            Minuto_Intervalo_Saida=30,
                            Hora_Intervalo_Entrada=12,
                            Minuto_Intervalo_Entrada=30,
                            Hora_Saida=17,
                            Minuto_Saida=0,
                            Hora_Saida_Reduzido = 16,
                            Minuto_Saida_Reduzido = 0
                        });
                    }

                    primaryParameters = LocalSettings.Settings.FirstOrDefault(o => o.IsPrimary == true);

                    return Context = new ServiceContext(primaryParameters);
                }
                else
                {
                    primaryParameters = new AppParameters();
                    LocalSettings = new AppSettings();

                    LocalSettings.Settings = new List<AppParameters>();
                    LocalSettings.Settings.Add(new AppParameters()
                    {
                        Host = "smtp.office365.com",
                        Porta = 587,
                        EmailEnvio = "lerron.jesus@transire.com",
                        EmailRecebimento = "lerron.jesus@conecthus.org.br",
                        SenhaEmail = "xxxxxxxxxxxxxxx",
                        IntervaloMiliSecond = 60000,
                        Matricula = "600855",
                        SenhaMatricula = "P@xxxxxx",
                        IsPrimary = true,
                        Hora_Entrada = 7,
                        Minuto_Entrada = 0,
                        Hora_Intervalo_Saida = 11,
                        Minuto_Intervalo_Saida = 30,
                        Hora_Intervalo_Entrada = 12,
                        Minuto_Intervalo_Entrada = 30,
                        Hora_Saida = 17,
                        Minuto_Saida = 0,
                        Hora_Saida_Reduzido = 16,
                        Minuto_Saida_Reduzido = 0
                    });

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    strJson = JsonSerializer.Serialize(LocalSettings, options);
                    fileName = @"C:\PCFCustom\Projetos\Config.json";
                    File.WriteAllText(fileName, strJson);

                    strJson = File.ReadAllText(fileName);
                    LocalSettings = JsonSerializer.Deserialize<AppSettings>(strJson);
                    primaryParameters = LocalSettings.Settings.FirstOrDefault(o => o.IsPrimary == true);
                    return Context = new ServiceContext(primaryParameters);
                }
            }
            catch (System.Exception ex)
            {
                Log.LogRegistraPonto(ex.Message.ToString());
                return null;
            }
        }
    }
}
