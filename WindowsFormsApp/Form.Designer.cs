namespace WindowsFormsApp
{
    partial class Form
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.selectData_button = new System.Windows.Forms.Button();
            this.loadData_button = new System.Windows.Forms.Button();
            this.chartGyroscope = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.turnsInfo = new System.Windows.Forms.Label();
            this.circularProgressBar = new CircularProgressBar.CircularProgressBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.selectedFile = new System.Windows.Forms.TextBox();
            this.leftGyroX = new System.Windows.Forms.CheckBox();
            this.leftGyroY = new System.Windows.Forms.CheckBox();
            this.leftGyroZ = new System.Windows.Forms.CheckBox();
            this.rightGyroX = new System.Windows.Forms.CheckBox();
            this.rightGyroY = new System.Windows.Forms.CheckBox();
            this.rightGyroZ = new System.Windows.Forms.CheckBox();
            this.turnsLegend = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartGyroscope)).BeginInit();
            this.SuspendLayout();
            // 
            // selectData_button
            // 
            this.selectData_button.Location = new System.Drawing.Point(31, 28);
            this.selectData_button.Name = "selectData_button";
            this.selectData_button.Size = new System.Drawing.Size(148, 54);
            this.selectData_button.TabIndex = 4;
            this.selectData_button.Text = "Select sensors data";
            this.selectData_button.UseVisualStyleBackColor = true;
            this.selectData_button.Click += new System.EventHandler(this.SelectDataButton);
            // 
            // loadData_button
            // 
            this.loadData_button.Location = new System.Drawing.Point(216, 28);
            this.loadData_button.Name = "loadData_button";
            this.loadData_button.Size = new System.Drawing.Size(148, 54);
            this.loadData_button.TabIndex = 5;
            this.loadData_button.Text = "Detect ski-turns";
            this.loadData_button.UseVisualStyleBackColor = true;
            this.loadData_button.Click += new System.EventHandler(this.DetectTurnsButton);
            // 
            // chartGyroscope
            // 
            this.chartGyroscope.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartGyroscope.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "ChartArea2";
            chartArea3.Name = "ChartArea3";
            this.chartGyroscope.ChartAreas.Add(chartArea1);
            this.chartGyroscope.ChartAreas.Add(chartArea2);
            this.chartGyroscope.ChartAreas.Add(chartArea3);
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Enabled = false;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            legend2.DockedToChartArea = "ChartArea2";
            legend2.Enabled = false;
            legend2.IsDockedInsideChartArea = false;
            legend2.Name = "Legend2";
            legend3.DockedToChartArea = "ChartArea3";
            legend3.Enabled = false;
            legend3.Name = "Legend3";
            this.chartGyroscope.Legends.Add(legend1);
            this.chartGyroscope.Legends.Add(legend2);
            this.chartGyroscope.Legends.Add(legend3);
            this.chartGyroscope.Location = new System.Drawing.Point(31, 140);
            this.chartGyroscope.Name = "chartGyroscope";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.DarkOrange;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Blue;
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            series4.ChartArea = "ChartArea3";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Red;
            series4.Legend = "Legend3";
            series4.Name = "Series4";
            series5.ChartArea = "ChartArea2";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series5.Legend = "Legend2";
            series5.Name = "Series5";
            series6.ChartArea = "ChartArea2";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.DarkOrange;
            series6.IsVisibleInLegend = false;
            series6.Legend = "Legend2";
            series6.Name = "Series6";
            series7.ChartArea = "ChartArea2";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Blue;
            series7.Legend = "Legend2";
            series7.Name = "Series7";
            this.chartGyroscope.Series.Add(series1);
            this.chartGyroscope.Series.Add(series2);
            this.chartGyroscope.Series.Add(series3);
            this.chartGyroscope.Series.Add(series4);
            this.chartGyroscope.Series.Add(series5);
            this.chartGyroscope.Series.Add(series6);
            this.chartGyroscope.Series.Add(series7);
            this.chartGyroscope.Size = new System.Drawing.Size(906, 481);
            this.chartGyroscope.TabIndex = 6;
            this.chartGyroscope.Text = "chart1";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            title1.IsDockedInsideChartArea = false;
            title1.Name = "Title1";
            title1.Text = "Filtered gyroscope signals";
            title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title2.DockedToChartArea = "ChartArea2";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            title2.IsDockedInsideChartArea = false;
            title2.Name = "Title2";
            title2.Text = "Classified ski-turns";
            title3.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title3.DockedToChartArea = "ChartArea3";
            title3.IsDockedInsideChartArea = false;
            title3.Name = "Title3";
            this.chartGyroscope.Titles.Add(title1);
            this.chartGyroscope.Titles.Add(title2);
            this.chartGyroscope.Titles.Add(title3);
            // 
            // turnsInfo
            // 
            this.turnsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.turnsInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.turnsInfo.ForeColor = System.Drawing.Color.Red;
            this.turnsInfo.Location = new System.Drawing.Point(27, 610);
            this.turnsInfo.Name = "turnsInfo";
            this.turnsInfo.Size = new System.Drawing.Size(1072, 38);
            this.turnsInfo.TabIndex = 7;
            this.turnsInfo.Text = "turnsInfo";
            this.turnsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // circularProgressBar
            // 
            this.circularProgressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.circularProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.circularProgressBar.AnimationSpeed = 500;
            this.circularProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.circularProgressBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.circularProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.circularProgressBar.InnerColor = System.Drawing.SystemColors.Control;
            this.circularProgressBar.InnerMargin = 0;
            this.circularProgressBar.InnerWidth = -2;
            this.circularProgressBar.Location = new System.Drawing.Point(494, 289);
            this.circularProgressBar.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar.Name = "circularProgressBar";
            this.circularProgressBar.OuterColor = System.Drawing.SystemColors.ButtonShadow;
            this.circularProgressBar.OuterMargin = -25;
            this.circularProgressBar.OuterWidth = 25;
            this.circularProgressBar.ProgressColor = System.Drawing.Color.BlueViolet;
            this.circularProgressBar.ProgressWidth = 13;
            this.circularProgressBar.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.circularProgressBar.Size = new System.Drawing.Size(100, 100);
            this.circularProgressBar.StartAngle = 290;
            this.circularProgressBar.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.circularProgressBar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.circularProgressBar.SubscriptText = ".23";
            this.circularProgressBar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.circularProgressBar.SuperscriptText = "°C";
            this.circularProgressBar.TabIndex = 8;
            this.circularProgressBar.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.circularProgressBar.Value = 60;
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // selectedFile
            // 
            this.selectedFile.Enabled = false;
            this.selectedFile.Location = new System.Drawing.Point(31, 99);
            this.selectedFile.MinimumSize = new System.Drawing.Size(333, 22);
            this.selectedFile.Multiline = true;
            this.selectedFile.Name = "selectedFile";
            this.selectedFile.ReadOnly = true;
            this.selectedFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.selectedFile.Size = new System.Drawing.Size(333, 22);
            this.selectedFile.TabIndex = 14;
            // 
            // leftGyroX
            // 
            this.leftGyroX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.leftGyroX.AutoSize = true;
            this.leftGyroX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.leftGyroX.Location = new System.Drawing.Point(1023, 167);
            this.leftGyroX.Name = "leftGyroX";
            this.leftGyroX.Size = new System.Drawing.Size(89, 21);
            this.leftGyroX.TabIndex = 18;
            this.leftGyroX.Text = "leftGyroX";
            this.leftGyroX.UseVisualStyleBackColor = true;
            this.leftGyroX.CheckedChanged += new System.EventHandler(this.leftGyroX_CheckedChanged);
            // 
            // leftGyroY
            // 
            this.leftGyroY.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.leftGyroY.AutoSize = true;
            this.leftGyroY.ForeColor = System.Drawing.Color.DarkOrange;
            this.leftGyroY.Location = new System.Drawing.Point(1023, 194);
            this.leftGyroY.Name = "leftGyroY";
            this.leftGyroY.Size = new System.Drawing.Size(89, 21);
            this.leftGyroY.TabIndex = 19;
            this.leftGyroY.Text = "leftGyroY";
            this.leftGyroY.UseVisualStyleBackColor = true;
            this.leftGyroY.CheckedChanged += new System.EventHandler(this.leftGyroY_CheckedChanged);
            // 
            // leftGyroZ
            // 
            this.leftGyroZ.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.leftGyroZ.AutoSize = true;
            this.leftGyroZ.ForeColor = System.Drawing.Color.Blue;
            this.leftGyroZ.Location = new System.Drawing.Point(1023, 221);
            this.leftGyroZ.Name = "leftGyroZ";
            this.leftGyroZ.Size = new System.Drawing.Size(89, 21);
            this.leftGyroZ.TabIndex = 20;
            this.leftGyroZ.Text = "leftGyroZ";
            this.leftGyroZ.UseVisualStyleBackColor = true;
            this.leftGyroZ.CheckedChanged += new System.EventHandler(this.leftGyroZ_CheckedChanged);
            // 
            // rightGyroX
            // 
            this.rightGyroX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rightGyroX.AutoSize = true;
            this.rightGyroX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rightGyroX.Location = new System.Drawing.Point(1023, 341);
            this.rightGyroX.Name = "rightGyroX";
            this.rightGyroX.Size = new System.Drawing.Size(98, 21);
            this.rightGyroX.TabIndex = 21;
            this.rightGyroX.Text = "rightGyroX";
            this.rightGyroX.UseVisualStyleBackColor = true;
            this.rightGyroX.CheckedChanged += new System.EventHandler(this.rightGyroX_CheckedChanged);
            // 
            // rightGyroY
            // 
            this.rightGyroY.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rightGyroY.AutoSize = true;
            this.rightGyroY.ForeColor = System.Drawing.Color.DarkOrange;
            this.rightGyroY.Location = new System.Drawing.Point(1023, 368);
            this.rightGyroY.Name = "rightGyroY";
            this.rightGyroY.Size = new System.Drawing.Size(98, 21);
            this.rightGyroY.TabIndex = 22;
            this.rightGyroY.Text = "rightGyroY";
            this.rightGyroY.UseVisualStyleBackColor = true;
            this.rightGyroY.CheckedChanged += new System.EventHandler(this.rightGyroY_CheckedChanged);
            // 
            // rightGyroZ
            // 
            this.rightGyroZ.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rightGyroZ.AutoSize = true;
            this.rightGyroZ.ForeColor = System.Drawing.Color.Blue;
            this.rightGyroZ.Location = new System.Drawing.Point(1023, 395);
            this.rightGyroZ.Name = "rightGyroZ";
            this.rightGyroZ.Size = new System.Drawing.Size(98, 21);
            this.rightGyroZ.TabIndex = 23;
            this.rightGyroZ.Text = "rightGyroZ";
            this.rightGyroZ.UseVisualStyleBackColor = true;
            this.rightGyroZ.CheckedChanged += new System.EventHandler(this.rightGyroZ_CheckedChanged);
            // 
            // turnsLegend
            // 
            this.turnsLegend.AutoSize = true;
            this.turnsLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.turnsLegend.ForeColor = System.Drawing.Color.Red;
            this.turnsLegend.Location = new System.Drawing.Point(1040, 500);
            this.turnsLegend.Name = "turnsLegend";
            this.turnsLegend.Size = new System.Drawing.Size(99, 20);
            this.turnsLegend.TabIndex = 24;
            this.turnsLegend.Text = "turnsLegend";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 679);
            this.Controls.Add(this.turnsLegend);
            this.Controls.Add(this.rightGyroZ);
            this.Controls.Add(this.rightGyroY);
            this.Controls.Add(this.rightGyroX);
            this.Controls.Add(this.leftGyroZ);
            this.Controls.Add(this.leftGyroY);
            this.Controls.Add(this.leftGyroX);
            this.Controls.Add(this.selectedFile);
            this.Controls.Add(this.circularProgressBar);
            this.Controls.Add(this.turnsInfo);
            this.Controls.Add(this.chartGyroscope);
            this.Controls.Add(this.loadData_button);
            this.Controls.Add(this.selectData_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form";
            this.Text = "Ski-turns classifier";
            this.Resize += new System.EventHandler(this.Form_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chartGyroscope)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button selectData_button;
        private System.Windows.Forms.Button loadData_button;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGyroscope;
        private System.Windows.Forms.Label turnsInfo;
        private System.Windows.Forms.Timer timer;
        private CircularProgressBar.CircularProgressBar circularProgressBar;
        private System.Windows.Forms.TextBox selectedFile;
        private System.Windows.Forms.CheckBox leftGyroX;
        private System.Windows.Forms.CheckBox leftGyroY;
        private System.Windows.Forms.CheckBox leftGyroZ;
        private System.Windows.Forms.CheckBox rightGyroX;
        private System.Windows.Forms.CheckBox rightGyroY;
        private System.Windows.Forms.CheckBox rightGyroZ;
        private System.Windows.Forms.Label turnsLegend;
    }
}

