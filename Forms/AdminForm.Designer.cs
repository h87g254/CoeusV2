﻿namespace CoeusV2.Forms
{
    partial class AdminForm
    {
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.ComboBox topicComboBox;
        private System.Windows.Forms.TextBox topicDescriptionTextBox;
        private System.Windows.Forms.TextBox subtopicTextBox;
        private System.Windows.Forms.TextBox subtopicDescriptionTextBox;
        private System.Windows.Forms.Button addTopicButton;
        private System.Windows.Forms.Button addSubtopicButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox keywordTextBox;
        private System.Windows.Forms.TextBox responseTextBox;
        private System.Windows.Forms.Button addToSubtopicButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox tableComboBox;
        private System.Windows.Forms.TextBox followUpQuestionTextBox;
        private System.Windows.Forms.Button addFollowUpQuestionButton;
        private System.Windows.Forms.TextBox positiveResponseTextBox;
        private System.Windows.Forms.Button addPositiveResponseButton;
        private System.Windows.Forms.TextBox negativeResponseTextBox;
        private System.Windows.Forms.Button addNegativeResponseButton;
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
            categoryComboBox = new ComboBox();
            keywordTextBox = new TextBox();
            responseTextBox = new TextBox();
            addButton = new Button();
            topicComboBox = new ComboBox();
            topicDescriptionTextBox = new TextBox();
            subtopicTextBox = new TextBox();
            subtopicDescriptionTextBox = new TextBox();
            addTopicButton = new Button();
            addSubtopicButton = new Button();
            addToSubtopicButton = new Button();
            dataGridView = new DataGridView();
            tableComboBox = new ComboBox();
            followUpQuestionTextBox = new TextBox();
            addFollowUpQuestionButton = new Button();
            positiveResponseTextBox = new TextBox();
            addPositiveResponseButton = new Button();
            negativeResponseTextBox = new TextBox();
            addNegativeResponseButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // categoryComboBox
            // 
            categoryComboBox.BackColor = Color.White;
            categoryComboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            categoryComboBox.ForeColor = Color.Black;
            categoryComboBox.Items.AddRange(new object[] { "Create new" });
            categoryComboBox.Location = new Point(12, 12);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(260, 25);
            categoryComboBox.TabIndex = 0;
            categoryComboBox.SelectedIndexChanged += CategoryComboBox_SelectedIndexChanged;
            // 
            // keywordTextBox
            // 
            keywordTextBox.BackColor = Color.White;
            keywordTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            keywordTextBox.ForeColor = Color.Black;
            keywordTextBox.Location = new Point(12, 38);
            keywordTextBox.Name = "keywordTextBox";
            keywordTextBox.PlaceholderText = "Keyword";
            keywordTextBox.Size = new Size(260, 25);
            keywordTextBox.TabIndex = 1;
            // 
            // responseTextBox
            // 
            responseTextBox.BackColor = Color.White;
            responseTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            responseTextBox.ForeColor = Color.Black;
            responseTextBox.Location = new Point(12, 64);
            responseTextBox.Name = "responseTextBox";
            responseTextBox.PlaceholderText = "Response";
            responseTextBox.Size = new Size(260, 25);
            responseTextBox.TabIndex = 2;
            // 
            // addButton
            // 
            addButton.BackColor = Color.DodgerBlue;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(12, 95);
            addButton.Name = "addButton";
            addButton.Size = new Size(115, 30);
            addButton.TabIndex = 3;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += AddButton_Click;
            // 
            // topicComboBox
            // 
            topicComboBox.BackColor = Color.White;
            topicComboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            topicComboBox.ForeColor = Color.Black;
            topicComboBox.Items.AddRange(new object[] { "Create new" });
            topicComboBox.Location = new Point(311, 12);
            topicComboBox.Name = "topicComboBox";
            topicComboBox.Size = new Size(260, 25);
            topicComboBox.TabIndex = 4;
            topicComboBox.SelectedIndexChanged += TopicComboBox_SelectedIndexChanged;
            // 
            // topicDescriptionTextBox
            // 
            topicDescriptionTextBox.BackColor = Color.White;
            topicDescriptionTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            topicDescriptionTextBox.ForeColor = Color.Black;
            topicDescriptionTextBox.Location = new Point(311, 38);
            topicDescriptionTextBox.Name = "topicDescriptionTextBox";
            topicDescriptionTextBox.PlaceholderText = "Topic Description";
            topicDescriptionTextBox.Size = new Size(260, 25);
            topicDescriptionTextBox.TabIndex = 5;
            // 
            // subtopicTextBox
            // 
            subtopicTextBox.BackColor = Color.White;
            subtopicTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            subtopicTextBox.ForeColor = Color.Black;
            subtopicTextBox.Location = new Point(311, 64);
            subtopicTextBox.Name = "subtopicTextBox";
            subtopicTextBox.PlaceholderText = "Subtopic";
            subtopicTextBox.Size = new Size(260, 25);
            subtopicTextBox.TabIndex = 6;
            // 
            // subtopicDescriptionTextBox
            // 
            subtopicDescriptionTextBox.BackColor = Color.White;
            subtopicDescriptionTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            subtopicDescriptionTextBox.ForeColor = Color.Black;
            subtopicDescriptionTextBox.Location = new Point(311, 90);
            subtopicDescriptionTextBox.Name = "subtopicDescriptionTextBox";
            subtopicDescriptionTextBox.PlaceholderText = "Subtopic Description";
            subtopicDescriptionTextBox.Size = new Size(260, 25);
            subtopicDescriptionTextBox.TabIndex = 7;
            // 
            // addTopicButton
            // 
            addTopicButton.BackColor = Color.DodgerBlue;
            addTopicButton.FlatStyle = FlatStyle.Flat;
            addTopicButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addTopicButton.ForeColor = Color.White;
            addTopicButton.Location = new Point(311, 121);
            addTopicButton.Name = "addTopicButton";
            addTopicButton.Size = new Size(115, 30);
            addTopicButton.TabIndex = 8;
            addTopicButton.Text = "Add Topic";
            addTopicButton.UseVisualStyleBackColor = false;
            addTopicButton.Click += AddTopicButton_Click;
            // 
            // addSubtopicButton
            // 
            addSubtopicButton.BackColor = Color.DodgerBlue;
            addSubtopicButton.FlatStyle = FlatStyle.Flat;
            addSubtopicButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addSubtopicButton.ForeColor = Color.White;
            addSubtopicButton.Location = new Point(456, 121);
            addSubtopicButton.Name = "addSubtopicButton";
            addSubtopicButton.Size = new Size(115, 30);
            addSubtopicButton.TabIndex = 9;
            addSubtopicButton.Text = "Add Subtopic";
            addSubtopicButton.UseVisualStyleBackColor = false;
            addSubtopicButton.Click += AddToSubtopicButton_Click;
            // 
            // addToSubtopicButton
            // 
            addToSubtopicButton.Location = new Point(0, 0);
            addToSubtopicButton.Name = "addToSubtopicButton";
            addToSubtopicButton.Size = new Size(75, 23);
            addToSubtopicButton.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.BackgroundColor = Color.White;
            dataGridView.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridView.Location = new Point(12, 328);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(559, 200);
            dataGridView.TabIndex = 10;
            // 
            // tableComboBox
            // 
            tableComboBox.BackColor = Color.White;
            tableComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tableComboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            tableComboBox.ForeColor = Color.Black;
            tableComboBox.Location = new Point(12, 297);
            tableComboBox.Name = "tableComboBox";
            tableComboBox.Size = new Size(260, 25);
            tableComboBox.TabIndex = 11;
            tableComboBox.SelectedIndexChanged += TableComboBox_SelectedIndexChanged;
            // 
            // followUpQuestionTextBox
            // 
            followUpQuestionTextBox.BackColor = Color.White;
            followUpQuestionTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            followUpQuestionTextBox.ForeColor = Color.Black;
            followUpQuestionTextBox.Location = new Point(311, 157);
            followUpQuestionTextBox.Name = "followUpQuestionTextBox";
            followUpQuestionTextBox.PlaceholderText = "Follow-up Question";
            followUpQuestionTextBox.Size = new Size(260, 25);
            followUpQuestionTextBox.TabIndex = 10;
            // 
            // addFollowUpQuestionButton
            // 
            addFollowUpQuestionButton.BackColor = Color.DodgerBlue;
            addFollowUpQuestionButton.ForeColor = Color.White;
            addFollowUpQuestionButton.FlatStyle = FlatStyle.Flat;
            addFollowUpQuestionButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addFollowUpQuestionButton.Location = new Point(311, 188);
            addFollowUpQuestionButton.Name = "addFollowUpQuestionButton";
            addFollowUpQuestionButton.Size = new Size(115, 30);
            addFollowUpQuestionButton.TabIndex = 11;
            addFollowUpQuestionButton.Text = "Add Question";
            addFollowUpQuestionButton.UseVisualStyleBackColor = false;
            addFollowUpQuestionButton.Click += AddFollowUpQuestionButton_Click;
            // 
            // positiveResponseTextBox
            // 
            positiveResponseTextBox.BackColor = Color.White;
            positiveResponseTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            positiveResponseTextBox.ForeColor = Color.Black;
            positiveResponseTextBox.Location = new Point(12, 131);
            positiveResponseTextBox.Name = "positiveResponseTextBox";
            positiveResponseTextBox.PlaceholderText = "Positive Response";
            positiveResponseTextBox.Size = new Size(200, 25);
            positiveResponseTextBox.TabIndex = 12;
            // 
            // addPositiveResponseButton
            // 
            addPositiveResponseButton.BackColor = Color.DodgerBlue;
            addPositiveResponseButton.ForeColor = Color.White;
            addPositiveResponseButton.FlatStyle = FlatStyle.Flat;
            addPositiveResponseButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addPositiveResponseButton.Location = new Point(12, 161);
            addPositiveResponseButton.Name = "addPositiveResponseButton";
            addPositiveResponseButton.Size = new Size(115, 30);
            addPositiveResponseButton.TabIndex = 13;
            addPositiveResponseButton.Text = "Add Positive";
            addPositiveResponseButton.UseVisualStyleBackColor = false;
            addPositiveResponseButton.Click += AddPositiveResponseButton_Click;
            // 
            // negativeResponseTextBox
            // 
            negativeResponseTextBox.BackColor = Color.White;
            negativeResponseTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            negativeResponseTextBox.ForeColor = Color.Black;
            negativeResponseTextBox.Location = new Point(12, 197);
            negativeResponseTextBox.Name = "negativeResponseTextBox";
            negativeResponseTextBox.PlaceholderText = "Negative Response";
            negativeResponseTextBox.Size = new Size(200, 25);
            negativeResponseTextBox.TabIndex = 14;
            // 
            // addNegativeResponseButton
            // 
            addNegativeResponseButton.BackColor = Color.DodgerBlue;
            addNegativeResponseButton.ForeColor = Color.White;
            addNegativeResponseButton.FlatStyle = FlatStyle.Flat;
            addNegativeResponseButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addNegativeResponseButton.Location = new Point(12, 228);
            addNegativeResponseButton.Name = "addNegativeResponseButton";
            addNegativeResponseButton.Size = new Size(115, 30);
            addNegativeResponseButton.TabIndex = 15;
            addNegativeResponseButton.Text = "Add Negative";
            addNegativeResponseButton.UseVisualStyleBackColor = false;
            addNegativeResponseButton.Click += AddNegativeResponseButton_Click;
            // 
            // AdminForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(586, 540);
            Controls.Add(categoryComboBox);
            Controls.Add(keywordTextBox);
            Controls.Add(responseTextBox);
            Controls.Add(addButton);
            Controls.Add(topicComboBox);
            Controls.Add(topicDescriptionTextBox);
            Controls.Add(subtopicTextBox);
            Controls.Add(subtopicDescriptionTextBox);
            Controls.Add(addTopicButton);
            Controls.Add(addSubtopicButton);
            Controls.Add(dataGridView);
            Controls.Add(tableComboBox);
            Controls.Add(followUpQuestionTextBox);
            Controls.Add(addFollowUpQuestionButton);
            Controls.Add(positiveResponseTextBox);
            Controls.Add(addPositiveResponseButton);
            Controls.Add(negativeResponseTextBox);
            Controls.Add(addNegativeResponseButton);
            Name = "AdminForm";
            Text = "Admin Panel";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
