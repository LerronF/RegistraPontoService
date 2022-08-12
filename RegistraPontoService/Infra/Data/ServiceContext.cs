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
        }
    }
}
