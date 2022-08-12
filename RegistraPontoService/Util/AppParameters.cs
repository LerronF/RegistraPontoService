namespace RegistraPontoService.Util
{
    public class AppParameters
    {
        public string Instance { get; set; }
        public bool IsPrimary { get; set; }
        public string Host { get; set; }
        public int Porta { get; set; }
        public string EmailEnvio { get; set; }
        public string EmailRecebimento { get; set; }        
        public string SenhaEmail { get; set; }
        public int IntervaloMiliSecond { get; set; }
        public string Matricula { get; set; }
        public string SenhaMatricula { get; set; }
        public System.DateTime LastUpdate { get; set; }
    }
}
