using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Dapper;

namespace TestInsert
{
    public class SqlDb2
    {
        private IDbConnection connexion;

        public SqlDb2()
        {
            connexion = GetConnection();
        }

        private IDbConnection GetConnection()
        {
            var name = "Db2_Production";
            var settings = ConfigurationManager.ConnectionStrings[name];

            var factory = DbProviderFactories.GetFactory(settings.ProviderName);

            var conn = factory.CreateConnection();
            conn.ConnectionString = settings.ConnectionString;

            return conn;
        }

        public IEnumerable<Client> LoadClients()
        {
            IEnumerable<Client> data = null;

            var sql = @"SELECT TIAAGE || TIACLI AS CodeCle, 
                               TIAAGE AS CodeAgence, 
                               TIACLI AS CodeClient, 
                               TIARSO AS RaisonSociale, 
                               TIASIR AS Siret, 
                               TIARU1 AS Adresse, 
                               TIACPO AS CodePostal, 
                               TIAVIL AS Ville, 
                               TIATLP AS Telephone,
                               SUBSTR(TIAIND, 1, 1) AS Type
                        FROM   INSTIAP
                        WHERE  TIASOC = '001'
                        AND    TIAIND IN ('CL1', 'PR1')
                        AND    TIAAGE = '001'
                        AND    TIACLI IS NOT NULL
                        ORDER BY 5, 1";

            try
            {
                connexion.Open();
                data = connexion.Query<Client>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connexion.Close();
            }

            return data;
        }
    }
}
