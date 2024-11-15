namespace CoeusV2.Forms
{
    partial class SettingsForm
    {
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.ComboBox themeComboBox;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.Button saveButton;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            usernameTextBox = new TextBox();
            themeComboBox = new ComboBox();
            previewPanel = new Panel();
            saveButton = new Button();
            SuspendLayout();
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = Color.White;
            usernameTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            usernameTextBox.ForeColor = Color.Black;
            usernameTextBox.Location = new Point(12, 12);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(260, 25);
            usernameTextBox.TabIndex = 0;
            usernameTextBox.Margin = new Padding(10,5,10,5);
            // 
            // themeComboBox
            // 
            themeComboBox.BackColor = Color.White;
            themeComboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            themeComboBox.ForeColor = Color.Black;
            themeComboBox.Items.AddRange(new object[] { "Light", "Dark", "Blue" });
            themeComboBox.Location = new Point(12, 41);
            themeComboBox.Name = "themeComboBox";
            themeComboBox.Size = new Size(260, 25);
            themeComboBox.Margin = new Padding(10, 5, 10, 5);
            themeComboBox.TabIndex = 1;
            themeComboBox.SelectedIndexChanged += ThemeComboBox_SelectedIndexChanged;
            // 
            // previewPanel
            // 
            previewPanel.BackColor = Color.White;
            previewPanel.Location = new Point(12, 70);
            previewPanel.Name = "previewPanel";
            previewPanel.Size = new Size(260, 100);
            previewPanel.Margin = new Padding(10);
            previewPanel.TabIndex = 2;
            // 
            // saveButton
            // 
            saveButton.BackColor = Color.Gray;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(12, 176);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(113, 27);
            saveButton.Margin = new Padding(10,5,10,5);
            saveButton.TabIndex = 3;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += SaveButton_Click;
            // 
            // SettingsForm
            // 
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = Color.White;
            ClientSize = new Size(284, 214);
            Controls.Add(usernameTextBox);
            Controls.Add(themeComboBox);
            Controls.Add(previewPanel);
            Controls.Add(saveButton);
            Name = "SettingsForm";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
