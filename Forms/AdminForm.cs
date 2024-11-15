using System;
using System.Data;
using System.Windows.Forms;
using CoeusV2.Database;

namespace CoeusV2.Forms
{
    public partial class AdminForm : Form
    {
        private ChatBotDatabase database;

        public AdminForm(ChatBotDatabase database)
        {
            InitializeComponent();
            this.database = database;
            LoadCategories();
            LoadTopics();
            LoadTableNames();
            database.DataUpdated += ChatBotDatabase_DataUpdated;
        }

        // Loads categories from the database
        private void LoadCategories()
        {
            var categories = database.GetCategories();
            categoryComboBox.Items.AddRange(categories.ToArray());
        }

        // Loads topics from the database
        private void LoadTopics()
        {
            var topics = database.GetTopics();
            topicComboBox.Items.AddRange(topics.ToArray());
        }

        // Loads table names from the database
        private void LoadTableNames()
        {
            var tableNames = database.GetTableNames();
            tableComboBox.Items.AddRange(tableNames.ToArray());
        }

        // Loads data for a specific table
        private void LoadTableData(string tableName)
        {
            var dataTable = database.GetTableData(tableName);
            dataGridView.DataSource = dataTable;
        }

        // Event handler for table combo box selection change
        private void TableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTable = tableComboBox.SelectedItem.ToString();
            LoadTableData(selectedTable);
        }

        // Event handler for category combo box selection change
        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedItem.ToString() == "Create new")
            {
                categoryComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                categoryComboBox.Text = "";
            }
            else
            {
                categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        // Event handler for topic combo box selection change
        private void TopicComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (topicComboBox.SelectedItem.ToString() == "Create new")
            {
                topicComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                topicComboBox.Text = "";
            }
            else
            {
                topicComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        // Event handler for Add button click
        private void AddButton_Click(object sender, EventArgs e)
        {
            string category = categoryComboBox.Text;
            string keyword = keywordTextBox.Text;
            string response = responseTextBox.Text;

            if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(response))
            {
                database.AddResponse(category, keyword, response);
                MessageBox.Show("Response added successfully!");
                categoryComboBox.Text = "";
                keywordTextBox.Clear();
                responseTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }

        // Event handler for Add Topic button click
        private void AddTopicButton_Click(object sender, EventArgs e)
        {
            string topic = topicComboBox.Text;
            string text = topicDescriptionTextBox.Text;

            if (!string.IsNullOrEmpty(topic) && !string.IsNullOrEmpty(text))
            {
                database.AddTopic(topic, text);
                MessageBox.Show("Topic added successfully!");
                topicComboBox.Text = "";
                topicDescriptionTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }

        // Event handler for Add to Subtopic button click
        private void AddToSubtopicButton_Click(object sender, EventArgs e)
        {
            string topic = topicComboBox.Text;
            string subtopic = subtopicTextBox.Text;
            string text = subtopicDescriptionTextBox.Text;

            if (!string.IsNullOrEmpty(topic) && !string.IsNullOrEmpty(subtopic) && !string.IsNullOrEmpty(text))
            {
                database.AddSubtopic(topic, subtopic, text);
                MessageBox.Show("Subtopic added successfully!");
                subtopicTextBox.Clear();
                subtopicDescriptionTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }

        // Event handler for database data updated event
        private void ChatBotDatabase_DataUpdated(object sender, EventArgs e)
        {
            LoadTableData();
        }

        // Loads table data
        private void LoadTableData()
        {
            var dataTable = database.GetTableData("Categories");
            dataGridView.DataSource = dataTable;
        }
    }
}