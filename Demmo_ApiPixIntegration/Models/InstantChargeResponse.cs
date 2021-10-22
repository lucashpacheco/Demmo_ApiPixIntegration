using System;

namespace Demmo_ApiPixIntegration.Models
{
    public class InstantChargeResponse
    {
        public int Id { get; set; }
        public string Txid { get; set; }
        public DateTime CreationDate { get; set; }
        public int Expiration { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string DebtorName { get; set; }
        public string DebtorDocument { get; set; }
        public string ReceiverKey { get; set; }
        public string QrCode { get; set; }
        public string ImageQrCode { get; set; }

        public InstantChargeResponse(int id, string txid, DateTime creationDate, int expiration, decimal ammount, string status, string debtorName, string debtorDocument, string receiverKey, string qrCode, string imageQrCode)
        {
            this.Id = id;
            this.Txid = txid;
            this.CreationDate = creationDate;
            this.Expiration = expiration;
            this.Amount = ammount;
            this.Status = status;
            this.DebtorName = debtorName;
            this.DebtorDocument = debtorDocument;
            this.ReceiverKey = receiverKey;
            this.QrCode = qrCode;
            this.ImageQrCode = imageQrCode;
        }
    }
}
