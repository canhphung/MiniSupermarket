namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpSearch = new GroupBox();
            btnLoad = new Button();
            btnSearch = new Button();
            txtKeyword = new TextBox();
            grpList = new GroupBox();
            dgvCustomers = new DataGridView();
            grpInfo = new GroupBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtMembershipRank = new TextBox();
            lblMembershipRank = new Label();
            txtRewardPoints = new TextBox();
            lblRewardPoints = new Label();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtPhoneNumber = new TextBox();
            lblPhoneNumber = new Label();
            txtCustomerName = new TextBox();
            lblCustomerName = new Label();
            txtCustomerId = new TextBox();
            lblCustomerId = new Label();
            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            grpInfo.SuspendLayout();
            SuspendLayout();

            // 
            // grpSearch
            // 
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Location = new Point(20, 15);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(1040, 80);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm khách hàng";

            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(485, 28);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(100, 30);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(370, 28);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 30);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(20, 32);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên hoặc số điện thoại...";
            txtKeyword.Size = new Size(330, 23);
            txtKeyword.TabIndex = 0;

            // 
            // grpList
            // 
            grpList.Controls.Add(dgvCustomers);
            grpList.Location = new Point(20, 110);
            grpList.Name = "grpList";
            grpList.Size = new Size(670, 440);
            grpList.TabIndex = 1;
            grpList.TabStop = false;
            grpList.Text = "Danh sách Khách hàng";

            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(15, 25);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(640, 400);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Controls.Add(txtMembershipRank);
            grpInfo.Controls.Add(lblMembershipRank);
            grpInfo.Controls.Add(txtRewardPoints);
            grpInfo.Controls.Add(lblRewardPoints);
            grpInfo.Controls.Add(txtAddress);
            grpInfo.Controls.Add(lblAddress);
            grpInfo.Controls.Add(txtPhoneNumber);
            grpInfo.Controls.Add(lblPhoneNumber);
            grpInfo.Controls.Add(txtCustomerName);
            grpInfo.Controls.Add(lblCustomerName);
            grpInfo.Controls.Add(txtCustomerId);
            grpInfo.Controls.Add(lblCustomerId);
            grpInfo.Location = new Point(705, 110);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(355, 440);
            grpInfo.TabIndex = 2;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin Khách hàng";

            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(238, 380);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 32);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(128, 380);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(90, 32);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(20, 380);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 32);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            // 
            // txtMembershipRank
            // 
            txtMembershipRank.Location = new Point(20, 335);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(308, 23);
            txtMembershipRank.TabIndex = 11;

            // 
            // lblMembershipRank
            // 
            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Location = new Point(20, 315);
            lblMembershipRank.Name = "lblMembershipRank";
            lblMembershipRank.Size = new Size(92, 15);
            lblMembershipRank.TabIndex = 10;
            lblMembershipRank.Text = "Hạng thành viên";

            // 
            // txtRewardPoints
            // 
            txtRewardPoints.Location = new Point(20, 275);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(308, 23);
            txtRewardPoints.TabIndex = 9;

            // 
            // lblRewardPoints
            // 
            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Location = new Point(20, 255);
            lblRewardPoints.Name = "lblRewardPoints";
            lblRewardPoints.Size = new Size(69, 15);
            lblRewardPoints.TabIndex = 8;
            lblRewardPoints.Text = "Điểm thưởng";

            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(20, 215);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(308, 23);
            txtAddress.TabIndex = 7;

            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(20, 195);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(43, 15);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Địa chỉ";

            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(20, 155);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(308, 23);
            txtPhoneNumber.TabIndex = 5;

            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new Point(20, 135);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(78, 15);
            lblPhoneNumber.TabIndex = 4;
            lblPhoneNumber.Text = "Số điện thoại";

            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(20, 95);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(308, 23);
            txtCustomerName.TabIndex = 3;

            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(20, 75);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(89, 15);
            lblCustomerName.TabIndex = 2;
            lblCustomerName.Text = "Tên khách hàng";

            // 
            // txtCustomerId
            // 
            txtCustomerId.Location = new Point(20, 35);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(308, 23);
            txtCustomerId.TabIndex = 1;

            // 
            // lblCustomerId
            // 
            lblCustomerId.AutoSize = true;
            lblCustomerId.Location = new Point(20, 15);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Size = new Size(38, 15);
            lblCustomerId.TabIndex = 0;
            lblCustomerId.Text = "Mã ID";

            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 571);
            Controls.Add(grpInfo);
            Controls.Add(grpList);
            Controls.Add(grpSearch);
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Khách hàng";
            Load += FormCustomerManagement_Load;

            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLoad;

        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dgvCustomers;

        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblRewardPoints;
        private System.Windows.Forms.TextBox txtRewardPoints;
        private System.Windows.Forms.Label lblMembershipRank;
        private System.Windows.Forms.TextBox txtMembershipRank;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
    }
}