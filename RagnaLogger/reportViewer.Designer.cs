namespace RagnaLogger
{
    partial class reportViewer
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
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Score 1: 1234.56m");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Score 2: 1234.58m");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Jan 01, 1970 00:00:00", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("OST", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Custom", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("undefined0", 1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("undefined1", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reportViewer));
            this.scoreList = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.songList = new System.Windows.Forms.ListView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.closeBtn = new System.Windows.Forms.Button();
            this.iconsList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // scoreList
            // 
            this.scoreList.Location = new System.Drawing.Point(12, 12);
            this.scoreList.Name = "scoreList";
            treeNode7.Name = "score0";
            treeNode7.Text = "Score 1: 1234.56m";
            treeNode8.Name = "score1";
            treeNode8.Text = "Score 2: 1234.58m";
            treeNode9.Name = "data19700101000000";
            treeNode9.Text = "Jan 01, 1970 00:00:00";
            this.scoreList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9});
            this.scoreList.Size = new System.Drawing.Size(383, 379);
            this.scoreList.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 428);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // songList
            // 
            listViewGroup1.Header = "OST";
            listViewGroup1.Name = "OST";
            listViewGroup1.Tag = "ost";
            listViewGroup2.Header = "Custom";
            listViewGroup2.Name = "Custom";
            listViewGroup2.Tag = "custom";
            this.songList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.songList.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.Tag = "song0";
            listViewItem2.Group = listViewGroup2;
            listViewItem2.Tag = "song1";
            this.songList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.songList.LargeImageList = this.iconsList;
            this.songList.Location = new System.Drawing.Point(401, 12);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(387, 379);
            this.songList.SmallImageList = this.iconsList;
            this.songList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.songList.TabIndex = 2;
            this.songList.UseCompatibleStateImageBehavior = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(3, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 428);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(12, 397);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "OK";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // iconsList
            // 
            this.iconsList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconsList.ImageStream")));
            this.iconsList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconsList.Images.SetKeyName(0, "CUS_1c.png");
            this.iconsList.Images.SetKeyName(1, "OST_1c.png");
            // 
            // reportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 428);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.songList);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.scoreList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "reportViewer";
            this.Text = "Report Viewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView scoreList;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView songList;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.ImageList iconsList;
    }
}