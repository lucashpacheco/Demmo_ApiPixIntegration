namespace Demmo_ApiPixIntegration.Models
{
    public class MakeChargeRequest
    {
        public int Id { get; set; }

        public string CampaignId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Key { get; set; }
        public decimal Value { get; set; }
        public string Message { get; set; }
    }
}
