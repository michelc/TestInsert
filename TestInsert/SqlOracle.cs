using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using Dapper;

namespace TestInsert
{
    class SqlOracle
    {
        private IDbConnection connexion;

        public SqlOracle()
        {
            connexion = GetConnection();
        }

        private IDbConnection GetConnection()
        {
            var name = "Oracle_Production";
            var settings = ConfigurationManager.ConnectionStrings[name];

            var factory = DbProviderFactories.GetFactory(settings.ProviderName);

            var conn = factory.CreateConnection();
            conn.ConnectionString = settings.ConnectionString;

            return conn;
        }

        public int ExecuteSql(string sql)
        {
            int data = 0;
            try
            {
                connexion.Open();
                data = connexion.Execute(sql);
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

        public int SaveClientsDb2(IEnumerable<Client> clients)
        {
            var sql = @"INSERT INTO Mcr_Clients_Db2
                          (CodeCle, CodeAgence, CodeClient, Siret, RaisonSociale, Adresse, CodePostal, Ville, Telephone, Type)
                        VALUES
                          (:CodeCle, :CodeAgence, :CodeClient, :Siret, :RaisonSociale, :Adresse, :CodePostal, :Ville, :Telephone, :Type)";

            int count = 0;
            try
            {
                connexion.Open();
                foreach (var client in clients)
                {
                    connexion.Execute(sql, client);
                    count++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connexion.Close();
            }

            return count;
        }

        public int SaveClientsDb2_BeginEnd(IEnumerable<Client> clients)
        {
            var sql = @"INSERT INTO Mcr_Clients_Db2 (CodeCle, CodeAgence, CodeClient, Siret, RaisonSociale, Adresse, CodePostal, Ville, Telephone, Type) VALUES (~{0}~, ~{1}~, ~{2}~, ~{3}~, ~{4}~, ~{5}~, ~{6}~, ~{7}~, ~{8}~, ~{9}~); ";

            int count = 0;
            try
            {
                connexion.Open();
                var batch = new StringBuilder();
                foreach (var client in clients)
                {
                    batch.Append(string.Format(sql, client.CodeCle, client.CodeAgence, client.CodeClient, client.Siret, client.RaisonSociale, client.Adresse, client.CodePostal, client.Ville, client.Telephone, client.Type));
                    count++;
                    if ((count % 500) == 0)
                    {
                        connexion.Execute(Sql_BeginEnd(batch).ToString());
                        batch = new StringBuilder();
                    }
                }
                connexion.Execute(Sql_BeginEnd(batch).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connexion.Close();
            }

            return count;
        }

        private StringBuilder Sql_BeginEnd(StringBuilder batch)
        {
            batch.Insert(0, "BEGIN ");
            batch.Append(" END;");
            batch.Replace("'", "''");
            batch.Replace("~", "'");
            return batch;
        }

        public int SaveClientsDb2_FromSelect(IEnumerable<Client> clients, bool append, string table)
        {
            var sql = @"UNION SELECT ~{0}~, ~{1}~, ~{2}~, ~{3}~, ~{4}~, ~{5}~, ~{6}~, ~{7}~, ~{8}~, ~{9}~ FROM DUAL ";

            int count = 0;
            try
            {
                connexion.Open();
                var batch = new StringBuilder();
                foreach (var client in clients)
                {
                    batch.Append(string.Format(sql, client.CodeCle, client.CodeAgence, client.CodeClient, client.Siret, client.RaisonSociale, client.Adresse, client.CodePostal, client.Ville, client.Telephone, client.Type));
                    count++;
                    if ((count % 500) == 0)
                    {
                        connexion.Execute(Sql_FromSelect(batch, append, table).ToString());
                        batch = new StringBuilder();
                    }
                }
                connexion.Execute(Sql_FromSelect(batch, append, table).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connexion.Close();
            }

            return count;
        }

        private StringBuilder Sql_FromSelect(StringBuilder batch, bool append, string table)
        {
            var start = @"BEGIN INSERT /*+ append */ INTO @{table_name} (CodeCle, CodeAgence, CodeClient, Siret, RaisonSociale, Adresse, CodePostal, Ville, Telephone, Type) ";
            batch.Remove(0, 5);
            batch.Insert(0, start);
            batch.Append("; END;");
            batch.Replace("'", "''");
            batch.Replace("~", "'");
            batch.Replace("@{table_name}", table);
            if (!append) batch.Replace(" /*+ append */ ", " ");
            return batch;
        }

    }
}