using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            layoutMain = new TableLayoutPanel();
            layoutSearch = new TableLayoutPanel();
            layoutContent = new TableLayoutPanel();
            layoutDetails = new TableLayoutPanel();
            layoutButtons = new TableLayoutPanel();

            grpSearch = new GroupBox();
            grpProducts = new GroupBox();
            grpDetails = new GroupBox();

            lblSearchBarcode = new Label();
            lblFilterCategory = new Label();
            lblId = new Label();
            lblBarcode = new Label();
            lblProductName = new Label();
            lblPrice = new Label();
            lblStock = new Label();
            lblCategory = new Label();
            lblNote = new Label();

            txtSearchBarcode = new TextBox();
            cboFilterCategory = new ComboBox();
            btnSearch = new Button();

            dgvProducts = new DataGridView();

            txtId = new TextBox();
            txtBarcode = new TextBox();
            txtProductName = new TextBox();
            nudPrice = new NumericUpDown();
            nudStock = new NumericUpDown();
            cboCategory = new ComboBox();

            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();

            layoutMain.SuspendLayout();
            layoutSearch.SuspendLayout();
            layoutContent.SuspendLayout();
            layoutDetails.SuspendLayout();
            layoutButtons.SuspendLayout();

            grpSearch.SuspendLayout();
            grpProducts.SuspendLayout();
            grpDetails.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)dgvProducts)
                .BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice)
                .BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock)
                .BeginInit();

            SuspendLayout();

            // layoutMain
            layoutMain.Name = "layoutMain";
            layoutMain.Dock = DockStyle.Fill;
            layoutMain.ColumnCount = 1;
            layoutMain.RowCount = 2;
            layoutMain.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));
            layoutMain.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100F));
            layoutMain.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));
            layoutMain.Controls.Add(grpSearch, 0, 0);
            layoutMain.Controls.Add(layoutContent, 0, 1);

            // grpSearch
            grpSearch.Name = "grpSearch";
            grpSearch.Text = "TÌM KIẾM SẢN PHẨM";
            grpSearch.Dock = DockStyle.Fill;
            grpSearch.BackColor = Color.White;
            grpSearch.Padding = new Padding(12);
            grpSearch.Margin = new Padding(0, 0, 0, 12);
            grpSearch.Controls.Add(layoutSearch);

            // layoutSearch
            layoutSearch.Name = "layoutSearch";
            layoutSearch.Dock = DockStyle.Fill;
            layoutSearch.ColumnCount = 5;
            layoutSearch.RowCount = 1;
            layoutSearch.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 70F));
            layoutSearch.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));
            layoutSearch.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 100F));
            layoutSearch.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));
            layoutSearch.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 120F));
            layoutSearch.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            layoutSearch.Controls.Add(lblSearchBarcode, 0, 0);
            layoutSearch.Controls.Add(txtSearchBarcode, 1, 0);
            layoutSearch.Controls.Add(lblFilterCategory, 2, 0);
            layoutSearch.Controls.Add(cboFilterCategory, 3, 0);
            layoutSearch.Controls.Add(btnSearch, 4, 0);

            // lblSearchBarcode
            lblSearchBarcode.Name = "lblSearchBarcode";
            lblSearchBarcode.Text = "Mã vạch";
            lblSearchBarcode.Dock = DockStyle.Fill;
            lblSearchBarcode.TextAlign = ContentAlignment.MiddleLeft;

            // txtSearchBarcode
            txtSearchBarcode.Name = "txtSearchBarcode";
            txtSearchBarcode.Anchor =
                AnchorStyles.Left | AnchorStyles.Right;
            txtSearchBarcode.PlaceholderText = "Nhập mã vạch...";
            txtSearchBarcode.MaxLength = 50;
            txtSearchBarcode.Margin = new Padding(3, 3, 12, 3);
            txtSearchBarcode.TabIndex = 0;

            // lblFilterCategory
            lblFilterCategory.Name = "lblFilterCategory";
            lblFilterCategory.Text = "Nhóm hàng";
            lblFilterCategory.Dock = DockStyle.Fill;
            lblFilterCategory.TextAlign = ContentAlignment.MiddleLeft;

            // cboFilterCategory
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Anchor =
                AnchorStyles.Left | AnchorStyles.Right;
            cboFilterCategory.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cboFilterCategory.Margin = new Padding(3, 3, 12, 3);
            cboFilterCategory.TabIndex = 1;

            // btnSearch
            btnSearch.Name = "btnSearch";
            btnSearch.Text = "TÌM KIẾM";
            btnSearch.Anchor =
                AnchorStyles.Left | AnchorStyles.Right;
            btnSearch.Height = 36;
            btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.TabIndex = 2;

            // layoutContent
            layoutContent.Name = "layoutContent";
            layoutContent.Dock = DockStyle.Fill;
            layoutContent.Margin = new Padding(0);
            layoutContent.ColumnCount = 2;
            layoutContent.RowCount = 1;
            layoutContent.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 68F));
            layoutContent.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 32F));
            layoutContent.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));
            layoutContent.Controls.Add(grpProducts, 0, 0);
            layoutContent.Controls.Add(grpDetails, 1, 0);

            // grpProducts
            grpProducts.Name = "grpProducts";
            grpProducts.Text = "DANH SÁCH SẢN PHẨM";
            grpProducts.Dock = DockStyle.Fill;
            grpProducts.BackColor = Color.White;
            grpProducts.Padding = new Padding(12);
            grpProducts.Margin = new Padding(0, 0, 8, 0);
            grpProducts.Controls.Add(dgvProducts);

            // dgvProducts
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.MultiSelect = false;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersHeight = 42;
            dgvProducts.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 41, 59);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvProducts.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F);
            dgvProducts.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);
            dgvProducts.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(15, 23, 42);
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);
            dgvProducts.GridColor = Color.FromArgb(226, 232, 240);
            dgvProducts.RowTemplate.Height = 36;
            dgvProducts.TabIndex = 0;

            // grpDetails
            grpDetails.Name = "grpDetails";
            grpDetails.Text = "THÔNG TIN SẢN PHẨM";
            grpDetails.Dock = DockStyle.Fill;
            grpDetails.BackColor = Color.White;
            grpDetails.Padding = new Padding(12);
            grpDetails.Margin = new Padding(4, 0, 0, 0);
            grpDetails.Controls.Add(layoutDetails);

            // layoutDetails
            layoutDetails.Name = "layoutDetails";
            layoutDetails.Dock = DockStyle.Fill;
            layoutDetails.AutoScroll = true;
            layoutDetails.ColumnCount = 1;
            layoutDetails.RowCount = 14;
            layoutDetails.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 26F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 38F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 26F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 38F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 26F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 38F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 26F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 38F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 26F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 38F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 26F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 38F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 108F));
            layoutDetails.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 64F));

            layoutDetails.Controls.Add(lblId, 0, 0);
            layoutDetails.Controls.Add(txtId, 0, 1);
            layoutDetails.Controls.Add(lblBarcode, 0, 2);
            layoutDetails.Controls.Add(txtBarcode, 0, 3);
            layoutDetails.Controls.Add(lblProductName, 0, 4);
            layoutDetails.Controls.Add(txtProductName, 0, 5);
            layoutDetails.Controls.Add(lblPrice, 0, 6);
            layoutDetails.Controls.Add(nudPrice, 0, 7);
            layoutDetails.Controls.Add(lblStock, 0, 8);
            layoutDetails.Controls.Add(nudStock, 0, 9);
            layoutDetails.Controls.Add(lblCategory, 0, 10);
            layoutDetails.Controls.Add(cboCategory, 0, 11);
            layoutDetails.Controls.Add(layoutButtons, 0, 12);
            layoutDetails.Controls.Add(lblNote, 0, 13);

            // lblId
            lblId.Name = "lblId";
            lblId.Text = "Mã sản phẩm";
            lblId.Dock = DockStyle.Fill;
            lblId.TextAlign = ContentAlignment.MiddleLeft;

            // txtId
            txtId.Name = "txtId";
            txtId.Dock = DockStyle.Top;
            txtId.ReadOnly = true;
            txtId.BackColor = Color.FromArgb(241, 245, 249);
            txtId.PlaceholderText = "Tự động tạo";
            txtId.TabStop = false;

            // lblBarcode
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Text = "Mã vạch";
            lblBarcode.Dock = DockStyle.Fill;
            lblBarcode.TextAlign = ContentAlignment.MiddleLeft;

            // txtBarcode
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Dock = DockStyle.Top;
            txtBarcode.MaxLength = 50;
            txtBarcode.PlaceholderText = "Ví dụ: 893000000016";
            txtBarcode.TabIndex = 0;

            // lblProductName
            lblProductName.Name = "lblProductName";
            lblProductName.Text = "Tên sản phẩm";
            lblProductName.Dock = DockStyle.Fill;
            lblProductName.TextAlign = ContentAlignment.MiddleLeft;

            // txtProductName
            txtProductName.Name = "txtProductName";
            txtProductName.Dock = DockStyle.Top;
            txtProductName.MaxLength = 150;
            txtProductName.PlaceholderText = "Nhập tên sản phẩm";
            txtProductName.TabIndex = 1;

            // lblPrice
            lblPrice.Name = "lblPrice";
            lblPrice.Text = "Đơn giá (VNĐ)";
            lblPrice.Dock = DockStyle.Fill;
            lblPrice.TextAlign = ContentAlignment.MiddleLeft;

            // nudPrice
            nudPrice.Name = "nudPrice";
            nudPrice.Dock = DockStyle.Top;
            nudPrice.Minimum = 0;
            nudPrice.Maximum = 9999999999999999.99m;
            nudPrice.DecimalPlaces = 2;
            nudPrice.Increment = 1000m;
            nudPrice.ThousandsSeparator = true;
            nudPrice.TabIndex = 2;

            // lblStock
            lblStock.Name = "lblStock";
            lblStock.Text = "Số lượng tồn kho";
            lblStock.Dock = DockStyle.Fill;
            lblStock.TextAlign = ContentAlignment.MiddleLeft;

            // nudStock
            nudStock.Name = "nudStock";
            nudStock.Dock = DockStyle.Top;
            nudStock.Minimum = 0;
            nudStock.Maximum = int.MaxValue;
            nudStock.DecimalPlaces = 0;
            nudStock.ThousandsSeparator = true;
            nudStock.TabIndex = 3;

            // lblCategory
            lblCategory.Name = "lblCategory";
            lblCategory.Text = "Nhóm hàng";
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.TextAlign = ContentAlignment.MiddleLeft;

            // cboCategory
            cboCategory.Name = "cboCategory";
            cboCategory.Dock = DockStyle.Top;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.TabIndex = 4;

            // layoutButtons
            layoutButtons.Name = "layoutButtons";
            layoutButtons.Dock = DockStyle.Fill;
            layoutButtons.Margin = new Padding(0, 8, 0, 0);
            layoutButtons.ColumnCount = 2;
            layoutButtons.RowCount = 2;
            layoutButtons.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));
            layoutButtons.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));
            layoutButtons.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));
            layoutButtons.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            layoutButtons.Controls.Add(btnLoad, 0, 0);
            layoutButtons.Controls.Add(btnAdd, 1, 0);
            layoutButtons.Controls.Add(btnUpdate, 0, 1);
            layoutButtons.Controls.Add(btnDelete, 1, 1);

            // btnLoad
            btnLoad.Name = "btnLoad";
            btnLoad.Text = "TẢI LẠI";
            btnLoad.Dock = DockStyle.Fill;
            btnLoad.BackColor = Color.FromArgb(71, 85, 105);
            btnLoad.ForeColor = Color.White;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Cursor = Cursors.Hand;
            btnLoad.TabIndex = 0;

            // btnAdd
            btnAdd.Name = "btnAdd";
            btnAdd.Text = "THÊM";
            btnAdd.Dock = DockStyle.Fill;
            btnAdd.BackColor = Color.FromArgb(22, 163, 74);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.TabIndex = 1;

            // btnUpdate
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Text = "CẬP NHẬT";
            btnUpdate.Dock = DockStyle.Fill;
            btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.TabIndex = 2;

            // btnDelete
            btnDelete.Name = "btnDelete";
            btnDelete.Text = "XÓA";
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.TabIndex = 3;

            // lblNote
            lblNote.Name = "lblNote";
            lblNote.Text =
                "Chọn một sản phẩm trong bảng để sửa hoặc xóa.\r\n" +
                "Nhấn Tải lại để làm mới dữ liệu.";
            lblNote.Dock = DockStyle.Fill;
            lblNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblNote.Font = new Font("Segoe UI", 9F);
            lblNote.Padding = new Padding(0, 8, 0, 0);

            // FormProductManagement
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1180, 720);
            Font = new Font("Segoe UI", 9F);
            Padding = new Padding(12);
            Name = "FormProductManagement";
            Text = "Quản lý sản phẩm & kho hàng";
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(layoutMain);

            layoutMain.ResumeLayout(false);
            layoutSearch.ResumeLayout(false);
            layoutSearch.PerformLayout();
            layoutContent.ResumeLayout(false);
            layoutDetails.ResumeLayout(false);
            layoutDetails.PerformLayout();
            layoutButtons.ResumeLayout(false);

            grpSearch.ResumeLayout(false);
            grpProducts.ResumeLayout(false);
            grpDetails.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)dgvProducts)
                .EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice)
                .EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStock)
                .EndInit();

            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutMain = null!;
        private TableLayoutPanel layoutSearch = null!;
        private TableLayoutPanel layoutContent = null!;
        private TableLayoutPanel layoutDetails = null!;
        private TableLayoutPanel layoutButtons = null!;

        private GroupBox grpSearch = null!;
        private GroupBox grpProducts = null!;
        private GroupBox grpDetails = null!;

        private Label lblSearchBarcode = null!;
        private Label lblFilterCategory = null!;
        private Label lblId = null!;
        private Label lblBarcode = null!;
        private Label lblProductName = null!;
        private Label lblPrice = null!;
        private Label lblStock = null!;
        private Label lblCategory = null!;
        private Label lblNote = null!;

        private TextBox txtSearchBarcode = null!;
        private ComboBox cboFilterCategory = null!;
        private Button btnSearch = null!;

        private DataGridView dgvProducts = null!;

        private TextBox txtId = null!;
        private TextBox txtBarcode = null!;
        private TextBox txtProductName = null!;
        private NumericUpDown nudPrice = null!;
        private NumericUpDown nudStock = null!;
        private ComboBox cboCategory = null!;

        private Button btnLoad = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
    }
}