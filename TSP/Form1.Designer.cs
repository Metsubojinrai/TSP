namespace TSP
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numCross = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.numMutate = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numPop = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numGeneration = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.btnData = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tourDiagram = new System.Windows.Forms.PictureBox();
            this.tourCityListTextBox = new System.Windows.Forms.TextBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.labelLastSolution = new System.Windows.Forms.Label();
            this.iterationLabel = new System.Windows.Forms.Label();
            this.drawnTourLengthLabel = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMutate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGeneration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tourDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numCross);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.numMutate);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.numPop);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.numGeneration);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(6, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(178, 220);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parameters";
            // 
            // numCross
            // 
            this.numCross.DecimalPlaces = 2;
            this.numCross.Location = new System.Drawing.Point(2, 136);
            this.numCross.Name = "numCross";
            this.numCross.Size = new System.Drawing.Size(163, 20);
            this.numCross.TabIndex = 6;
            this.numCross.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1, 120);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Crossover (%)";
            // 
            // numMutate
            // 
            this.numMutate.DecimalPlaces = 2;
            this.numMutate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numMutate.Location = new System.Drawing.Point(3, 184);
            this.numMutate.Name = "numMutate";
            this.numMutate.Size = new System.Drawing.Size(163, 20);
            this.numMutate.TabIndex = 4;
            this.numMutate.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Mutation (%)";
            // 
            // numPop
            // 
            this.numPop.Location = new System.Drawing.Point(3, 91);
            this.numPop.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numPop.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPop.Name = "numPop";
            this.numPop.Size = new System.Drawing.Size(163, 20);
            this.numPop.TabIndex = 2;
            this.numPop.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Popultion Size (>1)";
            // 
            // numGeneration
            // 
            this.numGeneration.Location = new System.Drawing.Point(3, 36);
            this.numGeneration.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numGeneration.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numGeneration.Name = "numGeneration";
            this.numGeneration.Size = new System.Drawing.Size(163, 20);
            this.numGeneration.TabIndex = 1;
            this.numGeneration.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(0, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(105, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Generations limit (>1)";
            // 
            // btnData
            // 
            this.btnData.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnData.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnData.Location = new System.Drawing.Point(21, 338);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(107, 35);
            this.btnData.TabIndex = 42;
            this.btnData.Text = "Data";
            this.btnData.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.ForeColor = System.Drawing.Color.Blue;
            this.btnStart.Location = new System.Drawing.Point(19, 398);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(109, 37);
            this.btnStart.TabIndex = 44;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tourDiagram
            // 
            this.tourDiagram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tourDiagram.BackColor = System.Drawing.Color.White;
            this.tourDiagram.Location = new System.Drawing.Point(190, 12);
            this.tourDiagram.Name = "tourDiagram";
            this.tourDiagram.Size = new System.Drawing.Size(500, 520);
            this.tourDiagram.TabIndex = 52;
            this.tourDiagram.TabStop = false;
            // 
            // tourCityListTextBox
            // 
            this.tourCityListTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tourCityListTextBox.Location = new System.Drawing.Point(707, 12);
            this.tourCityListTextBox.Multiline = true;
            this.tourCityListTextBox.Name = "tourCityListTextBox";
            this.tourCityListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tourCityListTextBox.Size = new System.Drawing.Size(100, 361);
            this.tourCityListTextBox.TabIndex = 53;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.Location = new System.Drawing.Point(707, 385);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 54;
            this.btnShow.Text = "Show Tour";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // labelLastSolution
            // 
            this.labelLastSolution.AutoSize = true;
            this.labelLastSolution.Location = new System.Drawing.Point(3, 284);
            this.labelLastSolution.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLastSolution.Name = "labelLastSolution";
            this.labelLastSolution.Size = new System.Drawing.Size(123, 13);
            this.labelLastSolution.TabIndex = 57;
            this.labelLastSolution.Text = "Best solution found @: 0";
            // 
            // iterationLabel
            // 
            this.iterationLabel.AutoSize = true;
            this.iterationLabel.Location = new System.Drawing.Point(5, 262);
            this.iterationLabel.Name = "iterationLabel";
            this.iterationLabel.Size = new System.Drawing.Size(57, 13);
            this.iterationLabel.TabIndex = 56;
            this.iterationLabel.Text = "Iteration: 0";
            // 
            // drawnTourLengthLabel
            // 
            this.drawnTourLengthLabel.AutoSize = true;
            this.drawnTourLengthLabel.Location = new System.Drawing.Point(3, 237);
            this.drawnTourLengthLabel.Name = "drawnTourLengthLabel";
            this.drawnTourLengthLabel.Size = new System.Drawing.Size(103, 13);
            this.drawnTourLengthLabel.TabIndex = 55;
            this.drawnTourLengthLabel.Text = "Drawn tour length: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 544);
            this.Controls.Add(this.labelLastSolution);
            this.Controls.Add(this.iterationLabel);
            this.Controls.Add(this.drawnTourLengthLabel);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.tourCityListTextBox);
            this.Controls.Add(this.tourDiagram);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form1";
            this.Text = "Travelling Salesman Problem";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMutate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGeneration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tourDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numCross;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numMutate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numPop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numGeneration;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox tourDiagram;
        private System.Windows.Forms.TextBox tourCityListTextBox;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label labelLastSolution;
        private System.Windows.Forms.Label iterationLabel;
        private System.Windows.Forms.Label drawnTourLengthLabel;
    }
}

