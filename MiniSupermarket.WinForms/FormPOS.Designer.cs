using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormPOS
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
            tableLayoutMain = new TableLayoutPanel();
            grpCart = new GroupBox();
            dgvCart = new DataGridView();
            panelBarcode = new Panel();
            lblBarcodeTitle = new Label();
            txtBarcode = new TextBox();
            grpPayment = new GroupBox();
            lblPhoneTitle = new Label();
            txtCustomerPhone = new TextBox();
            lblCustomerName = new Label();
            lblTotalTitle = new Label();
            lblTotalAmount = new Label();
            lblCashTitle = new Label();
            txtCashReceived = new TextBox();
            lblChangeTitle = new Label();
            lblChange = new Label();
            btnCheckout = new Button();
            btnClearCart = new Button();
            tableLayoutMain.SuspendLayout();
            grpCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            panelBarcode.SuspendLayout();
            grpPayment.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 2;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutMain.Controls.Add(grpCart, 0, 0);
            tableLayoutMain.Controls.Add(grpPayment, 1, 0);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(12, 12);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 1;
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.Size = new Size(976, 576);
            tableLayoutMain.TabIndex = 0;
            // 
            // grpCart
            // 
            grpCart.Controls.Add(dgvCart);
            grpCart.Controls.Add(panelBarcode);
            grpCart.Dock = DockStyle.Fill;
            grpCart.Location = new Point(3, 3);
            grpCart.Name = "grpCart";
            grpCart.Padding = new Padding(12);
            grpCart.Size = new Size(628, 570);
            grpCart.TabIndex = 0;
            grpCart.TabStop = false;
            grpCart.Text = "Giỏ hàng";
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Location = new Point(12, 103);
            dgvCart.MultiSelect = false;
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersVisible = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(604, 455);
            dgvCart.TabIndex = 1;
            // 
            // panelBarcode
            // 
            panelBarcode.Controls.Add(lblBarcodeTitle);
            panelBarcode.Controls.Add(txtBarcode);
            panelBarcode.Dock = DockStyle.Top;
            panelBarcode.Location = new Point(12, 28);
            panelBarcode.Name = "panelBarcode";
            panelBarcode.Size = new Size(604, 75);
            panelBarcode.TabIndex = 2;
            // 
            // lblBarcodeTitle
            // 
            lblBarcodeTitle.AutoSize = true;
            lblBarcodeTitle.Location = new Point(0, 5);
            lblBarcodeTitle.Name = "lblBarcodeTitle";
            lblBarcodeTitle.Size = new Size(196, 15);
            lblBarcodeTitle.TabIndex = 0;
            lblBarcodeTitle.Text = "Quét / nhập mã vạch rồi nhấn Enter";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(0, 30);
            txtBarcode.MaxLength = 50;
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(300, 23);
            txtBarcode.TabIndex = 0;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
            // 
            // grpPayment
            // 
            grpPayment.Controls.Add(lblPhoneTitle);
            grpPayment.Controls.Add(txtCustomerPhone);
            grpPayment.Controls.Add(lblCustomerName);
            grpPayment.Controls.Add(lblTotalTitle);
            grpPayment.Controls.Add(lblTotalAmount);
            grpPayment.Controls.Add(lblCashTitle);
            grpPayment.Controls.Add(txtCashReceived);
            grpPayment.Controls.Add(lblChangeTitle);
            grpPayment.Controls.Add(lblChange);
            grpPayment.Controls.Add(btnCheckout);
            grpPayment.Controls.Add(btnClearCart);
            grpPayment.Dock = DockStyle.Fill;
            grpPayment.Location = new Point(637, 3);
            grpPayment.Name = "grpPayment";
            grpPayment.Size = new Size(336, 570);
            grpPayment.TabIndex = 1;
            grpPayment.TabStop = false;
            grpPayment.Text = "Khách hàng và thanh toán";
            // 
            // lblPhoneTitle
            // 
            lblPhoneTitle.AutoSize = true;
            lblPhoneTitle.Location = new Point(16, 30);
            lblPhoneTitle.Name = "lblPhoneTitle";
            lblPhoneTitle.Size = new Size(193, 15);
            lblPhoneTitle.TabIndex = 0;
            lblPhoneTitle.Text = "SĐT khách hàng — Enter để tra cứu";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerPhone.Location = new Point(16, 55);
            txtCustomerPhone.MaxLength = 20;
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.Size = new Size(314, 23);
            txtCustomerPhone.TabIndex = 0;
            txtCustomerPhone.TextChanged += txtCustomerPhone_TextChanged;
            txtCustomerPhone.KeyDown += txtCustomerPhone_KeyDown;
            // 
            // lblCustomerName
            // 
            lblCustomerName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblCustomerName.Location = new Point(16, 90);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(426, 50);
            lblCustomerName.TabIndex = 1;
            lblCustomerName.Text = "Khách vãng lai";
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Location = new Point(16, 160);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(66, 15);
            lblTotalTitle.TabIndex = 2;
            lblTotalTitle.Text = "TỔNG TIỀN";
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalAmount.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(22, 140, 95);
            lblTotalAmount.Location = new Point(16, 185);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(426, 55);
            lblTotalAmount.TabIndex = 3;
            lblTotalAmount.Text = "0 đ";
            // 
            // lblCashTitle
            // 
            lblCashTitle.AutoSize = true;
            lblCashTitle.Location = new Point(16, 260);
            lblCashTitle.Name = "lblCashTitle";
            lblCashTitle.Size = new Size(161, 15);
            lblCashTitle.TabIndex = 4;
            lblCashTitle.Text = "Tiền khách đưa, ví dụ: 100000";
            // 
            // txtCashReceived
            // 
            txtCashReceived.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCashReceived.Location = new Point(16, 285);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.Size = new Size(314, 23);
            txtCashReceived.TabIndex = 1;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;
            // 
            // lblChangeTitle
            // 
            lblChangeTitle.AutoSize = true;
            lblChangeTitle.Location = new Point(16, 330);
            lblChangeTitle.Name = "lblChangeTitle";
            lblChangeTitle.Size = new Size(65, 15);
            lblChangeTitle.TabIndex = 5;
            lblChangeTitle.Text = "TIỀN THỪA";
            // 
            // lblChange
            // 
            lblChange.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblChange.Location = new Point(16, 355);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(290, 40);
            lblChange.TabIndex = 6;
            lblChange.Text = "0 đ";
            // 
            // btnCheckout
            // 
            btnCheckout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCheckout.BackColor = Color.FromArgb(22, 140, 95);
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(16, 425);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(314, 45);
            btnCheckout.TabIndex = 2;
            btnCheckout.Text = "THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;
            // 
            // btnClearCart
            // 
            btnClearCart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnClearCart.BackColor = Color.FromArgb(190, 55, 55);
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(16, 480);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(314, 40);
            btnClearCart.TabIndex = 3;
            btnClearCart.Text = "HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;
            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1000, 600);
            Controls.Add(tableLayoutMain);
            Name = "FormPOS";
            Padding = new Padding(12);
            Text = "Bán hàng POS";
            FormClosing += FormPOS_FormClosing;
            Load += FormPOS_Load;
            tableLayoutMain.ResumeLayout(false);
            grpCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            panelBarcode.ResumeLayout(false);
            panelBarcode.PerformLayout();
            grpPayment.ResumeLayout(false);
            grpPayment.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutMain = null!;
        private GroupBox grpCart = null!;
        private Panel panelBarcode = null!;
        private Label lblBarcodeTitle = null!;
        private TextBox txtBarcode = null!;
        private DataGridView dgvCart = null!;

        private GroupBox grpPayment = null!;
        private Label lblPhoneTitle = null!;
        private TextBox txtCustomerPhone = null!;
        private Label lblCustomerName = null!;
        private Label lblTotalTitle = null!;
        private Label lblTotalAmount = null!;
        private Label lblCashTitle = null!;
        private TextBox txtCashReceived = null!;
        private Label lblChangeTitle = null!;
        private Label lblChange = null!;
        private Button btnCheckout = null!;
        private Button btnClearCart = null!;
    }
}