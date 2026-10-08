using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelFilters = new Panel();
            lblReportDate = new Label();
            dtpReportDate = new DateTimePicker();
            btnRunReport = new Button();

            tableCards = new TableLayoutPanel();

            panelOrders = new Panel();
            lblOrdersTitle = new Label();
            lblTotalOrders = new Label();

            panelRevenue = new Panel();
            lblRevenueTitle = new Label();
            lblTotalRevenue = new Label();

            panelBestSeller = new Panel();
            lblBestSellerTitle = new Label();
            lblBestSeller = new Label();

            lblReportNote = new Label();

            panelFilters.SuspendLayout();
            tableCards.SuspendLayout();
            panelOrders.SuspendLayout();
            panelRevenue.SuspendLayout();
            panelBestSeller.SuspendLayout();
            SuspendLayout();

            //
            // panelFilters
            //
            panelFilters.Controls.Add(lblReportDate);
            panelFilters.Controls.Add(dtpReportDate);
            panelFilters.Controls.Add(btnRunReport);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(968, 80);

            //
            // lblReportDate
            //
            lblReportDate.AutoSize = true;
            lblReportDate.Location = new Point(8, 12);
            lblReportDate.Text = "Ngày xem báo cáo";

            //
            // dtpReportDate
            //
            dtpReportDate.CustomFormat = "dd/MM/yyyy";
            dtpReportDate.Format = DateTimePickerFormat.Custom;
            dtpReportDate.Location = new Point(8, 35);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(190, 23);
            dtpReportDate.TabIndex = 0;

            //
            // btnRunReport
            //
            btnRunReport.BackColor = Color.FromArgb(41, 100, 180);
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(215, 29);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(170, 36);
            btnRunReport.TabIndex = 1;
            btnRunReport.Text = "Xem doanh thu ca";
            btnRunReport.UseVisualStyleBackColor = false;
            btnRunReport.Click += btnRunReport_Click;

            //
            // tableCards
            //
            tableCards.ColumnCount = 3;
            tableCards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.33F));
            tableCards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.33F));
            tableCards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.34F));
            tableCards.Controls.Add(panelOrders, 0, 0);
            tableCards.Controls.Add(panelRevenue, 1, 0);
            tableCards.Controls.Add(panelBestSeller, 2, 0);
            tableCards.Dock = DockStyle.Top;
            tableCards.Name = "tableCards";
            tableCards.RowCount = 1;
            tableCards.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));
            tableCards.Size = new Size(968, 190);
            tableCards.TabIndex = 1;

            //
            // panelOrders
            //
            panelOrders.BackColor = Color.FromArgb(41, 100, 180);
            panelOrders.Controls.Add(lblTotalOrders);
            panelOrders.Controls.Add(lblOrdersTitle);
            panelOrders.Dock = DockStyle.Fill;
            panelOrders.Margin = new Padding(8);
            panelOrders.Name = "panelOrders";
            panelOrders.Padding = new Padding(16);

            //
            // lblOrdersTitle
            //
            lblOrdersTitle.Dock = DockStyle.Top;
            lblOrdersTitle.Font =
                new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOrdersTitle.ForeColor = Color.White;
            lblOrdersTitle.Height = 40;
            lblOrdersTitle.Text = "TỔNG SỐ HÓA ĐƠN";
            lblOrdersTitle.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblTotalOrders
            //
            lblTotalOrders.Dock = DockStyle.Fill;
            lblTotalOrders.Font =
                new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.White;
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Text = "--";
            lblTotalOrders.TextAlign = ContentAlignment.MiddleLeft;

            //
            // panelRevenue
            //
            panelRevenue.BackColor = Color.FromArgb(22, 140, 95);
            panelRevenue.Controls.Add(lblTotalRevenue);
            panelRevenue.Controls.Add(lblRevenueTitle);
            panelRevenue.Dock = DockStyle.Fill;
            panelRevenue.Margin = new Padding(8);
            panelRevenue.Name = "panelRevenue";
            panelRevenue.Padding = new Padding(16);

            //
            // lblRevenueTitle
            //
            lblRevenueTitle.Dock = DockStyle.Top;
            lblRevenueTitle.Font =
                new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.White;
            lblRevenueTitle.Height = 40;
            lblRevenueTitle.Text = "TỔNG DOANH THU";
            lblRevenueTitle.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblTotalRevenue
            //
            lblTotalRevenue.Dock = DockStyle.Fill;
            lblTotalRevenue.Font =
                new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.White;
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Text = "--";
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleLeft;

            //
            // panelBestSeller
            //
            panelBestSeller.BackColor = Color.FromArgb(145, 90, 190);
            panelBestSeller.Controls.Add(lblBestSeller);
            panelBestSeller.Controls.Add(lblBestSellerTitle);
            panelBestSeller.Dock = DockStyle.Fill;
            panelBestSeller.Margin = new Padding(8);
            panelBestSeller.Name = "panelBestSeller";
            panelBestSeller.Padding = new Padding(16);

            //
            // lblBestSellerTitle
            //
            lblBestSellerTitle.Dock = DockStyle.Top;
            lblBestSellerTitle.Font =
                new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBestSellerTitle.ForeColor = Color.White;
            lblBestSellerTitle.Height = 40;
            lblBestSellerTitle.Text = "MẶT HÀNG BÁN CHẠY";
            lblBestSellerTitle.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblBestSeller
            //
            lblBestSeller.Dock = DockStyle.Fill;
            lblBestSeller.Font =
                new Font("Segoe UI", 18F, FontStyle.Bold);
            lblBestSeller.ForeColor = Color.White;
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Text = "--";
            lblBestSeller.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblReportNote
            //
            lblReportNote.Dock = DockStyle.Fill;
            lblReportNote.ForeColor = Color.FromArgb(75, 85, 99);
            lblReportNote.Name = "lblReportNote";
            lblReportNote.Padding = new Padding(8, 20, 8, 0);
            lblReportNote.Text =
                "Chọn ngày và bấm Xem doanh thu ca.\n" +
                "Dữ liệu minh họa cho bài tập phân quyền.";

            //
            // FormQuickReport
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1000, 600);
            Controls.Add(lblReportNote);
            Controls.Add(tableCards);
            Controls.Add(panelFilters);
            Name = "FormQuickReport";
            Padding = new Padding(16);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo doanh thu nhanh";
            Load += FormQuickReport_Load;

            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            tableCards.ResumeLayout(false);
            panelOrders.ResumeLayout(false);
            panelRevenue.ResumeLayout(false);
            panelBestSeller.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFilters = null!;
        private Label lblReportDate = null!;
        private DateTimePicker dtpReportDate = null!;
        private Button btnRunReport = null!;

        private TableLayoutPanel tableCards = null!;

        private Panel panelOrders = null!;
        private Label lblOrdersTitle = null!;
        private Label lblTotalOrders = null!;

        private Panel panelRevenue = null!;
        private Label lblRevenueTitle = null!;
        private Label lblTotalRevenue = null!;

        private Panel panelBestSeller = null!;
        private Label lblBestSellerTitle = null!;
        private Label lblBestSeller = null!;

        private Label lblReportNote = null!;
    }
}