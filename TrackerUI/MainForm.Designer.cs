using TrackerLibrary;

namespace TrackerUI
{
    partial class MainForm
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnVillain = new System.Windows.Forms.Button();
            this.btnBoard = new System.Windows.Forms.Button();
            this.btnCEV = new System.Windows.Forms.Button();
            this.btnDashBoard = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblArrow_Hide = new System.Windows.Forms.Label();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.lblArrow_Show = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnImport);
            this.panelMenu.Controls.Add(this.btnVillain);
            this.panelMenu.Controls.Add(this.btnBoard);
            this.panelMenu.Controls.Add(this.btnCEV);
            this.panelMenu.Controls.Add(this.btnDashBoard);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(350, 1631);
            this.panelMenu.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(199)))));
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSettings.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.btnSettings.Location = new System.Drawing.Point(0, 550);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(350, 100);
            this.btnSettings.TabIndex = 6;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            this.btnSettings.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnSettings.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnImport
            // 
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnImport.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.btnImport.Location = new System.Drawing.Point(0, 450);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(350, 100);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "ImportHH";
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            this.btnImport.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnImport.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnVillain
            // 
            this.btnVillain.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVillain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnVillain.FlatAppearance.BorderSize = 0;
            this.btnVillain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnVillain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnVillain.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnVillain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.btnVillain.Location = new System.Drawing.Point(0, 350);
            this.btnVillain.Name = "btnVillain";
            this.btnVillain.Size = new System.Drawing.Size(350, 100);
            this.btnVillain.TabIndex = 4;
            this.btnVillain.Text = "Villian Analyze";
            this.btnVillain.UseVisualStyleBackColor = false;
            this.btnVillain.Click += new System.EventHandler(this.btnVillian_Click);
            this.btnVillain.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnVillain.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBoard
            // 
            this.btnBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBoard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnBoard.FlatAppearance.BorderSize = 0;
            this.btnBoard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnBoard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnBoard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBoard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.btnBoard.Location = new System.Drawing.Point(0, 250);
            this.btnBoard.Name = "btnBoard";
            this.btnBoard.Size = new System.Drawing.Size(350, 100);
            this.btnBoard.TabIndex = 3;
            this.btnBoard.Text = "Board Analyze";
            this.btnBoard.UseVisualStyleBackColor = false;
            this.btnBoard.Click += new System.EventHandler(this.btnBoard_Click);
            this.btnBoard.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBoard.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnCEV
            // 
            this.btnCEV.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCEV.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCEV.FlatAppearance.BorderSize = 0;
            this.btnCEV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnCEV.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCEV.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCEV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.btnCEV.Location = new System.Drawing.Point(0, 150);
            this.btnCEV.Name = "btnCEV";
            this.btnCEV.Size = new System.Drawing.Size(350, 100);
            this.btnCEV.TabIndex = 2;
            this.btnCEV.Text = "CEV Analyze";
            this.btnCEV.UseVisualStyleBackColor = false;
            this.btnCEV.Click += new System.EventHandler(this.btnCEV_Click);
            this.btnCEV.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnCEV.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnDashBoard
            // 
            this.btnDashBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashBoard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDashBoard.FlatAppearance.BorderSize = 0;
            this.btnDashBoard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnDashBoard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnDashBoard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDashBoard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.btnDashBoard.Location = new System.Drawing.Point(0, 50);
            this.btnDashBoard.Name = "btnDashBoard";
            this.btnDashBoard.Size = new System.Drawing.Size(350, 100);
            this.btnDashBoard.TabIndex = 1;
            this.btnDashBoard.Text = "DahsBoard";
            this.btnDashBoard.UseVisualStyleBackColor = false;
            this.btnDashBoard.Click += new System.EventHandler(this.btnDashBoard_Click);
            this.btnDashBoard.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnDashBoard.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(242)))), ((int)(((byte)(225)))));
            this.panelLogo.Controls.Add(this.lblArrow_Hide);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(350, 50);
            this.panelLogo.TabIndex = 0;
            // 
            // lblArrow_Hide
            // 
            this.lblArrow_Hide.AutoSize = true;
            this.lblArrow_Hide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(242)))), ((int)(((byte)(225)))));
            this.lblArrow_Hide.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblArrow_Hide.Font = new System.Drawing.Font("STCaiyun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblArrow_Hide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.lblArrow_Hide.Location = new System.Drawing.Point(291, 0);
            this.lblArrow_Hide.Name = "lblArrow_Hide";
            this.lblArrow_Hide.Size = new System.Drawing.Size(59, 50);
            this.lblArrow_Hide.TabIndex = 1;
            this.lblArrow_Hide.Text = "- -";
            this.lblArrow_Hide.Click += new System.EventHandler(this.lblArrow_Hide_Click);
            // 
            // panelDesktop
            // 
            this.panelDesktop.AutoScroll = true;
            this.panelDesktop.Controls.Add(this.lblArrow_Show);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelDesktop.Location = new System.Drawing.Point(350, 0);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(2502, 1631);
            this.panelDesktop.TabIndex = 3;
            // 
            // lblArrow_Show
            // 
            this.lblArrow_Show.BackColor = System.Drawing.SystemColors.Control;
            this.lblArrow_Show.Font = new System.Drawing.Font("STCaiyun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblArrow_Show.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(125)))), ((int)(((byte)(95)))));
            this.lblArrow_Show.Location = new System.Drawing.Point(0, 0);
            this.lblArrow_Show.Name = "lblArrow_Show";
            this.lblArrow_Show.Size = new System.Drawing.Size(70, 50);
            this.lblArrow_Show.TabIndex = 7;
            this.lblArrow_Show.Text = "- -";
            this.lblArrow_Show.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblArrow_Show.Click += new System.EventHandler(this.lblArrow_Show_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(2852, 1631);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(2878, 1702);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelDesktop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnVillain;
        private System.Windows.Forms.Button btnBoard;
        private System.Windows.Forms.Button btnCEV;
        private System.Windows.Forms.Button btnDashBoard;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblArrow_Hide;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Button btnSettings;
        private Label lblArrow_Show;
    }
}