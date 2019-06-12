
namespace OneRainGraph
{
    partial class Form1 
    {
        /*public Form1()
        {   
            InitializeComponent();   
        }  */ 
    

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
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel1 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel2 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataSet1 = new OneRainGraph.DataSet1();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.gboxOneRain = new System.Windows.Forms.GroupBox();
            this.rbGetOneRain = new OneRainGraph.RoundButton();
            this.tTipGetOneRain = new System.Windows.Forms.ToolTip(this.components);
            this.rbSavePlot = new OneRainGraph.RoundButton();
            this.rbPublishToDashboard = new OneRainGraph.RoundButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgressBar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.gboxOneRain.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.AxisX.CustomLabels.Add(customLabel1);
            chartArea1.AxisX.CustomLabels.Add(customLabel2);
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsLogarithmic = true;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.Maximum = 100D;
            chartArea1.AxisX.Minimum = 0.1D;
            chartArea1.AxisX.Title = "Duration (hours)";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX2.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisX2.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.Maximum = 14D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "Maximum Rainfall Depth (inches)";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "DDF Curve";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.AutoFitMinFontSize = 9;
            legend1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.ItemColumnSpacing = 60;
            legend1.MaximumAutoSize = 80F;
            legend1.Name = "Legend1";
            legend1.TextWrapThreshold = 35;
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(28, 18);
            this.chart1.Name = "chart1";
            series1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series1.BorderWidth = 2;
            series1.ChartArea = "DDF Curve";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(255)))));
            series1.CustomProperties = "IsXAxisQuantitative=True";
            series1.Legend = "Legend1";
            series1.Name = "FDOT 2-Year (50%)";
            series1.ToolTip = "FDOT 2-Year (50%)";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series2.BorderWidth = 2;
            series2.ChartArea = "DDF Curve";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(255)))));
            series2.CustomProperties = "IsXAxisQuantitative=True";
            series2.Legend = "Legend1";
            series2.Name = "FDOT 5-Year (20%)";
            series2.ToolTip = "FDOT 5-Year (20%)";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series3.BorderWidth = 2;
            series3.ChartArea = "DDF Curve";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            series3.CustomProperties = "IsXAxisQuantitative=True";
            series3.Legend = "Legend1";
            series3.Name = "FDOT 10-Year (10%)";
            series3.ToolTip = "FDOT 10-Year (10%)";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series4.BorderWidth = 2;
            series4.ChartArea = "DDF Curve";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            series4.CustomProperties = "IsXAxisQuantitative=True";
            series4.Legend = "Legend1";
            series4.Name = "FDOT 25-Year (4%)";
            series4.ToolTip = "FDOT 25-Year (4%)";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series5.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series5.BorderWidth = 2;
            series5.ChartArea = "DDF Curve";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            series5.CustomProperties = "IsXAxisQuantitative=True";
            series5.Legend = "Legend1";
            series5.Name = "FDOT 50-Year (2%)";
            series5.ToolTip = "FDOT 50-Year (2%)";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series5.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series6.BorderWidth = 2;
            series6.ChartArea = "DDF Curve";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Color = System.Drawing.Color.Red;
            series6.CustomProperties = "IsXAxisQuantitative=True";
            series6.Legend = "Legend1";
            series6.Name = "FDOT 100-Year (1%)";
            series6.ToolTip = "FDOT 100-Year (1%)";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series6.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(941, 457);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(6, 33);
            this.startDatePicker.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.startDatePicker.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(157, 20);
            this.startDatePicker.TabIndex = 1;
            this.startDatePicker.Value = new System.DateTime(2018, 11, 30, 10, 0, 0, 0);
            this.startDatePicker.ValueChanged += new System.EventHandler(this.startDatePicker_ValueChanged);
            // 
            // endDatePicker
            // 
            this.endDatePicker.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(167, 33);
            this.endDatePicker.MaxDate = new System.DateTime(2050, 6, 6, 0, 0, 0, 0);
            this.endDatePicker.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.RightToLeftLayout = true;
            this.endDatePicker.Size = new System.Drawing.Size(157, 20);
            this.endDatePicker.TabIndex = 2;
            this.endDatePicker.Value = new System.DateTime(2018, 12, 3, 10, 14, 0, 0);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(6, 16);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(74, 17);
            this.lblStartDate.TabIndex = 3;
            this.lblStartDate.Text = "Start Date";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(164, 16);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(64, 17);
            this.lblEndDate.TabIndex = 4;
            this.lblEndDate.Text = "End Date";
            // 
            // gboxOneRain
            // 
            this.gboxOneRain.Controls.Add(this.rbGetOneRain);
            this.gboxOneRain.Controls.Add(this.lblEndDate);
            this.gboxOneRain.Controls.Add(this.lblStartDate);
            this.gboxOneRain.Controls.Add(this.endDatePicker);
            this.gboxOneRain.Controls.Add(this.startDatePicker);
            this.gboxOneRain.Location = new System.Drawing.Point(26, 480);
            this.gboxOneRain.Name = "gboxOneRain";
            this.gboxOneRain.Size = new System.Drawing.Size(481, 62);
            this.gboxOneRain.TabIndex = 6;
            this.gboxOneRain.TabStop = false;
            this.gboxOneRain.Text = "OneRain";
            // 
            // rbGetOneRain
            // 
            this.rbGetOneRain.BackColor = System.Drawing.Color.Transparent;
            this.rbGetOneRain.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGetOneRain.Location = new System.Drawing.Point(332, 19);
            this.rbGetOneRain.Name = "rbGetOneRain";
            this.rbGetOneRain.Size = new System.Drawing.Size(141, 36);
            this.rbGetOneRain.TabIndex = 7;
            this.rbGetOneRain.Text = "Get OneRain Data";
            this.tTipGetOneRain.SetToolTip(this.rbGetOneRain, "Get OneRain data for your selected dates");
            this.rbGetOneRain.UseVisualStyleBackColor = false;
            this.rbGetOneRain.Click += new System.EventHandler(this.rbGetOneRain_Click);
            // 
            // tTipGetOneRain
            // 
            this.tTipGetOneRain.BackColor = System.Drawing.SystemColors.InactiveCaption;
            // 
            // rbSavePlot
            // 
            this.rbSavePlot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbSavePlot.BackgroundImage")));
            this.rbSavePlot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rbSavePlot.Location = new System.Drawing.Point(821, 492);
            this.rbSavePlot.Name = "rbSavePlot";
            this.rbSavePlot.Size = new System.Drawing.Size(47, 43);
            this.rbSavePlot.TabIndex = 8;
            this.tTipGetOneRain.SetToolTip(this.rbSavePlot, "Save a local copy of the graph and data");
            this.rbSavePlot.UseVisualStyleBackColor = true;
            this.rbSavePlot.Click += new System.EventHandler(this.rbSavePlot_Click);
            // 
            // rbPublishToDashboard
            // 
            this.rbPublishToDashboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbPublishToDashboard.BackgroundImage")));
            this.rbPublishToDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rbPublishToDashboard.Location = new System.Drawing.Point(911, 492);
            this.rbPublishToDashboard.Name = "rbPublishToDashboard";
            this.rbPublishToDashboard.Size = new System.Drawing.Size(41, 43);
            this.rbPublishToDashboard.TabIndex = 7;
            this.tTipGetOneRain.SetToolTip(this.rbPublishToDashboard, "Publish this graph to the Dashboard");
            this.rbPublishToDashboard.UseVisualStyleBackColor = true;
            this.rbPublishToDashboard.Click += new System.EventHandler(this.rbPublishToDashboard_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(533, 513);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(254, 18);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // lblProgressBar
            // 
            this.lblProgressBar.AutoSize = true;
            this.lblProgressBar.Location = new System.Drawing.Point(535, 492);
            this.lblProgressBar.Name = "lblProgressBar";
            this.lblProgressBar.Size = new System.Drawing.Size(74, 13);
            this.lblProgressBar.TabIndex = 10;
            this.lblProgressBar.Text = "lblProgressBar";
            this.lblProgressBar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(981, 544);
            this.Controls.Add(this.lblProgressBar);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.rbSavePlot);
            this.Controls.Add(this.rbPublishToDashboard);
            this.Controls.Add(this.gboxOneRain);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Depth Duration Curves";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.gboxOneRain.ResumeLayout(false);
            this.gboxOneRain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.GroupBox gboxOneRain;
        private RoundButton rbGetOneRain;
        private RoundButton rbPublishToDashboard;
        private System.Windows.Forms.ToolTip tTipGetOneRain;
        private RoundButton rbSavePlot;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgressBar;
    }
}

