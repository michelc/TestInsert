using System;
using System.Linq;
using System.Diagnostics;

namespace TestInsert
{
    class Program
    {

        static void Main(string[] args)
        {
            CopyDb2_Version1a();
            CopyDb2_Version1b();
            CopyDb2_Version2a();
            CopyDb2_Version2b();
            CopyDb2_Version3a();
            CopyDb2_Version3b();
            CopyDb2_Version4a();
            CopyDb2_Version4b();

            Console.Write("Appuyer sur <Entrée> pour quitter... ");
            Console.ReadLine();
        }

        static void CopyDb2_Version1a()
        {
            Console.WriteLine("INSERT avec LOGGING");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 LOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));
            
            Console.WriteLine();
        }

        static void CopyDb2_Version1b()
        {
            Console.WriteLine("INSERT avec NOLOGGING");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 NOLOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            Console.WriteLine();
        }

        static void CopyDb2_Version2a()
        {
            Console.WriteLine("INSERT avec LOGGING + BEGIN / END");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 LOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2_BeginEnd(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2_BeginEnd(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            Console.WriteLine();
        }

        static void CopyDb2_Version2b()
        {
            Console.WriteLine("INSERT avec NOLOGGING + BEGIN / END");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 NOLOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2_BeginEnd(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2_BeginEnd(clients);
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            Console.WriteLine();
        }

        static void CopyDb2_Version3a()
        {
            Console.WriteLine("INSERT FROM SELECT avec LOGGING");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 LOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2_FromSelect(clients, false, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2_FromSelect(clients, false, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            Console.WriteLine();
        }

        static void CopyDb2_Version3b()
        {
            Console.WriteLine("INSERT FROM SELECT avec NOLOGGING");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 NOLOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2_FromSelect(clients, false, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2_FromSelect(clients, false, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            Console.WriteLine();
        }

        static void CopyDb2_Version4a()
        {
            Console.WriteLine("APPEND FROM SELECT avec LOGGING");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 LOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2_FromSelect(clients, true, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2_FromSelect(clients, true, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            Console.WriteLine();
        }

        static void CopyDb2_Version4b()
        {
            Console.WriteLine("APPEND FROM SELECT avec NOLOGGING");
            var db2 = new SqlDb2();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var clients = db2.LoadClients();
            stopwatch.Stop();
            Console.WriteLine(string.Format("* Chargement => {0} lignes en {1:0.00} secondes", clients.ToList().Count(), stopwatch.ElapsedMilliseconds / 1000.0));

            var oracle = new SqlOracle();
            oracle.ExecuteSql("ALTER TABLE Mcr_Clients_Db2 NOLOGGING");

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            var count = oracle.SaveClientsDb2_FromSelect(clients, true, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 1° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            oracle.ExecuteSql("TRUNCATE TABLE Mcr_Clients_Db2");
            stopwatch = new Stopwatch();
            stopwatch.Start();
            count = oracle.SaveClientsDb2_FromSelect(clients, true, "Mcr_Clients_Db2");
            stopwatch.Stop();
            Console.WriteLine(string.Format("* 2° Sauvegarde => {0} lignes en {1:0.00} secondes", count, stopwatch.ElapsedMilliseconds / 1000.0));

            Console.WriteLine();
        }

    }
}