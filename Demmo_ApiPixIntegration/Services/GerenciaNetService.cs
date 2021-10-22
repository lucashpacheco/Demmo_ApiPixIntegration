using Demmo_ApiPixIntegration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Demmo_ApiPixIntegration.Services
{
    public class GerenciaNetService : IAPIPixService
    {
        private readonly IConfiguration _configuration;
        private readonly APIPixSettings _aPIPixSettings;
        public GerenciaNetService(IConfiguration configuration, APIPixSettings options)
        {
            _configuration = configuration;
            _aPIPixSettings = options;  
        }

        #region Conection with API
        /// <summary>
        /// Inicio o cliente de Rest
        /// </summary>
        /// <param name="fullUrl"></param>
        /// <returns></returns>
        private RestClient StartClient(string fullUrl)
        {
            var certificate = GetCertificate();
            RestClient client = new RestClient(fullUrl);
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            return client;
        }

        private X509Certificate2 GetCertificate()
        {
            try
            {
                var path = Path.GetFullPath(Path.Combine("./Cert", _aPIPixSettings.GN_P12));
                return new X509Certificate2(path, _aPIPixSettings.GN_P12_pass); 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// Configura o cabeçalho da solitação
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private RestRequest ConfigHeader(string authorization , Method method = Method.POST)
        {
            RestRequest request = new RestRequest(method);
            request.AddHeader("Authorization", authorization);
            if  (method == Method.POST)
            {
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
            }

            return request;
        }


        #endregion

        public async Task<string> GetToken()
        {
            try
            {
                var identification = String.Format("{0}:{1}", _aPIPixSettings.GN_Client_Id, _aPIPixSettings.GN_Client_Secret);
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(identification);
                var bs64 = Convert.ToBase64String(toEncodeAsBytes);

                var content = new Dictionary<string, string>();
                content.Add("grant_type", "client_credentials");

                var client = StartClient(_aPIPixSettings.GN_Url_OAuth);
                var request = ConfigHeader("Basic " + bs64);
                request.AddParameter("application/json", SimpleJson.SerializeObject(content), ParameterType.RequestBody);

                IRestResponse response = await client.ExecuteAsync(request);

                var responseJson = SimpleJson.DeserializeObject<Dictionary<string,dynamic>>(response.Content);

                return responseJson["access_token"]; 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        public async Task<List<InstantChargeResponse>> MakeCharge(List<MakeChargeRequest> requests)
        {
            try
            {
                var oAuthToken = GetToken();
                var responseList = new List<InstantChargeResponse>();
                foreach (MakeChargeRequest request in requests)
                {
                    InstantCharge content = new InstantCharge(request.Cpf, request.Name, request.Value, request.Key, request.Message);

                    IRestResponse response = GetInstantCharge(content, oAuthToken.Result).Result;

                    var responseJson = SimpleJson.DeserializeObject<Dictionary<string, dynamic>>(response.Content);

                    IRestResponse responseQrCodeBs64 = GetQrCode((int)responseJson["loc"]["id"], oAuthToken.Result).Result;

                    var responseQrCodeBs64Json = SimpleJson.DeserializeObject<Dictionary<string, dynamic>>(responseQrCodeBs64.Content);

                    var qrCodeBs64 = responseQrCodeBs64Json["imagemQrcode"];
                    
                    var serviceResponse = new InstantChargeResponse(
                        1, //todo aplicar id 
                        responseJson["txid"],
                        DateTime.Parse(responseJson["loc"]["criacao"]),
                        (int)responseJson["calendario"]["expiracao"],
                        Convert.ToDecimal(responseJson["valor"]["original"]),
                        responseJson["status"],
                        responseJson["devedor"]["nome"],
                        responseJson["devedor"]["cpf"],
                        responseJson["chave"],
                        responseQrCodeBs64Json["qrcode"],
                        responseQrCodeBs64Json["imagemQrcode"]);

                    responseList.Add(serviceResponse);
                }
                return responseList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public async Task<IRestResponse> GetQrCode(int loc , string oAuthToken)
        {
            try
            {
                var client = StartClient(_aPIPixSettings.GN_Url_GeraQr);
                var requestOut = ConfigHeader("Bearer " + oAuthToken, Method.GET);
                requestOut.AddUrlSegment("id", loc);
                var response = await client.ExecuteAsync(requestOut);
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        
        public async Task<IRestResponse> GetInstantCharge(InstantCharge content , string oAuthToken)
        {
            try
            {
                var client = StartClient(_aPIPixSettings.GN_Url_CobrancaImediata);
                var requestOut = ConfigHeader("Bearer " + oAuthToken);
                requestOut.AddParameter("application/json", SimpleJson.SerializeObject(content), ParameterType.RequestBody);

                var response = await client.ExecuteAsync(requestOut);
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        
        
    }
}

