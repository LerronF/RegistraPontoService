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
        public System.DateTime LastUpdate { get; set; }
    }
}
