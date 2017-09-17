namespace GraphicNeuralNetwork.Exemple
{
    partial class TestingForm
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
            this.input = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.output = new System.Windows.Forms.Label();
            this.next = new System.Windows.Forms.Button();
            this.run100 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.input)).BeginInit();
            this.SuspendLayout();
            // 
            // input
            // 
            this.input.Location = new System.Drawing.Point(13, 13);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(350, 350);
            this.input.TabIndex = 0;
            this.input.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(479, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "Output :";
            // 
            // output
            // 
            this.output.AutoSize = true;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(479, 189);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(103, 48);
            this.output.TabIndex = 2;
            this.output.Text = "NaN";
            // 
            // next
            // 
            this.next.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.next.Location = new System.Drawing.Point(13, 386);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(163, 94);
            this.next.TabIndex = 3;
            this.next.Text = "Next";
            this.next.UseVisualStyleBackColor = true;
            // 
            // run100
            // 
            this.run100.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.run100.Location = new System.Drawing.Point(182, 386);
            this.run100.Name = "run100";
            this.run100.Size = new System.Drawing.Size(181, 94);
            this.run100.TabIndex = 4;
            this.run100.Text = "Run 100";
            this.run100.UseVisualStyleBackColor = true;
            // 
            // TestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 516);
            this.Controls.Add(this.run100);
            this.Controls.Add(this.next);
            this.Controls.Add(this.output);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.input);
            this.Name = "TestingForm";
            this.Text = "TestingForm";
            ((System.ComponentModel.ISupportInitialize)(this.input)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label output;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button run100;
    }
}