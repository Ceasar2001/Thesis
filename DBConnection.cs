//using System;
//using System.Data.SQLite;
//using System.IO;

//namespace TeacherPortal
//{
//    internal class DBConnection
//    {
//        private readonly string connectionString;
//        public static string _title = "Teacher Portal";

//        public DBConnection()
//        {
//            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
//            string databasePath = Path.Combine(baseDirectory, "TeacherPortalsDB.db");

//            if (!File.Exists(databasePath))
//            {
//                Console.Error.WriteLine($"Database file not found at: {databasePath}");
//                throw new FileNotFoundException($"Database file not found at: {databasePath}");
//            }

//            connectionString = $"Data Source={databasePath};Version=3;";
//            Console.WriteLine($"Using Database Path: {databasePath}");
//        }

//        public SQLiteConnection GetConnection
//        {
//            get
//            {
//                try
//                {
//                    var connection = new SQLiteConnection(connectionString);
//                    connection.Open();
//                    Console.WriteLine("SQLite connection opened successfully!");
//                    return connection;
//                }
//                catch (Exception ex)
//                {
//                    Console.Error.WriteLine($"Error creating SQLite connection: {ex.Message}");
//                    throw;
//                }
//            }
//        }
//    }
//}

using System;
using System.Data.SQLite;
using System.IO;

namespace TeacherPortal
{
    internal class DBConnection
    {
        private readonly string connectionString;
        public static string _title = "Teacher Portal";
        public string _aycode = "";

        public DBConnection()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string databasePath = Path.Combine(baseDirectory, "TeacherPortalsDB.db");

            // Ensure that the database file exists
            if (!File.Exists(databasePath))
            {
                Console.Error.WriteLine($"Database file not found at: {databasePath}");
                throw new FileNotFoundException($"Database file not found at: {databasePath}");
            }

            connectionString = $"Data Source={databasePath};Version=3;";
            Console.WriteLine($"Using Database Path: {databasePath}");
        }

        // Return the connection object without opening it here
        public SQLiteConnection GetConnection
        {
            get
            {
                return new SQLiteConnection(connectionString); // Return connection but don't open it
            }
        }
    }
}


// if the system is in deployment mode
//using System;
//using System.Data.SQLite;
//using System.IO;

//namespace TeacherPortal
//{
//    internal class DBConnection
//    {
//        private readonly string connectionString;
//        public static string _title = "Teacher Portal";
//        public string _aycode = "";

//        public DBConnection()
//        {
//            // Get the AppData path for the current user
//            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TeacherPortal");

//            // Ensure the directory exists
//            if (!Directory.Exists(appDataPath))
//            {
//                Directory.CreateDirectory(appDataPath);
//            }

//            // Set the full database path
//            string databasePath = Path.Combine(appDataPath, "TeacherPortalsDB.db");

//            // If the database does not exist, create it
//            if (!File.Exists(databasePath))
//            {
//                SQLiteConnection.CreateFile(databasePath);
//                Console.WriteLine("New database file created at: " + databasePath);
//            }

//            connectionString = $"Data Source={databasePath};Version=3;";
//            Console.WriteLine($"Using Database Path: {databasePath}");
//        }

//        // Return the connection object without opening it here
//        public SQLiteConnection GetConnection
//        {
//            get
//            {
//                return new SQLiteConnection(connectionString); // Return connection but don't open it
//            }
//        }
//    }
//}
