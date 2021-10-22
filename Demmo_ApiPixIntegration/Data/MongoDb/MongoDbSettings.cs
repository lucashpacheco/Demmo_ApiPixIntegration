namespace Demmo_ApiPixIntegration.Data.MongoDb
{
    /// <summary>
    /// Configurações do MongoDb armazenadas no appsettings.json.
    /// </summary>
    public class MongoDbSettings
    {

        /// <summary>
        /// String de conexão.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Nome do banco de dados.
        /// </summary>
        public string Database { get; set; }

    }
}
