namespace GraphicNeuralNetwork.Exemple
{
    partial class Graph
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
            this.chart = new System.Windows.Forms.Panel();
            this.labels = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Location = new System.Drawing.Point(176, 12);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(1018, 508);
            this.chart.TabIndex = 0;
            // 
            // labels
            // 
            this.labels.Location = new System.Drawing.Point(12, 12);
            this.labels.Name = "labels";
            this.labels.Size = new System.Drawing.Size(146, 508);
            this.labels.TabIndex = 1;
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 532);
            this.Controls.Add(this.labels);
            this.Controls.Add(this.chart);
            this.Name = "Graph";
            this.Text = "Graph";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel chart;
        private System.Windows.Forms.Panel labels;
    }
}