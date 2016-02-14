using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using static System.Console;

namespace ConnectionSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            switch (args[0])
            {
                case "-o":
                    OpenConnection();
                    break;
                case "-c":
                    ConnectionUsingConfig();
                    break;
                case "-i":
                    ConnectionInformation();
                    break;
                case "-t":
                    Transactions();
                    break;
                default:
                    ShowUsage();
                    break;
            }


            ReadLine();
        }

        public static void ShowUsage()
        {
            WriteLine("ConnectionSamples command");
            WriteLine("\t-o\tOpen Connection");
            WriteLine("\t-c\tUse Configuration File");
            WriteLine("\t-i\tConnection Information");
            WriteLine("\t-t\tTransactions");
        }



        public static void OpenConnection()
        {
            string connectionString = @"server=(localdb)\MSSQLLocalDB;" +
                            "integrated security=SSPI;" +
                            "database=AdventureWorks2014";
            var connection = new SqlConnection(connectionString);

            connection.Open();

            // Do something useful
            WriteLine("connection opened");

            connection.Close();
        }

        public static void ConnectionUsingConfig()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("config.json");
            IConfiguration config = configurationBuilder.Build();
         
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            WriteLine(connectionString);
        }

        public static string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("config.json");

            IConfiguration config = configurationBuilder.Build();
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            return connectionString;
        }

        public static void ConnectionInformation()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.InfoMessage += (sender, e) =>
                {
                    WriteLine($"warning or info {e.Message}");
                };
                connection.StateChange += (sender, e) =>
                {

                    WriteLine($"current state: {e.CurrentState}, before: {e.OriginalState}");
                };
                connection.Open();

                WriteLine("connection opened");
                // Do something useful

            }
        }

        public static void Transactions()
        {
            string connectionString = GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction tx = connection.BeginTransaction();
                tx.Save("one");

                tx.Commit();
            }

        }

    }
}
