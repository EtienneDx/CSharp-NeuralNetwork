namespace GraphicNeuralNetwork.Exemple
{
    partial class NetworkForm
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
            this.progress = new System.Windows.Forms.Label();
            this.networkPanel = new System.Windows.Forms.Panel();
            this.stopTraining = new System.Windows.Forms.Button();
            this.startTraining = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.toJson = new System.Windows.Forms.Button();
            this.testNetwork = new System.Windows.Forms.Button();
            this.costGraph = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.AutoSize = true;
            this.progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progress.Location = new System.Drawing.Point(12, 621);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(187, 48);
            this.progress.TabIndex = 0;
            this.progress.Text = "Progress";
            // 
            // networkPanel
            // 
            this.networkPanel.Location = new System.Drawing.Point(197, 12);
            this.networkPanel.Name = "networkPanel";
            this.networkPanel.Size = new System.Drawing.Size(1274, 606);
            this.networkPanel.TabIndex = 1;
            // 
            // stopTraining
            // 
            this.stopTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopTraining.Location = new System.Drawing.Point(13, 97);
            this.stopTraining.Name = "stopTraining";
            this.stopTraining.Size = new System.Drawing.Size(178, 79);
            this.stopTraining.TabIndex = 2;
            this.stopTraining.Text = "Stop Training";
            this.stopTraining.UseVisualStyleBackColor = true;
            // 
            // startTraining
            // 
            this.startTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startTraining.Location = new System.Drawing.Point(13, 12);
            this.startTraining.Name = "startTraining";
            this.startTraining.Size = new System.Drawing.Size(178, 79);
            this.startTraining.TabIndex = 3;
            this.startTraining.Text = "Start Training";
            this.startTraining.UseVisualStyleBackColor = true;
            // 
            // refresh
            // 
            this.refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh.Location = new System.Drawing.Point(13, 182);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(178, 79);
            this.refresh.TabIndex = 13;
            this.refresh.Text = "Refresh visual";
            this.refresh.UseVisualStyleBackColor = true;
            // 
            // toJson
            // 
            this.toJson.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toJson.Location = new System.Drawing.Point(13, 267);
            this.toJson.Name = "toJson";
            this.toJson.Size = new System.Drawing.Size(178, 79);
            this.toJson.TabIndex = 14;
            this.toJson.Text = "Save to Json";
            this.toJson.UseVisualStyleBackColor = true;
            // 
            // testNetwork
            // 
            this.testNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testNetwork.Location = new System.Drawing.Point(13, 352);
            this.testNetwork.Name = "testNetwork";
            this.testNetwork.Size = new System.Drawing.Size(178, 79);
            this.testNetwork.TabIndex = 15;
            this.testNetwork.Text = "Test network";
            this.testNetwork.UseVisualStyleBackColor = true;
            // 
            // costGraph
            // 
            this.costGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costGraph.Location = new System.Drawing.Point(13, 437);
            this.costGraph.Name = "costGraph";
            this.costGraph.Size = new System.Drawing.Size(178, 79);
            this.costGraph.TabIndex = 16;
            this.costGraph.Text = "See Cost Graph";
            this.costGraph.UseVisualStyleBackColor = true;
            // 
            // NetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1483, 733);
            this.Controls.Add(this.costGraph);
            this.Controls.Add(this.testNetwork);
            this.Controls.Add(this.toJson);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.startTraining);
            this.Controls.Add(this.stopTraining);
            this.Controls.Add(this.networkPanel);
            this.Controls.Add(this.progress);
            this.Name = "NetworkForm";
            this.Text = "NetworkForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label progress;
        private System.Windows.Forms.Panel networkPanel;
        private System.Windows.Forms.Button stopTraining;
        private System.Windows.Forms.Button startTraining;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Button toJson;
        private System.Windows.Forms.Button testNetwork;
        private System.Windows.Forms.Button costGraph;
    }
}