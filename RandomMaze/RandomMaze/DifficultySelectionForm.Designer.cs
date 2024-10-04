namespace RandomMaze
{
    partial class DifficultySelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DifficultySelectionForm));
            this.btnEasy = new System.Windows.Forms.Button();
            this.btnMedium = new System.Windows.Forms.Button();
            this.btnHard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEasy
            // 
            this.btnEasy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEasy.BackgroundImage")));
            this.btnEasy.Font = new System.Drawing.Font("Russo One", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnEasy.Location = new System.Drawing.Point(104, 49);
            this.btnEasy.Name = "btnEasy";
            this.btnEasy.Size = new System.Drawing.Size(140, 51);
            this.btnEasy.TabIndex = 0;
            this.btnEasy.Text = "Easy";
            this.btnEasy.UseVisualStyleBackColor = true;
            this.btnEasy.Click += new System.EventHandler(this.btnEasy_Click);
            // 
            // btnMedium
            // 
            this.btnMedium.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMedium.BackgroundImage")));
            this.btnMedium.Font = new System.Drawing.Font("Russo One", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnMedium.Location = new System.Drawing.Point(104, 106);
            this.btnMedium.Name = "btnMedium";
            this.btnMedium.Size = new System.Drawing.Size(140, 51);
            this.btnMedium.TabIndex = 1;
            this.btnMedium.Text = "Medium";
            this.btnMedium.UseVisualStyleBackColor = true;
            this.btnMedium.Click += new System.EventHandler(this.btnMedium_Click);
            // 
            // btnHard
            // 
            this.btnHard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHard.BackgroundImage")));
            this.btnHard.Font = new System.Drawing.Font("Russo One", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnHard.Location = new System.Drawing.Point(104, 163);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(140, 51);
            this.btnHard.TabIndex = 2;
            this.btnHard.Text = "Hard";
            this.btnHard.UseVisualStyleBackColor = true;
            this.btnHard.Click += new System.EventHandler(this.btnHard_Click);
            // 
            // DifficultySelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(349, 270);
            this.Controls.Add(this.btnHard);
            this.Controls.Add(this.btnMedium);
            this.Controls.Add(this.btnEasy);
            this.Name = "DifficultySelectionForm";
            this.Text = "DifficultySelectionForm";
            this.Load += new System.EventHandler(this.DifficultySelectionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEasy;
        private System.Windows.Forms.Button btnMedium;
        private System.Windows.Forms.Button btnHard;
    }
}