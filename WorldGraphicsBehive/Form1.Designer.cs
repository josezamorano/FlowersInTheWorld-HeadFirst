namespace WorldGraphicsBehive
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.FlowerTimer = new System.Windows.Forms.Timer(this.components);
            this.LabelFormHeight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FlowerTimer
            // 
            this.FlowerTimer.Tick += new System.EventHandler(this.FlowerTimer_Tick);
            // 
            // LabelFormHeight
            // 
            this.LabelFormHeight.AutoSize = true;
            this.LabelFormHeight.Location = new System.Drawing.Point(13, 13);
            this.LabelFormHeight.Name = "LabelFormHeight";
            this.LabelFormHeight.Size = new System.Drawing.Size(29, 13);
            this.LabelFormHeight.TabIndex = 0;
            this.LabelFormHeight.Text = "label";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 451);
            this.Controls.Add(this.LabelFormHeight);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer FlowerTimer;
        private System.Windows.Forms.Label LabelFormHeight;

    }
}

