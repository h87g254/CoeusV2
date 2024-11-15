using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CoeusV2.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(string currentUsername, string currentTheme)
        {
            InitializeComponent();
            usernameTextBox.Text = currentUsername;
            themeComboBox.SelectedItem = currentTheme;
        }

        // Event handler for theme combo box selection change
        private void ThemeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (themeComboBox.SelectedItem.ToString())
            {
                case "Light":
                    previewPanel.BackColor = Color.White;
                    break;
                case "Dark":
                    previewPanel.BackColor = Color.Black;
                    break;
                case "Blue":
                    previewPanel.BackColor = Color.DodgerBlue;
                    break;
            }
        }

        // Event handler for Save button click
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Username = usernameTextBox.Text;
            SelectedTheme = themeComboBox.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        public string Username { get; private set; }
        public string SelectedTheme { get; private set; }
    }
}
