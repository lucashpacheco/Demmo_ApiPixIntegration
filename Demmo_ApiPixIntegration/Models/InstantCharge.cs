namespace Demmo_ApiPixIntegration.Models
{
    public class InstantCharge
    {
        public Calendario calendario { get; set; }
        public Devedor devedor { get; set; }
        public Valor valor { get; set; }
        public string chave { get; set; }
        public string solicitacaoPagador { get; set; }

        public InstantCharge(string devedorCpf , string devedorNome , decimal valorOriginal , string chave , string solicitacaoPagador , int expiracao = 3600)
        {
            this.calendario = new Calendario();
                this.calendario.expiracao = expiracao;
            this.devedor = new Devedor();
                this.devedor.cpf = devedorCpf;
                this.devedor.nome = devedorNome;
            this.valor = new Valor();
                this.valor.original = valorOriginal.ToString();
            this.chave = chave;
            this.solicitacaoPagador = solicitacaoPagador;

        }
    }

    public class Calendario
    {
        public int expiracao { get; set; }
    }

    public class Devedor
    {
        public string cpf { get; set; }
        public string nome { get; set; }
    }

    public class Valor
    {
        public string original { get; set; }
    }


    
}
