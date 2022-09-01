using System;
using System.Data;
using Npgsql;

namespace connect_toposgres
{
    public class Program
    {
        // Obtain connection string information from the portal
        //
        private static string Host = "localhost";
        private static string User = "user";
        private static string DBname = "network";
        private static string Password = "user123";
        private static string Port = "5432";
        static void Main(string[] args)

        {
            GetValues();
            AddValues();
        }

        public static NpgsqlConnection GetConx()
        {

            // Build connection string using parameters from portal
            string connString =
                String.Format(
                    "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);

            var conn = new NpgsqlConnection(connString);
            return conn;
        }


        public static void GetValues()
        {
            var conx = GetConx();
            Console.WriteLine("Choose 'C' for Citizens or 'D' for Districts");
            string choice = Console.ReadLine();
            string query_option = "";

            if (choice == "C")
            {
                query_option = Citizens.Query;
            }
            else if (choice == "D")
            {
                query_option = Districts.Query;
            }
            else
            {
                Console.WriteLine("Enter a valid option.");
            }

            using (conx)
            {
                Console.WriteLine("Opening connection");
                conx.Open();


                using (var command = new NpgsqlCommand(query_option, conx))
                {

                    var reader = command.ExecuteReader();
                    int fieldCount = reader.FieldCount;
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            string.Format(
                                "Reading from table={0}, {1}, {2}, {3}",
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetInt32(2),
                                reader.GetInt32(3)
                                )
                            );
                    }

                    reader.Close();
                }
            }
        }

        public static void AddValues()
        {
            var conx = GetConx();
            Console.WriteLine("Choose 'T' to insert values in Districts, 'I' to insert values in Citizens, or 'E' to exit");
            string choice = Console.ReadLine();

            using (conx)
            {
                Console.WriteLine("Opening connection");
                conx.Open();

                if (choice == "T")
                {
                    var dis = new Districts();
                    Console.WriteLine("Enter District ID:");
                    dis.dis_id = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter District Name:");
                    dis.dis_name = Console.ReadLine();

                    var sql = "INSERT INTO Districts(dis_id, dis_name) VALUES(@dis_id, @dis_name)";
                    using (var cmd = new NpgsqlCommand(sql, conx))
                    {
                        cmd.Parameters.AddWithValue("@dis_id", dis.dis_id);
                        cmd.Parameters.AddWithValue("@dis_name", dis.dis_name);

                        cmd.ExecuteNonQuery();
                    }
                }

                else if (choice == "I")
                {
                    var cit = new Citizens();
                    Console.WriteLine("Enter Citizen ID:");
                    cit.citiz_id = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Citizen Name:");
                    cit.citiz_name = Console.ReadLine();
                    Console.WriteLine("Enter Citizen Number:");
                    cit.citiz_nbr = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter District ID:");
                    cit.dis_id = double.Parse(Console.ReadLine());

                    var sql = "INSERT INTO Citizens(citiz_id, citiz_name, citiz_nbr, dis_id) VALUES(@citiz_id, @citiz_name, @citiz_nbr, @dis_id)";
                    using (var cmd = new NpgsqlCommand(sql, conx))
                    {
                        cmd.Parameters.AddWithValue("@citiz_id", cit.citiz_id);
                        cmd.Parameters.AddWithValue("@citiz_name", cit.citiz_name);
                        cmd.Parameters.AddWithValue("@citiz_nbr", cit.citiz_nbr);
                        cmd.Parameters.AddWithValue("@dis_id", cit.dis_id);

                        cmd.ExecuteNonQuery();
                    }
                }

                else if (choice == "E")
                {
                    return;
                }

                else
                {
                    Console.WriteLine("Enter a valid option.");
                }

            }
        }
    }
}