namespace AFDataViewer
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
            this.components = new System.ComponentModel.Container();
            this.piSystemPicker1 = new OSIsoft.AF.UI.PISystemPicker();
            this.afDatabasePicker1 = new OSIsoft.AF.UI.AFDatabasePicker();
            this.afElementFindCtrl1 = new OSIsoft.AF.UI.AFElementFindCtrl();
            this.afTreeView1 = new OSIsoft.AF.UI.AFTreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTreeHeading = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxPostFetch = new System.Windows.Forms.GroupBox();
            this.cbxUoms = new System.Windows.Forms.ComboBox();
            this.cbxTimeZones = new System.Windows.Forms.ComboBox();
            this.lblDataAttrPath = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gbxPreFetch = new System.Windows.Forms.GroupBox();
            this.cbxDataMethods = new System.Windows.Forms.ComboBox();
            this.lblSelectedAttrPath = new System.Windows.Forms.Label();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbxPostFetch.SuspendLayout();
            this.gbxPreFetch.SuspendLayout();
            this.SuspendLayout();
            // 
            // piSystemPicker1
            // 
            this.piSystemPicker1.AccessibleDescription = "PI System Picker";
            this.piSystemPicker1.AccessibleName = "PI System Picker";
            this.piSystemPicker1.Cursor = System.Windows.Forms.Cursors.Default;
            this.piSystemPicker1.Location = new System.Drawing.Point(97, 26);
            this.piSystemPicker1.LoginPromptSetting = OSIsoft.AF.UI.PISystemPicker.LoginPromptSettingOptions.Default;
            this.piSystemPicker1.Name = "piSystemPicker1";
            this.piSystemPicker1.ShowBegin = false;
            this.piSystemPicker1.ShowConnect = false;
            this.piSystemPicker1.ShowDelete = false;
            this.piSystemPicker1.ShowEnd = false;
            this.piSystemPicker1.ShowFind = false;
            this.piSystemPicker1.ShowList = false;
            this.piSystemPicker1.ShowNavigation = false;
            this.piSystemPicker1.ShowNew = false;
            this.piSystemPicker1.ShowNext = false;
            this.piSystemPicker1.ShowPrevious = false;
            this.piSystemPicker1.ShowProperties = false;
            this.piSystemPicker1.Size = new System.Drawing.Size(292, 22);
            this.piSystemPicker1.TabIndex = 5;
            this.piSystemPicker1.SelectionChange += new OSIsoft.AF.UI.SelectionChangeEventHandler(this.piSystemPicker1_SelectionChange);
            // 
            // afDatabasePicker1
            // 
            this.afDatabasePicker1.AccessibleDescription = "Database Picker";
            this.afDatabasePicker1.AccessibleName = "Database Picker";
            this.afDatabasePicker1.ConnectAutomatically = true;
            this.afDatabasePicker1.Location = new System.Drawing.Point(97, 63);
            this.afDatabasePicker1.Name = "afDatabasePicker1";
            this.afDatabasePicker1.ShowBegin = false;
            this.afDatabasePicker1.ShowConfigurationDatabase = OSIsoft.AF.UI.ShowConfigurationDatabase.Hide;
            this.afDatabasePicker1.ShowDelete = false;
            this.afDatabasePicker1.ShowEnd = false;
            this.afDatabasePicker1.ShowFind = false;
            this.afDatabasePicker1.ShowList = false;
            this.afDatabasePicker1.ShowNavigation = false;
            this.afDatabasePicker1.ShowNew = false;
            this.afDatabasePicker1.ShowNext = false;
            this.afDatabasePicker1.ShowPrevious = false;
            this.afDatabasePicker1.ShowProperties = false;
            this.afDatabasePicker1.Size = new System.Drawing.Size(292, 22);
            this.afDatabasePicker1.TabIndex = 6;
            this.afDatabasePicker1.SelectionChange += new OSIsoft.AF.UI.SelectionChangeEventHandler(this.afDatabasePicker1_SelectionChange);
            // 
            // afElementFindCtrl1
            // 
            this.afElementFindCtrl1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.afElementFindCtrl1.Location = new System.Drawing.Point(14, 116);
            this.afElementFindCtrl1.Margin = new System.Windows.Forms.Padding(4);
            this.afElementFindCtrl1.MinimumSize = new System.Drawing.Size(0, 22);
            this.afElementFindCtrl1.Name = "afElementFindCtrl1";
            this.afElementFindCtrl1.Size = new System.Drawing.Size(375, 24);
            this.afElementFindCtrl1.TabIndex = 7;
            this.afElementFindCtrl1.AFElementUpdated += new OSIsoft.AF.UI.AFElementFindCtrl.AFElementUpdatedEventHandler(this.afElementFindCtrl1_AFElementUpdated);
            // 
            // afTreeView1
            // 
            this.afTreeView1.HideSelection = false;
            this.afTreeView1.Location = new System.Drawing.Point(14, 184);
            this.afTreeView1.Name = "afTreeView1";
            this.afTreeView1.ShowAnalyses = false;
            this.afTreeView1.ShowAnalysisTemplates = false;
            this.afTreeView1.ShowAttributes = true;
            this.afTreeView1.ShowCases = false;
            this.afTreeView1.ShowCaseTemplates = false;
            this.afTreeView1.ShowCategories = false;
            this.afTreeView1.ShowConfigurationDatabase = OSIsoft.AF.UI.ShowConfigurationDatabase.Hide;
            this.afTreeView1.ShowContacts = false;
            this.afTreeView1.ShowDatabases = false;
            this.afTreeView1.ShowElements = false;
            this.afTreeView1.ShowElementTemplates = false;
            this.afTreeView1.ShowEnumerations = false;
            this.afTreeView1.ShowEventFrameTemplates = false;
            this.afTreeView1.ShowLayers = false;
            this.afTreeView1.ShowModels = false;
            this.afTreeView1.ShowModelTemplates = false;
            this.afTreeView1.ShowNodeToolTips = true;
            this.afTreeView1.ShowNotificationTemplates = false;
            this.afTreeView1.ShowPlugIns = false;
            this.afTreeView1.ShowReferenceTypes = false;
            this.afTreeView1.ShowTableConnections = false;
            this.afTreeView1.ShowTables = false;
            this.afTreeView1.ShowTransferTemplates = false;
            this.afTreeView1.Size = new System.Drawing.Size(375, 419);
            this.afTreeView1.TabIndex = 8;
            this.afTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.afTreeView1_AfterSelect);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Asset Server:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "AF Database:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Element of Interest";
            // 
            // lblTreeHeading
            // 
            this.lblTreeHeading.AutoSize = true;
            this.lblTreeHeading.Location = new System.Drawing.Point(11, 159);
            this.lblTreeHeading.Name = "lblTreeHeading";
            this.lblTreeHeading.Size = new System.Drawing.Size(142, 13);
            this.lblTreeHeading.TabIndex = 1;
            this.lblTreeHeading.Text = "Select an AF Attribute in tree";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(429, 20);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(123, 23);
            this.btnAbout.TabIndex = 15;
            this.btnAbout.Text = "About this Version";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(424, 83);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(115, 26);
            this.btnGetData.TabIndex = 14;
            this.btnGetData.Text = "Get Data";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(429, 281);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(674, 322);
            this.dataGridView1.TabIndex = 13;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Timestamp";
            this.Column1.MaxInputLength = 50;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(569, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Version 2 - Validation of User Inputs occur BEFORE \'Get Data\' is clicked.";
            // 
            // gbxPostFetch
            // 
            this.gbxPostFetch.Controls.Add(this.cbxUoms);
            this.gbxPostFetch.Controls.Add(this.cbxTimeZones);
            this.gbxPostFetch.Controls.Add(this.lblDataAttrPath);
            this.gbxPostFetch.Controls.Add(this.label10);
            this.gbxPostFetch.Controls.Add(this.label11);
            this.gbxPostFetch.Location = new System.Drawing.Point(429, 194);
            this.gbxPostFetch.Name = "gbxPostFetch";
            this.gbxPostFetch.Size = new System.Drawing.Size(674, 90);
            this.gbxPostFetch.TabIndex = 11;
            this.gbxPostFetch.TabStop = false;
            this.gbxPostFetch.Text = "Last \'Get Data\' Attribute:";
            // 
            // cbxUoms
            // 
            this.cbxUoms.FormattingEnabled = true;
            this.cbxUoms.Location = new System.Drawing.Point(438, 54);
            this.cbxUoms.Name = "cbxUoms";
            this.cbxUoms.Size = new System.Drawing.Size(181, 21);
            this.cbxUoms.TabIndex = 5;
            this.cbxUoms.SelectedIndexChanged += new System.EventHandler(this.cbxUoms_SelectedIndexChanged);
            // 
            // cbxTimeZones
            // 
            this.cbxTimeZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTimeZones.FormattingEnabled = true;
            this.cbxTimeZones.Location = new System.Drawing.Point(90, 54);
            this.cbxTimeZones.Name = "cbxTimeZones";
            this.cbxTimeZones.Size = new System.Drawing.Size(207, 21);
            this.cbxTimeZones.TabIndex = 5;
            this.cbxTimeZones.SelectedIndexChanged += new System.EventHandler(this.cbxTimeZones_SelectedIndexChanged);
            // 
            // lblDataAttrPath
            // 
            this.lblDataAttrPath.AutoSize = true;
            this.lblDataAttrPath.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataAttrPath.Location = new System.Drawing.Point(23, 26);
            this.lblDataAttrPath.Name = "lblDataAttrPath";
            this.lblDataAttrPath.Size = new System.Drawing.Size(119, 14);
            this.lblDataAttrPath.TabIndex = 1;
            this.lblDataAttrPath.Text = "Click \'Get Data\'";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Time Zone:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(397, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "UOM:";
            // 
            // gbxPreFetch
            // 
            this.gbxPreFetch.Controls.Add(this.cbxDataMethods);
            this.gbxPreFetch.Controls.Add(this.btnGetData);
            this.gbxPreFetch.Controls.Add(this.lblSelectedAttrPath);
            this.gbxPreFetch.Controls.Add(this.tbEndTime);
            this.gbxPreFetch.Controls.Add(this.label7);
            this.gbxPreFetch.Controls.Add(this.tbStartTime);
            this.gbxPreFetch.Controls.Add(this.lblElapsed);
            this.gbxPreFetch.Controls.Add(this.label12);
            this.gbxPreFetch.Controls.Add(this.label6);
            this.gbxPreFetch.Location = new System.Drawing.Point(429, 63);
            this.gbxPreFetch.Name = "gbxPreFetch";
            this.gbxPreFetch.Size = new System.Drawing.Size(674, 125);
            this.gbxPreFetch.TabIndex = 12;
            this.gbxPreFetch.TabStop = false;
            this.gbxPreFetch.Text = "Required Inputs Passed to Server Before Fetching Data";
            // 
            // cbxDataMethods
            // 
            this.cbxDataMethods.FormattingEnabled = true;
            this.cbxDataMethods.Items.AddRange(new object[] {
            "GetValues",
            "RecordedValues",
            "PlotValues"});
            this.cbxDataMethods.Location = new System.Drawing.Point(424, 45);
            this.cbxDataMethods.Name = "cbxDataMethods";
            this.cbxDataMethods.Size = new System.Drawing.Size(183, 21);
            this.cbxDataMethods.TabIndex = 5;
            this.cbxDataMethods.SelectedIndexChanged += new System.EventHandler(this.cbxDataMethods_SelectedIndexChanged);
            // 
            // lblSelectedAttrPath
            // 
            this.lblSelectedAttrPath.AutoSize = true;
            this.lblSelectedAttrPath.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedAttrPath.Location = new System.Drawing.Point(23, 23);
            this.lblSelectedAttrPath.Name = "lblSelectedAttrPath";
            this.lblSelectedAttrPath.Size = new System.Drawing.Size(378, 14);
            this.lblSelectedAttrPath.TabIndex = 1;
            this.lblSelectedAttrPath.Text = "Select an AFAttribute from the tree in the lower left";
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(101, 83);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(180, 20);
            this.tbEndTime.TabIndex = 4;
            this.tbEndTime.Text = "*";
            this.tbEndTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TimeBox_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "End Time:";
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(101, 46);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(180, 20);
            this.tbStartTime.TabIndex = 4;
            this.tbStartTime.Text = "t-7d";
            this.tbStartTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TimeBox_KeyUp);
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = true;
            this.lblElapsed.Location = new System.Drawing.Point(557, 90);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(71, 13);
            this.lblElapsed.TabIndex = 10;
            this.lblElapsed.Text = "0.00 seconds";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(346, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Data Method:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Start Time:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 625);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxPostFetch);
            this.Controls.Add(this.gbxPreFetch);
            this.Controls.Add(this.afTreeView1);
            this.Controls.Add(this.lblTreeHeading);
            this.Controls.Add(this.piSystemPicker1);
            this.Controls.Add(this.afDatabasePicker1);
            this.Controls.Add(this.afElementFindCtrl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Name = "MainForm";
            this.Text = "AF Data Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbxPostFetch.ResumeLayout(false);
            this.gbxPostFetch.PerformLayout();
            this.gbxPreFetch.ResumeLayout(false);
            this.gbxPreFetch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OSIsoft.AF.UI.PISystemPicker piSystemPicker1;
        private OSIsoft.AF.UI.AFDatabasePicker afDatabasePicker1;
        private OSIsoft.AF.UI.AFElementFindCtrl afElementFindCtrl1;
        private OSIsoft.AF.UI.AFTreeView afTreeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTreeHeading;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxPostFetch;
        private System.Windows.Forms.ComboBox cbxUoms;
        private System.Windows.Forms.ComboBox cbxTimeZones;
        private System.Windows.Forms.Label lblDataAttrPath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbxPreFetch;
        private System.Windows.Forms.ComboBox cbxDataMethods;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSelectedAttrPath;
    }
}

