namespace CoeusV2.Forms
{
    partial class MainForm
    {
        private System.Windows.Forms.TextBox userInputTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button adminButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button settingsButton;
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
            userInputTextBox = new TextBox();
            flowLayoutPanel = new FlowLayoutPanel();
            adminButton = new Button();
            sendButton = new Button();
            settingsButton = new Button();
            SuspendLayout();
            // 
            // userInputTextBox
            // 
            userInputTextBox.BackColor = Color.White;
            userInputTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            userInputTextBox.ForeColor = Color.Black;
            userInputTextBox.Location = new Point(12, 400);
            userInputTextBox.Name = "userInputTextBox";
            userInputTextBox.Size = new Size(635, 25);
            userInputTextBox.TabIndex = 0;
            userInputTextBox.KeyDown += UserInputTextBox_KeyDown;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.BackColor = Color.White;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.Location = new Point(12, 12);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(760, 379);
            flowLayoutPanel.TabIndex = 1;
            flowLayoutPanel.WrapContents = false;
            // 
            // adminButton
            // 
            adminButton.BackColor = Color.Gray;
            adminButton.FlatStyle = FlatStyle.Flat;
            adminButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            adminButton.ForeColor = Color.White;
            adminButton.Location = new Point(12, 431);
            adminButton.Name = "adminButton";
            adminButton.Size = new Size(101, 31);
            adminButton.TabIndex = 2;
            adminButton.Text = "Admin";
            adminButton.UseVisualStyleBackColor = false;
            adminButton.Click += AdminButton_Click;
            // 
            // sendButton
            // 
            sendButton.BackColor = Color.DodgerBlue;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            sendButton.ForeColor = Color.White;
            sendButton.Location = new Point(669, 396);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(92, 31);
            sendButton.TabIndex = 3;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = false;
            sendButton.Click += SendButton_Click;
            // 
            // settingsButton
            // 
            settingsButton.BackColor = Color.Gray;
            settingsButton.FlatStyle = FlatStyle.Flat;
            settingsButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            settingsButton.ForeColor = Color.White;
            settingsButton.Location = new Point(119, 431);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(101, 31);
            settingsButton.TabIndex = 4;
            settingsButton.Text = "Settings";
            settingsButton.UseVisualStyleBackColor = false;
            settingsButton.Click += SettingsButton_Click;
            // 
            // MainForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(784, 469);
            Controls.Add(userInputTextBox);
            Controls.Add(flowLayoutPanel);
            Controls.Add(adminButton);
            Controls.Add(sendButton);
            Controls.Add(settingsButton);
            Name = "MainForm";
            Text = "Chatbot";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
