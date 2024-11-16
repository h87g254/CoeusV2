using System.Data.SQLite;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using CoeusV2.Database;
using CoeusV2.Utils;
using CoeusV2.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace CoeusV2.Forms
{
    public partial class MainForm : Form
    {
        private ChatBotDatabase database;
        private string username = "User";
        private string selectedTheme = "Light";
        private List<string> userInputHistory = new List<string>();
        private int historyIndex = -1;

        public MainForm()
        {
            InitializeComponent();
            database = new ChatBotDatabase();
            ApplyTheme(selectedTheme);
        }

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage();
                userInputHistory.Add(userInputTextBox.Text);
                historyIndex = userInputHistory.Count;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (historyIndex > 0)
                {
                    historyIndex--;
                    userInputTextBox.Text = userInputHistory[historyIndex];
                    userInputTextBox.SelectionStart = userInputTextBox.Text.Length;
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (historyIndex < userInputHistory.Count - 1)
                {
                    historyIndex++;
                    userInputTextBox.Text = userInputHistory[historyIndex];
                    userInputTextBox.SelectionStart = userInputTextBox.Text.Length;
                }
                e.Handled = true;
            }
        }


        // Handles the Admin button click event to open the AdminForm
        private void AdminButton_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm(database);
            adminForm.Show();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void userInputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private string currentTopic = string.Empty;
        // Handles the Send button click event to send a message
        private async void SendMessage()
        {
            string userInput = userInputTextBox.Text;
            if (string.IsNullOrWhiteSpace(userInput)) return;

            AddMessageToChat(username, userInput, Color.DodgerBlue, Color.White);

            // Show typing indicator
            var typingIndicator = new Label
            {
                Text = "Coeus is typing...",
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                AutoSize = true
            };
            flowLayoutPanel.Controls.Add(typingIndicator);
            flowLayoutPanel.ScrollControlIntoView(typingIndicator); // Auto-scroll to the typing indicator
            await Task.Delay(2500); // Simulate typing delay

            string botResponse = string.Empty;

            if (!string.IsNullOrEmpty(currentTopic))
            {
                // Check if the user response is positive or negative
                if (IsPositiveResponse(userInput))
                {
                    // Get subtopic response
                    string subtopicResponse = database.GetSubtopicResponse(currentTopic);
                    if (!string.IsNullOrEmpty(subtopicResponse))
                    {
                        botResponse = subtopicResponse;
                    }
                    else
                    {
                        botResponse = "I don't have more information on this topic.";
                    }
                }
                else if (IsNegativeResponse(userInput))
                {
                    botResponse = "Okay, let me know if you have any other questions.";
                    currentTopic = string.Empty; // Reset current topic
                }
            }
            else
            {
                string categoriesResponse = database.GetCategoriesResponse(userInput);
                string topicResponse = database.GetTopicResponse(userInput);

                if (!string.IsNullOrEmpty(categoriesResponse) && string.IsNullOrEmpty(topicResponse))
                {
                    botResponse = categoriesResponse;
                }
                else if (!string.IsNullOrEmpty(topicResponse) && string.IsNullOrEmpty(categoriesResponse))
                {
                    botResponse = topicResponse;
                    currentTopic = userInput; // Set current topic

                    // Append follow-up question to the topic response
                    string followUpQuestion = database.GetFollowUpQuestion();
                    if (!string.IsNullOrEmpty(followUpQuestion))
                    {
                        botResponse += "\n\n" + followUpQuestion;
                    }
                }
                else
                {
                    botResponse = "I don't understand.";
                }
            }

            // Remove typing indicator
            flowLayoutPanel.Controls.Remove(typingIndicator);

            AddMessageToChat("Coeus", botResponse, Color.LightGray, Color.Black);

            userInputTextBox.Clear();
        }

        private bool IsPositiveResponse(string userInput)
        {
            var positiveResponses = database.GetPositiveResponses();
            return positiveResponses.Any(response => userInput.ToLower().Contains(response.ToLower()));
        }

        private bool IsNegativeResponse(string userInput)
        {
            var negativeResponses = database.GetNegativeResponses();
            return negativeResponses.Any(response => userInput.ToLower().Contains(response.ToLower()));
        }

        private void AddMessageToChat(string sender, string message, Color backColor, Color foreColor)
        {
            if (string.IsNullOrEmpty(sender)) throw new ArgumentException("Sender cannot be null or empty", nameof(sender));
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("Message cannot be null or empty", nameof(message));

            const int charLimit = 70;
            StringBuilder formattedMessage = new StringBuilder();

            while (message.Length > charLimit)
            {
                int splitIndex = message.LastIndexOf(' ', charLimit);
                if (splitIndex == -1) splitIndex = charLimit;
                formattedMessage.AppendLine(message.Substring(0, splitIndex));
                message = message.Substring(splitIndex).Trim();
            }
            formattedMessage.AppendLine(message);

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            Label messageLabel = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(flowLayoutPanel.Width - 40, 0),
                Text = $"{timestamp} {sender}: {formattedMessage}",
                Padding = new Padding(10),
                Margin = new Padding(5),
                BackColor = backColor,
                ForeColor = foreColor,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10),
                AutoEllipsis = true
            };

            // Add rounded corners
            messageLabel.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                Rectangle r = messageLabel.ClientRectangle;
                r.Inflate(-1, -1);
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 20;
                    path.AddArc(r.Left, r.Top, radius, radius, 180, 90);
                    path.AddArc(r.Right - radius, r.Top, radius, radius, 270, 90);
                    path.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(r.Left, r.Bottom - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Brush brush = new SolidBrush(backColor))
                    {
                        g.FillPath(brush, path);
                    }
                }
                TextRenderer.DrawText(g, messageLabel.Text, messageLabel.Font, r, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            };

            flowLayoutPanel.Controls.Add(messageLabel);
            flowLayoutPanel.ScrollControlIntoView(messageLabel);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm(username, selectedTheme))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Apply settings
                    username = settingsForm.Username;
                    selectedTheme = settingsForm.SelectedTheme;

                    // Update UI based on selected theme
                    ApplyTheme(selectedTheme);
                }
            }
        }

        private void ApplyTheme(string theme)
        {
            switch (theme)
            {
                case "Light":
                    BackColor = Color.White;
                    ForeColor = Color.Black;
                    break;
                case "Dark":
                    BackColor = Color.Black;
                    ForeColor = Color.White;
                    break;
                case "Blue":
                    BackColor = Color.DodgerBlue;
                    ForeColor = Color.White;
                    break;
            }
        }
    }
}
