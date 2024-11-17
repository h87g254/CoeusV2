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

                    command = new SQLiteCommand(
                        "CREATE TABLE Subtopics (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "Topic INTEGER NOT NULL, " +
                        "Subtopic TEXT NOT NULL, " +
                        "Text TEXT NOT NULL, " +
                        "FOREIGN KEY (Topic) REFERENCES Topics(Topic))", connection);
                    command.ExecuteNonQuery();

                    command = new SQLiteCommand(
                        "CREATE TABLE IF NOT EXISTS NegativeResponses (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "Response TEXT NOT NULL)", connection);
                    command.ExecuteNonQuery();

                    command = new SQLiteCommand(
                        "CREATE TABLE IF NOT EXISTS PositiveResponses (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "Response TEXT NOT NULL)", connection);
                    command.ExecuteNonQuery();

                    command = new SQLiteCommand(
                        "CREATE TABLE IF NOT EXISTS FollowUpQuestions (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "Question TEXT NOT NULL)", connection);
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
            }

            OnDataUpdated();
        }

        // Adds a subtopic to the database
        public void AddSubtopic(int topicId, string subtopic, string text)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Subtopics (Topic, Subtopic, Text) VALUES (@topic, @subtopic, @text)", connection);
                command.Parameters.AddWithValue("@topic", topicId);
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

                if (!string.IsNullOrEmpty(closestTopic))
                {
                    command = new SQLiteCommand("SELECT Text FROM Topics WHERE Topic = @topic", connection);
                    command.Parameters.AddWithValue("@topic", closestTopic);
                    var topicText = command.ExecuteScalar()?.ToString();

                    if (!string.IsNullOrEmpty(topicText))
                    {
                        return topicText;
                    }

                    // Check for subtopics
                    command = new SQLiteCommand("SELECT Subtopic, Text FROM Subtopics WHERE Topic = @topic", connection);
                    command.Parameters.AddWithValue("@topic", closestTopic);
                    reader = command.ExecuteReader();
                    var subtopics = new List<(string Subtopic, string Text)>();

                    while (reader.Read())
                    {
                        subtopics.Add((reader.GetString(0), reader.GetString(1)));
                    }

                    string closestSubtopic = string.Empty;
                    minDistance = 2;

                    foreach (var subtopic in subtopics)
                    {
                        int distance = LevenshteinDistance.Compute(userInput, subtopic.Subtopic);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestSubtopic = subtopic.Text;
                        }
                    }

                    if (!string.IsNullOrEmpty(closestSubtopic))
                    {
                        return closestSubtopic;
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

        public string GetSubtopicResponse(string subtopic)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Text FROM Subtopics WHERE Subtopic = @subtopic ORDER BY RANDOM() LIMIT 1", connection);
                command.Parameters.AddWithValue("@subtopic", subtopic);
                var result = command.ExecuteScalar();
                return result?.ToString() ?? "I don't have more information on this subtopic.";
            }
        }


        public void AddFollowUpQuestion(string question)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO FollowUpQuestions (Question) VALUES (@question)", connection);
                command.Parameters.AddWithValue("@question", question);
                command.ExecuteNonQuery();
            }
        }

        public void AddPositiveResponse(string response)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO PositiveResponses (Response) VALUES (@response)", connection);
                command.Parameters.AddWithValue("@response", response);
                command.ExecuteNonQuery();
            }
        }

        public void AddNegativeResponse(string response)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO NegativeResponses (Response) VALUES (@response)", connection);
                command.Parameters.AddWithValue("@response", response);
                command.ExecuteNonQuery();
            }
        }

        public string GetFollowUpQuestion()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Question FROM FollowUpQuestions ORDER BY RANDOM() LIMIT 1", connection);
                var result = command.ExecuteScalar();
                return result?.ToString() ?? string.Empty;
            }
        }

        public List<string> GetPositiveResponses()
        {
            var responses = new List<string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Response FROM PositiveResponses", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    responses.Add(reader.GetString(0));
                }
            }
            return responses;
        }

        public List<string> GetNegativeResponses()
        {
            var responses = new List<string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Response FROM NegativeResponses", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    responses.Add(reader.GetString(0));
                }
            }
            return responses;
        }

        public string GetPositiveResponse()
        {
            var positiveResponses = GetPositiveResponses();
            if (positiveResponses != null && positiveResponses.Count > 0)
            {
                // Return a random positive response
                Random random = new Random();
                int index = random.Next(positiveResponses.Count);
                return positiveResponses[index];
            }
            return "I don't have a positive response at the moment.";
        }

        public string GetNegativeResponse()
        {
            var negativeResponses = GetNegativeResponses();
            if (negativeResponses != null && negativeResponses.Count > 0)
            {
                // Return a random negative response
                Random random = new Random();
                int index = random.Next(negativeResponses.Count);
                return negativeResponses[index];
            }
            return "I don't have a negative response at the moment.";
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
                        return response?.ToString() ?? string.Empty;
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
        public List<(int Id, string Name)> GetTopics()
        {
            var topics = new List<(int Id, string Name)>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Id, Topic FROM Topics", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    topics.Add((reader.GetInt32(0), reader.GetString(1)));
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