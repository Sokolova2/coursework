namespace coursework
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ExitButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.InsertInOrdersButton = new System.Windows.Forms.Button();
            this.InsertDishesButton = new System.Windows.Forms.Button();
            this.UpdateDishesButton = new System.Windows.Forms.Button();
            this.DeleteDishesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.CausesValidation = false;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 25.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(997, 95);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "\t\t\t\t\t\t\tМеню";
            this.richTextBox1.UseWaitCursor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(12, 101);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(733, 460);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ExitButton.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExitButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.ExitButton.Location = new System.Drawing.Point(957, 0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(40, 37);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "X";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BackButton.Location = new System.Drawing.Point(0, 0);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(56, 36);
            this.BackButton.TabIndex = 6;
            this.BackButton.Text = "←";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click_1);
            // 
            // InsertInOrdersButton
            // 
            this.InsertInOrdersButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.InsertInOrdersButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InsertInOrdersButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.InsertInOrdersButton.Location = new System.Drawing.Point(780, 510);
            this.InsertInOrdersButton.Name = "InsertInOrdersButton";
            this.InsertInOrdersButton.Size = new System.Drawing.Size(203, 51);
            this.InsertInOrdersButton.TabIndex = 5;
            this.InsertInOrdersButton.Text = "Додати в замовлення";
            this.InsertInOrdersButton.UseVisualStyleBackColor = false;
            this.InsertInOrdersButton.Click += new System.EventHandler(this.InsertInOrdersButton_Click);
            // 
            // InsertDishesButton
            // 
            this.InsertDishesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.InsertDishesButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InsertDishesButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.InsertDishesButton.Location = new System.Drawing.Point(780, 119);
            this.InsertDishesButton.Name = "InsertDishesButton";
            this.InsertDishesButton.Size = new System.Drawing.Size(203, 51);
            this.InsertDishesButton.TabIndex = 7;
            this.InsertDishesButton.Text = "Додати нове блюдо";
            this.InsertDishesButton.UseVisualStyleBackColor = false;
            this.InsertDishesButton.Click += new System.EventHandler(this.InsertDishesButton_Click);
            // 
            // UpdateDishesButton
            // 
            this.UpdateDishesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.UpdateDishesButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UpdateDishesButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.UpdateDishesButton.Location = new System.Drawing.Point(780, 214);
            this.UpdateDishesButton.Name = "UpdateDishesButton";
            this.UpdateDishesButton.Size = new System.Drawing.Size(203, 51);
            this.UpdateDishesButton.TabIndex = 8;
            this.UpdateDishesButton.Text = "Оновити меню";
            this.UpdateDishesButton.UseVisualStyleBackColor = false;
            this.UpdateDishesButton.Click += new System.EventHandler(this.UpdateDishesButton_Click);
            // 
            // DeleteDishesButton
            // 
            this.DeleteDishesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.DeleteDishesButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteDishesButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.DeleteDishesButton.Location = new System.Drawing.Point(780, 310);
            this.DeleteDishesButton.Name = "DeleteDishesButton";
            this.DeleteDishesButton.Size = new System.Drawing.Size(203, 51);
            this.DeleteDishesButton.TabIndex = 9;
            this.DeleteDishesButton.Text = "Видалити ";
            this.DeleteDishesButton.UseVisualStyleBackColor = false;
            this.DeleteDishesButton.Click += new System.EventHandler(this.DeleteDishesButton_Click_1);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(995, 573);
            this.Controls.Add(this.DeleteDishesButton);
            this.Controls.Add(this.UpdateDishesButton);
            this.Controls.Add(this.InsertDishesButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.InsertInOrdersButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Menu";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button InsertInOrdersButton;
        private System.Windows.Forms.Button InsertDishesButton;
        private System.Windows.Forms.Button UpdateDishesButton;
        private System.Windows.Forms.Button DeleteDishesButton;
    }
}

