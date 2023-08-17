using RegistraPontoService.Util;

namespace RegistraPontoService.Infra.Data
{
    public class ServiceContext
    {
        public string Host { get; set; }
        public int Porta { get; set; }
        public string EmailEnvio { get; set; }
        public string EmailRecebimento { get; set; }
        public string SenhaEmail { get; set; }
        public int IntervaloMiliSecond { get; set; }
        public string Matricula { get; set; }
        public string SenhaMatricula { get; set; }
        public int Hora_Entrada { get; set; }
        public int Minuto_Entrada { get; set; }
        public int Hora_Intervalo_Saida { get; set; }
        public int Minuto_Intervalo_Saida { get; set; }
        public int Hora_Intervalo_Entrada { get; set; }
        public int Minuto_Intervalo_Entrada { get; set; }
        public int Hora_Saida { get; set; }
        public int Minuto_Saida { get; set; }
        public int Hora_Saida_Reduzido { get; set; }
        public int Minuto_Saida_Reduzido { get; set; }

        public ServiceContext(AppParameters appParameters)
        {
            Host = appParameters.Host;
            Porta = appParameters.Porta;
            EmailEnvio = appParameters.EmailEnvio;
            EmailRecebimento = appParameters.EmailRecebimento;
            SenhaEmail = appParameters.SenhaEmail;
            IntervaloMiliSecond = appParameters.IntervaloMiliSecond;
            Matricula = appParameters.Matricula;
            SenhaMatricula = appParameters.SenhaMatricula;

            Hora_Entrada = appParameters.Hora_Entrada;
            Minuto_Entrada = appParameters.Minuto_Entrada;
            Hora_Intervalo_Saida = appParameters.Hora_Intervalo_Saida;
            Minuto_Intervalo_Saida = appParameters.Minuto_Intervalo_Saida;
            Hora_Intervalo_Entrada = appParameters.Hora_Intervalo_Entrada;
            Minuto_Intervalo_Entrada = appParameters.Minuto_Intervalo_Entrada;
            Hora_Saida = appParameters.Hora_Saida;
            Minuto_Saida = appParameters.Minuto_Saida;
            Hora_Saida_Reduzido = appParameters.Hora_Saida_Reduzido;
            Minuto_Saida_Reduzido = appParameters.Minuto_Saida_Reduzido;
        }
    }
}
