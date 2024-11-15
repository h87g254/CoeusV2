using CoeusV2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoeusV2.Database
{
    public class ChatBotDatabase
    {
        private string databaseFile = "Resources/database.db";
        private string connectionString;
        public event EventHandler DataUpdated;

        public ChatBotDatabase()
        {
            connectionString = $"Data Source={databaseFile};Version=3;";
            InitializeDatabase();
            DataUpdated = delegate { };
        }

        // Initializes the database
        protected virtual void OnDataUpdated()
        {
            DataUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(databaseFile))
            {
                SQLiteConnection.CreateFile(databaseFile);
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(
                        "CREATE TABLE Topics (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "Topic TEXT NOT NULL, " +
                        "Text TEXT NOT NULL)", connection);
                    command.ExecuteNonQuery();

                    command = new SQLiteCommand(
                        "CREATE TABLE Categories (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "Category TEXT NOT NULL, " +
                        "Keyword TEXT NOT NULL, " +
                        "Response TEXT NOT NULL)", connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Adds a topic to the database
        public void AddTopic(string topic, string text)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Topics (Topic, Text) VALUES (@topic, @text)", connection);
                command.Parameters.AddWithValue("@topic", topic);
                command.Parameters.AddWithValue("@text", text);
                command.ExecuteNonQuery();

                // Create a table for the subtopics of this topic
                var createTableCommand = new SQLiteCommand($"CREATE TABLE [{topic} Subtopics] (" +
                                                           "Subtopic TEXT NOT NULL, " +
                                                           "Text TEXT NOT NULL)", connection);
                createTableCommand.ExecuteNonQuery();
            }

            OnDataUpdated();
        }

        // Adds a subtopic to the database
        public void AddSubtopic(string topic, string subtopic, string text)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand($"INSERT INTO [{topic} Subtopics] (Subtopic, Text) VALUES (@subtopic, @text)", connection);
                command.Parameters.AddWithValue("@subtopic", subtopic);
                command.Parameters.AddWithValue("@text", text);
                command.ExecuteNonQuery();
            }

            OnDataUpdated();
        }

        // Gets a response for a given user input based on topics
        public string GetTopicResponse(string userInput)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Topic FROM Topics", connection);
                var reader = command.ExecuteReader();
                var topics = new List<string>();

                while (reader.Read())
                {
                    topics.Add(reader.GetString(0));
                }

                string closestTopic = string.Empty;
                int minDistance = 2;

                foreach (var topic in topics)
                {
                    int distance = LevenshteinDistance.Compute(userInput, topic);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestTopic = topic;
                    }
                }

                if (closestTopic != null)
                {
                    command = new SQLiteCommand("SELECT Text FROM Topics WHERE Topic = @topic", connection);
                    command.Parameters.AddWithValue("@topic", closestTopic);
                    var topicText = command.ExecuteScalar()?.ToString();

                    if (topicText != null)
                    {
                        return topicText;
                    }
                }

                // Check for subtopics
                command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name LIKE @tableName", connection);
                command.Parameters.AddWithValue("@tableName", userInput + " Subtopics");
                var tableName = command.ExecuteScalar()?.ToString();

                if (tableName != null)
                {
                    command = new SQLiteCommand($"SELECT Subtopic FROM [{tableName}]", connection);
                    reader = command.ExecuteReader();
                    var subtopics = new List<string>();

                    while (reader.Read())
                    {
                        subtopics.Add(reader.GetString(0));
                    }

                    string closestSubtopic = string.Empty;
                    minDistance = 2;

                    foreach (var subtopic in subtopics)
                    {
                        int distance = LevenshteinDistance.Compute(userInput, subtopic);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestSubtopic = subtopic;
                        }
                    }

                    if (closestSubtopic != null)
                    {
                        command = new SQLiteCommand($"SELECT Text FROM [{tableName}] WHERE Subtopic = @subtopic", connection);
                        command.Parameters.AddWithValue("@subtopic", closestSubtopic);
                        var subtopicText = command.ExecuteScalar()?.ToString();

                        if (subtopicText != null)
                        {
                            return subtopicText;
                        }
                    }
                }

                return string.Empty;
            }
        }

        // Adds a response to the database
        public void AddResponse(string category, string keyword, string response)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Categories (Category, Keyword, Response) VALUES (@category, @keyword, @response)", connection);
                command.Parameters.AddWithValue("@category", category);
                command.Parameters.AddWithValue("@keyword", keyword);
                command.Parameters.AddWithValue("@response", response);
                command.ExecuteNonQuery();
            }

            OnDataUpdated();
        }

        // Gets a response for a given keyword based on categories
        public string GetCategoriesResponse(string keyword)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Keyword FROM Categories", connection);
                var reader = command.ExecuteReader();
                var keywords = new List<string>();

                while (reader.Read())
                {
                    keywords.Add(reader.GetString(0));
                }

                string closestKeyword = string.Empty;
                int minDistance = 2;

                foreach (var k in keywords)
                {
                    int distance = LevenshteinDistance.Compute(keyword, k);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestKeyword = k;
                    }
                }

                if (closestKeyword != null)
                {
                    command = new SQLiteCommand("SELECT Category FROM Categories WHERE Keyword = @keyword", connection);
                    command.Parameters.AddWithValue("@keyword", closestKeyword);
                    var category = command.ExecuteScalar()?.ToString();

                    if (category != null)
                    {
                        command = new SQLiteCommand("SELECT Response FROM Categories WHERE Category = @category ORDER BY RANDOM() LIMIT 1", connection);
                        command.Parameters.AddWithValue("@category", category);
                        var response = command.ExecuteScalar();
                        return response?.ToString()?? string.Empty;
                    }
                }

                return string.Empty;
            }
        }

        // Gets a list of categories from the database
        public List<string> GetCategories()
        {
            var categories = new List<string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT DISTINCT Category FROM Categories", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(reader.GetString(0));
                }
            }
            return categories;
        }

        // Gets a list of topics from the database
        public List<string> GetTopics()
        {
            var topics = new List<string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Topic FROM Topics", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    topics.Add(reader.GetString(0));
                }
            }
            return topics;
        }

        // Gets a list of table names from the database
        public List<string> GetTableNames()
        {
            List<string> tableNames = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Resources/database.db;Version=3;"))
            {
                connection.Open();
                DataTable tables = connection.GetSchema("Tables");

                foreach (DataRow row in tables.Rows)
                {
                    var tableName = row["TABLE_NAME"]?.ToString();
                    if (tableName != null) 
                    {
                        tableNames.Add(tableName);
                    }
                }
            }

            return tableNames;
        }

        // Gets data for a specific table from the database
        public DataTable GetTableData(string tableName)
        {
            DataTable dataTable = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Check if the table exists
                var command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=@tableName", connection);
                command.Parameters.AddWithValue("@tableName", tableName);
                var result = command.ExecuteScalar();

                if (result == null)
                {
                    throw new Exception($"Table '{tableName}' does not exist in the database.");
                }

                // If the table exists, proceed to fill the DataTable
                string query = $"SELECT * FROM {tableName}";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

    }
}
