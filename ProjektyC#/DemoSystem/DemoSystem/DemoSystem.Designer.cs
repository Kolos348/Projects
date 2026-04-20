namespace DemoSystem
{
    partial class DemoSystem
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // mainTimer
            // 
            mainTimer.Enabled = true;
            mainTimer.Interval = 50;
            mainTimer.Tick += mainTimer_Tick;
            // 
            // DemoSystem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 729);
            DoubleBuffered = true;
            Margin = new Padding(2, 1, 2, 1);
            Name = "DemoSystem";
            Text = "Demo system (c) 2025 Logos";
            Paint += DemoSystem_Paint;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer mainTimer;
    }
}
