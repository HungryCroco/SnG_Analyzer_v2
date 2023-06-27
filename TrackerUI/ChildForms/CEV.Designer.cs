using OxyPlot.Legends;
using OxyPlot;
using OxyPlot.WindowsForms;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace TrackerUI.ChildForms
{
    public partial class NonFlickerPanel : TableLayoutPanel
    {
            public NonFlickerPanel()
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.UserPaint, true);
            }

            public NonFlickerPanel(IContainer container)
            {
                container.Add(this);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.UserPaint, true);
            }
    }
    partial class CEV
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
        public void InitializeComponent()
        {
            this.splitCnt_Overview = new System.Windows.Forms.SplitContainer();
            this.splitCnt_Chart = new System.Windows.Forms.SplitContainer();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtBor_minTourneys = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lbl_calcTime = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cBox_3_3 = new System.Windows.Forms.CheckBox();
            this.cBox_3_1 = new System.Windows.Forms.CheckBox();
            this.cBox_3_2 = new System.Windows.Forms.CheckBox();
            this.cBox_3_11 = new System.Windows.Forms.CheckBox();
            this.cBox_3_10 = new System.Windows.Forms.CheckBox();
            this.cBox_3_9 = new System.Windows.Forms.CheckBox();
            this.cBox_3_13 = new System.Windows.Forms.CheckBox();
            this.cBox_3_6 = new System.Windows.Forms.CheckBox();
            this.cBox_3_8 = new System.Windows.Forms.CheckBox();
            this.cBox_3_7 = new System.Windows.Forms.CheckBox();
            this.cBox_3_18 = new System.Windows.Forms.CheckBox();
            this.cBox_3_14 = new System.Windows.Forms.CheckBox();
            this.cBox_3_15 = new System.Windows.Forms.CheckBox();
            this.cBox_3_17 = new System.Windows.Forms.CheckBox();
            this.cBox_3_16 = new System.Windows.Forms.CheckBox();
            this.cBox_3_12 = new System.Windows.Forms.CheckBox();
            this.cBox_3_5 = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_FishRatio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_cev_t = new System.Windows.Forms.Label();
            this.lbl_bi = new System.Windows.Forms.Label();
            this.lbl_AmtTourney = new System.Windows.Forms.Label();
            this.lbl_Period = new System.Windows.Forms.Label();
            this.cBox_3_4 = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitCnt_Overview)).BeginInit();
            this.splitCnt_Overview.Panel1.SuspendLayout();
            this.splitCnt_Overview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCnt_Chart)).BeginInit();
            this.splitCnt_Chart.Panel2.SuspendLayout();
            this.splitCnt_Chart.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitCnt_Overview
            // 
            this.splitCnt_Overview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCnt_Overview.Location = new System.Drawing.Point(0, 0);
            this.splitCnt_Overview.Name = "splitCnt_Overview";
            this.splitCnt_Overview.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCnt_Overview.Panel1
            // 
            this.splitCnt_Overview.Panel1.Controls.Add(this.splitCnt_Chart);
            this.splitCnt_Overview.Size = new System.Drawing.Size(1894, 1009);
            this.splitCnt_Overview.SplitterDistance = 405;
            this.splitCnt_Overview.SplitterWidth = 10;
            this.splitCnt_Overview.TabIndex = 0;
            // 
            // splitCnt_Chart
            // 
            this.splitCnt_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCnt_Chart.Location = new System.Drawing.Point(0, 0);
            this.splitCnt_Chart.Name = "splitCnt_Chart";
            // 
            // splitCnt_Chart.Panel1
            // 
            this.splitCnt_Chart.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitCnt_Chart.Panel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.splitCnt_Chart.Panel1.ForeColor = System.Drawing.SystemColors.Control;
            this.splitCnt_Chart.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitCnt_Chart.Panel1.Resize += new System.EventHandler(this.splitCnt_Chart_Panel1_Resize);
            // 
            // splitCnt_Chart.Panel2
            // 
            this.splitCnt_Chart.Panel2.Controls.Add(this.checkBox6);
            this.splitCnt_Chart.Panel2.Controls.Add(this.checkBox5);
            this.splitCnt_Chart.Panel2.Controls.Add(this.checkBox4);
            this.splitCnt_Chart.Panel2.Controls.Add(this.checkBox3);
            this.splitCnt_Chart.Panel2.Controls.Add(this.textBox1);
            this.splitCnt_Chart.Panel2.Controls.Add(this.checkBox2);
            this.splitCnt_Chart.Panel2.Controls.Add(this.checkBox1);
            this.splitCnt_Chart.Panel2.Controls.Add(this.txtBor_minTourneys);
            this.splitCnt_Chart.Panel2.Controls.Add(this.label26);
            this.splitCnt_Chart.Panel2.Controls.Add(this.label27);
            this.splitCnt_Chart.Panel2.Controls.Add(this.lbl_calcTime);
            this.splitCnt_Chart.Panel2.Controls.Add(this.label25);
            this.splitCnt_Chart.Size = new System.Drawing.Size(1894, 405);
            this.splitCnt_Chart.SplitterDistance = 1578;
            this.splitCnt_Chart.SplitterWidth = 10;
            this.splitCnt_Chart.TabIndex = 0;
            // 
            // checkBox6
            // 
            this.checkBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(-1064, 311);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox6.Size = new System.Drawing.Size(180, 36);
            this.checkBox6.TabIndex = 11;
            this.checkBox6.Text = "noSD cEV / t";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(-1036, 269);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox5.Size = new System.Drawing.Size(152, 36);
            this.checkBox5.TabIndex = 10;
            this.checkBox5.Text = "SD cEV / t";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(-1037, 227);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox4.Size = new System.Drawing.Size(155, 36);
            this.checkBox4.TabIndex = 9;
            this.checkBox4.Text = "HU cEV / t";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(-1040, 185);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox3.Size = new System.Drawing.Size(157, 36);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "3W cEV / t";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(-966, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(83, 39);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "60";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(-1000, 101);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox2.Size = new System.Drawing.Size(117, 36);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "$EV / t";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(-1043, 143);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(161, 36);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "$EV+RB / t";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtBor_minTourneys
            // 
            this.txtBor_minTourneys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBor_minTourneys.Location = new System.Drawing.Point(-966, 9);
            this.txtBor_minTourneys.Name = "txtBor_minTourneys";
            this.txtBor_minTourneys.Size = new System.Drawing.Size(83, 39);
            this.txtBor_minTourneys.TabIndex = 3;
            this.txtBor_minTourneys.Text = "5000";
            this.txtBor_minTourneys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(-1060, 59);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(88, 32);
            this.label26.TabIndex = 2;
            this.label26.Text = "RB % : ";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(-1086, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(114, 32);
            this.label27.TabIndex = 2;
            this.label27.Text = "min T-s : ";
            // 
            // lbl_calcTime
            // 
            this.lbl_calcTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_calcTime.AutoSize = true;
            this.lbl_calcTime.ForeColor = System.Drawing.Color.Red;
            this.lbl_calcTime.Location = new System.Drawing.Point(-935, 350);
            this.lbl_calcTime.Name = "lbl_calcTime";
            this.lbl_calcTime.Size = new System.Drawing.Size(70, 32);
            this.lbl_calcTime.TabIndex = 1;
            this.lbl_calcTime.Text = " xx.xx";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(-1063, 350);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(126, 32);
            this.label25.TabIndex = 0;
            this.label25.Text = "calc Time: ";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Location = new System.Drawing.Point(1786, 205);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(104, 388);
            this.label45.TabIndex = 64;
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Location = new System.Drawing.Point(1687, 205);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(92, 388);
            this.label44.TabIndex = 63;
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label46.Location = new System.Drawing.Point(1588, 205);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(92, 388);
            this.label46.TabIndex = 62;
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label43.Location = new System.Drawing.Point(1390, 205);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(92, 388);
            this.label43.TabIndex = 59;
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Location = new System.Drawing.Point(1489, 205);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(92, 388);
            this.label42.TabIndex = 58;
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Location = new System.Drawing.Point(1192, 205);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(92, 388);
            this.label41.TabIndex = 57;
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label40.Location = new System.Drawing.Point(1291, 205);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(92, 388);
            this.label40.TabIndex = 56;
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label39.Location = new System.Drawing.Point(994, 205);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(92, 388);
            this.label39.TabIndex = 55;
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Location = new System.Drawing.Point(1093, 205);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(92, 388);
            this.label38.TabIndex = 54;
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Location = new System.Drawing.Point(796, 205);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(92, 388);
            this.label37.TabIndex = 53;
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Location = new System.Drawing.Point(895, 205);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(92, 388);
            this.label36.TabIndex = 52;
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Location = new System.Drawing.Point(598, 205);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(92, 388);
            this.label35.TabIndex = 51;
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Location = new System.Drawing.Point(697, 205);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(92, 388);
            this.label34.TabIndex = 50;
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Location = new System.Drawing.Point(400, 205);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(92, 388);
            this.label33.TabIndex = 49;
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Location = new System.Drawing.Point(499, 205);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(92, 388);
            this.label32.TabIndex = 48;
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Location = new System.Drawing.Point(301, 205);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(92, 388);
            this.label31.TabIndex = 47;
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Location = new System.Drawing.Point(202, 205);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(92, 388);
            this.label29.TabIndex = 46;
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Location = new System.Drawing.Point(103, 205);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(92, 388);
            this.label28.TabIndex = 45;
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cBox_3_3
            // 
            this.cBox_3_3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_3.AutoSize = true;
            this.cBox_3_3.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_3.Location = new System.Drawing.Point(301, 157);
            this.cBox_3_3.Name = "cBox_3_3";
            this.cBox_3_3.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_3.TabIndex = 3;
            this.cBox_3_3.Tag = "3";
            this.cBox_3_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_3.UseVisualStyleBackColor = true;
            // 
            // cBox_3_1
            // 
            this.cBox_3_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_1.AutoSize = true;
            this.cBox_3_1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_1.Location = new System.Drawing.Point(103, 157);
            this.cBox_3_1.Name = "cBox_3_1";
            this.cBox_3_1.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_1.TabIndex = 1;
            this.cBox_3_1.Tag = "1";
            this.cBox_3_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_1.UseVisualStyleBackColor = true;
            // 
            // cBox_3_2
            // 
            this.cBox_3_2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_2.AutoSize = true;
            this.cBox_3_2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_2.Location = new System.Drawing.Point(202, 157);
            this.cBox_3_2.Name = "cBox_3_2";
            this.cBox_3_2.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_2.TabIndex = 2;
            this.cBox_3_2.Tag = "2";
            this.cBox_3_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_2.UseVisualStyleBackColor = true;
            // 
            // cBox_3_11
            // 
            this.cBox_3_11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_11.AutoSize = true;
            this.cBox_3_11.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_11.Location = new System.Drawing.Point(1093, 157);
            this.cBox_3_11.Name = "cBox_3_11";
            this.cBox_3_11.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_11.TabIndex = 44;
            this.cBox_3_11.Tag = "11";
            this.cBox_3_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_11.UseVisualStyleBackColor = true;
            // 
            // cBox_3_10
            // 
            this.cBox_3_10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_10.AutoSize = true;
            this.cBox_3_10.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_10.Location = new System.Drawing.Point(994, 157);
            this.cBox_3_10.Name = "cBox_3_10";
            this.cBox_3_10.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_10.TabIndex = 10;
            this.cBox_3_10.Tag = "10";
            this.cBox_3_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_10.UseVisualStyleBackColor = true;
            // 
            // cBox_3_9
            // 
            this.cBox_3_9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_9.AutoSize = true;
            this.cBox_3_9.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_9.Location = new System.Drawing.Point(895, 157);
            this.cBox_3_9.Name = "cBox_3_9";
            this.cBox_3_9.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_9.TabIndex = 9;
            this.cBox_3_9.Tag = "9";
            this.cBox_3_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_9.UseVisualStyleBackColor = true;
            // 
            // cBox_3_13
            // 
            this.cBox_3_13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_13.AutoSize = true;
            this.cBox_3_13.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_13.Location = new System.Drawing.Point(1291, 157);
            this.cBox_3_13.Name = "cBox_3_13";
            this.cBox_3_13.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_13.TabIndex = 41;
            this.cBox_3_13.Tag = "13";
            this.cBox_3_13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_13.UseVisualStyleBackColor = true;
            // 
            // cBox_3_6
            // 
            this.cBox_3_6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_6.AutoSize = true;
            this.cBox_3_6.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_6.Location = new System.Drawing.Point(598, 157);
            this.cBox_3_6.Name = "cBox_3_6";
            this.cBox_3_6.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_6.TabIndex = 6;
            this.cBox_3_6.Tag = "6";
            this.cBox_3_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_6.UseVisualStyleBackColor = true;
            // 
            // cBox_3_8
            // 
            this.cBox_3_8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_8.AutoSize = true;
            this.cBox_3_8.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_8.Location = new System.Drawing.Point(796, 157);
            this.cBox_3_8.Name = "cBox_3_8";
            this.cBox_3_8.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_8.TabIndex = 8;
            this.cBox_3_8.Tag = "8";
            this.cBox_3_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_8.UseVisualStyleBackColor = true;
            // 
            // cBox_3_7
            // 
            this.cBox_3_7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_7.AutoSize = true;
            this.cBox_3_7.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_7.Location = new System.Drawing.Point(697, 157);
            this.cBox_3_7.Name = "cBox_3_7";
            this.cBox_3_7.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_7.TabIndex = 7;
            this.cBox_3_7.Tag = "7";
            this.cBox_3_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_7.UseVisualStyleBackColor = true;
            // 
            // cBox_3_18
            // 
            this.cBox_3_18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_18.AutoSize = true;
            this.cBox_3_18.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_18.Location = new System.Drawing.Point(1786, 157);
            this.cBox_3_18.Name = "cBox_3_18";
            this.cBox_3_18.Size = new System.Drawing.Size(104, 44);
            this.cBox_3_18.TabIndex = 37;
            this.cBox_3_18.Tag = "18";
            this.cBox_3_18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_18.UseVisualStyleBackColor = true;
            // 
            // cBox_3_14
            // 
            this.cBox_3_14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_14.AutoSize = true;
            this.cBox_3_14.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_14.Location = new System.Drawing.Point(1390, 157);
            this.cBox_3_14.Name = "cBox_3_14";
            this.cBox_3_14.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_14.TabIndex = 36;
            this.cBox_3_14.Tag = "14";
            this.cBox_3_14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_14.UseVisualStyleBackColor = true;
            // 
            // cBox_3_15
            // 
            this.cBox_3_15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_15.AutoSize = true;
            this.cBox_3_15.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_15.Location = new System.Drawing.Point(1489, 157);
            this.cBox_3_15.Name = "cBox_3_15";
            this.cBox_3_15.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_15.TabIndex = 35;
            this.cBox_3_15.Tag = "15";
            this.cBox_3_15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_15.UseVisualStyleBackColor = true;
            // 
            // cBox_3_17
            // 
            this.cBox_3_17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_17.AutoSize = true;
            this.cBox_3_17.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_17.Location = new System.Drawing.Point(1687, 157);
            this.cBox_3_17.Name = "cBox_3_17";
            this.cBox_3_17.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_17.TabIndex = 33;
            this.cBox_3_17.Tag = "17";
            this.cBox_3_17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_17.UseVisualStyleBackColor = true;
            // 
            // cBox_3_16
            // 
            this.cBox_3_16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_16.AutoSize = true;
            this.cBox_3_16.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_16.Location = new System.Drawing.Point(1588, 157);
            this.cBox_3_16.Name = "cBox_3_16";
            this.cBox_3_16.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_16.TabIndex = 32;
            this.cBox_3_16.Tag = "16";
            this.cBox_3_16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_16.UseVisualStyleBackColor = true;
            // 
            // cBox_3_12
            // 
            this.cBox_3_12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_12.AutoSize = true;
            this.cBox_3_12.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_12.Location = new System.Drawing.Point(1192, 157);
            this.cBox_3_12.Name = "cBox_3_12";
            this.cBox_3_12.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_12.TabIndex = 31;
            this.cBox_3_12.Tag = "12";
            this.cBox_3_12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_12.UseVisualStyleBackColor = true;
            // 
            // cBox_3_5
            // 
            this.cBox_3_5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_5.AutoSize = true;
            this.cBox_3_5.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_5.Location = new System.Drawing.Point(499, 157);
            this.cBox_3_5.Name = "cBox_3_5";
            this.cBox_3_5.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_5.TabIndex = 5;
            this.cBox_3_5.Tag = "5";
            this.cBox_3_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_5.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Location = new System.Drawing.Point(796, 52);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(191, 50);
            this.label24.TabIndex = 28;
            this.label24.Text = "BB v BTN";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Location = new System.Drawing.Point(598, 52);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(191, 50);
            this.label23.TabIndex = 27;
            this.label23.Text = "SB v BTN";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Location = new System.Drawing.Point(1489, 103);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(92, 50);
            this.label22.TabIndex = 26;
            this.label22.Text = "vFISH";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Location = new System.Drawing.Point(1192, 103);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(92, 50);
            this.label21.TabIndex = 25;
            this.label21.Text = "vREG";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Location = new System.Drawing.Point(1291, 103);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 50);
            this.label20.TabIndex = 24;
            this.label20.Text = "vFISH";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(994, 103);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(92, 50);
            this.label19.TabIndex = 23;
            this.label19.Text = "vREG";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(1588, 103);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 50);
            this.label18.TabIndex = 22;
            this.label18.Text = "vREG";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(895, 103);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 50);
            this.label17.TabIndex = 21;
            this.label17.Text = "vFISH";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(697, 103);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 50);
            this.label16.TabIndex = 20;
            this.label16.Text = "vFISH";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(1687, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 50);
            this.label15.TabIndex = 19;
            this.label15.Text = "vFISH";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(796, 103);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 50);
            this.label14.TabIndex = 18;
            this.label14.Text = "vREG";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(598, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 50);
            this.label13.TabIndex = 17;
            this.label13.Text = "vREG";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(499, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 50);
            this.label12.TabIndex = 16;
            this.label12.Text = "vFISH";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(400, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 50);
            this.label11.TabIndex = 15;
            this.label11.Text = "vREG";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(4, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 388);
            this.label10.TabIndex = 14;
            this.label10.Text = "AVERAGE";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(1093, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 50);
            this.label9.TabIndex = 13;
            this.label9.Text = "vFISH";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(1390, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 50);
            this.label8.TabIndex = 12;
            this.label8.Text = "vREG";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(1588, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(191, 50);
            this.label7.TabIndex = 11;
            this.label7.Text = "BB";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(1390, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 50);
            this.label6.TabIndex = 10;
            this.label6.Text = "SB";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(1192, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 50);
            this.label5.TabIndex = 9;
            this.label5.Text = "BBvSB (BvB)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(994, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 50);
            this.label4.TabIndex = 8;
            this.label4.Text = "SBvBB (BvB)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(400, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 50);
            this.label3.TabIndex = 7;
            this.label3.Text = "BTN v BB";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_FishRatio
            // 
            this.lbl_FishRatio.AutoSize = true;
            this.lbl_FishRatio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_FishRatio.Location = new System.Drawing.Point(1786, 1);
            this.lbl_FishRatio.Name = "lbl_FishRatio";
            this.lbl_FishRatio.Size = new System.Drawing.Size(104, 152);
            this.lbl_FishRatio.TabIndex = 6;
            this.lbl_FishRatio.Text = "F/R Ratio";
            this.lbl_FishRatio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1390, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(389, 50);
            this.label2.TabIndex = 5;
            this.label2.Text = "HU";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(400, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(983, 50);
            this.label1.TabIndex = 4;
            this.label1.Text = "3W";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_cev_t
            // 
            this.lbl_cev_t.AutoSize = true;
            this.lbl_cev_t.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_cev_t.Location = new System.Drawing.Point(301, 1);
            this.lbl_cev_t.Name = "lbl_cev_t";
            this.lbl_cev_t.Size = new System.Drawing.Size(92, 152);
            this.lbl_cev_t.TabIndex = 3;
            this.lbl_cev_t.Text = "cEV/t";
            this.lbl_cev_t.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_bi
            // 
            this.lbl_bi.AutoSize = true;
            this.lbl_bi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_bi.Location = new System.Drawing.Point(202, 1);
            this.lbl_bi.Name = "lbl_bi";
            this.lbl_bi.Size = new System.Drawing.Size(92, 152);
            this.lbl_bi.TabIndex = 2;
            this.lbl_bi.Text = "av. BI";
            this.lbl_bi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AmtTourney
            // 
            this.lbl_AmtTourney.AutoSize = true;
            this.lbl_AmtTourney.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_AmtTourney.Location = new System.Drawing.Point(103, 1);
            this.lbl_AmtTourney.Name = "lbl_AmtTourney";
            this.lbl_AmtTourney.Size = new System.Drawing.Size(92, 152);
            this.lbl_AmtTourney.TabIndex = 1;
            this.lbl_AmtTourney.Text = "Tourney Played";
            this.lbl_AmtTourney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Period
            // 
            this.lbl_Period.AutoSize = true;
            this.lbl_Period.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Period.Location = new System.Drawing.Point(4, 1);
            this.lbl_Period.Name = "lbl_Period";
            this.lbl_Period.Size = new System.Drawing.Size(92, 152);
            this.lbl_Period.TabIndex = 0;
            this.lbl_Period.Text = "Month";
            this.lbl_Period.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cBox_3_4
            // 
            this.cBox_3_4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_3_4.AutoSize = true;
            this.cBox_3_4.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_4.Location = new System.Drawing.Point(400, 157);
            this.cBox_3_4.Name = "cBox_3_4";
            this.cBox_3_4.Size = new System.Drawing.Size(92, 44);
            this.cBox_3_4.TabIndex = 4;
            this.cBox_3_4.Tag = "4";
            this.cBox_3_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBox_3_4.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(0, 32);
            this.label30.TabIndex = 46;
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CEV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1894, 1009);
            this.ControlBox = false;
            this.Controls.Add(this.label30);
            this.Controls.Add(this.splitCnt_Overview);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Location = new System.Drawing.Point(0, -50);
            this.Name = "CEV";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpinAnalyzer";
            this.Resize += new System.EventHandler(this.CEV_Resize);
            this.splitCnt_Overview.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCnt_Overview)).EndInit();
            this.splitCnt_Overview.ResumeLayout(false);
            this.splitCnt_Chart.Panel2.ResumeLayout(false);
            this.splitCnt_Chart.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCnt_Chart)).EndInit();
            this.splitCnt_Chart.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitCnt_Overview;
        private System.Windows.Forms.SplitContainer splitCnt_Chart;
        //private System.Windows.Forms.TableLayoutPanel tlp_overview;
        private NonFlickerPanel tlp_overview;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_FishRatio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_cev_t;
        private System.Windows.Forms.Label lbl_bi;
        private System.Windows.Forms.Label lbl_AmtTourney;
        private System.Windows.Forms.Label lbl_Period;
        private System.Windows.Forms.Label label10;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label24;
        private Label label23;
        private Label label22;
        private Label label21;
        private Label label20;
        private Label label19;
        private Label lbl_calcTime;
        private Label label25;
        private CheckBox cBox_3_4;
        private CheckBox cBox_3_5;
        private CheckBox cBox_3_11;
        private CheckBox cBox_3_10;
        private CheckBox cBox_3_9;
        private CheckBox cBox_3_13;
        private CheckBox cBox_3_6;
        private CheckBox cBox_3_8;
        private CheckBox cBox_3_7;
        private CheckBox cBox_3_18;
        private CheckBox cBox_3_14;
        private CheckBox cBox_3_15;
        private CheckBox cBox_3_17;
        private CheckBox cBox_3_16;
        private CheckBox cBox_3_12;
        private CheckBox cBox_3_3;
        private CheckBox cBox_3_1;
        private CheckBox cBox_3_2;
        private TextBox txtBor_minTourneys;
        private Label label26;
        private Label label27;
        private CheckBox checkBox1;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private TextBox textBox1;
        private CheckBox checkBox2;
        private Label label45;
        private Label label44;
        private Label label46;
        private Label label43;
        private Label label42;
        private Label label41;
        private Label label40;
        private Label label39;
        private Label label38;
        private Label label37;
        private Label label36;
        private Label label35;
        private Label label34;
        private Label label33;
        private Label label32;
        private Label label31;
        private Label label29;
        private Label label28;
        private Label label30;
        private ToolStripMenuItem menu;

    }
}