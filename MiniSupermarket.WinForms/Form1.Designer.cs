#nullable enable annotations

using System.Drawing;
using System.Windows.Forms;
using MiniSupermarket.WinForms.UI;

namespace MiniSupermarket.WinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Font = UiTheme.BodyFont;
            ForeColor = UiTheme.Ink;
            BackColor = UiTheme.Background;
            ClientSize = new Size(1120, 720);
            Padding = new Padding(24);
            AutoScroll = true;
            Name = "Form1";
            Text = "MiniMart • MiniMart";
            StartPosition = FormStartPosition.CenterScreen;
            pageLayout = new TableLayoutPanel();
            pageLayout.Name = "pageLayout";
            pageLayout.Dock = DockStyle.Fill;
            pageLayout.ColumnCount = 1;
            pageLayout.RowCount = 2;
            pageLayout.Margin = new Padding(0);
            pageLayout.Padding = new Padding(0);
            pageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            pageLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pageLayout.MinimumSize = new Size(860, 540);
            pageHeader = new TableLayoutPanel();
            pageHeader.Name = "pageHeader";
            pageHeader.Dock = DockStyle.Fill;
            pageHeader.ColumnCount = 1;
            pageHeader.RowCount = 2;
            pageHeader.Margin = new Padding(0);
            pageHeader.Padding = new Padding(0);
            pageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pageHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            pageHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            pageTitle = new Label();
            pageTitle.Name = "pageTitle";
            pageTitle.Text = "MiniMart";
            pageTitle.AutoSize = false;
            pageTitle.Dock = DockStyle.Fill;
            pageTitle.Font = UiTheme.HeadingFont;
            pageTitle.ForeColor = UiTheme.Ink;
            pageTitle.Margin = new Padding(0);
            pageTitle.TextAlign = ContentAlignment.MiddleLeft;
            pageSubtitle = new Label();
            pageSubtitle.Name = "pageSubtitle";
            pageSubtitle.Text = "Hệ thống quản lý siêu thị mini.";
            pageSubtitle.AutoSize = false;
            pageSubtitle.Dock = DockStyle.Fill;
            pageSubtitle.Font = UiTheme.BodyFont;
            pageSubtitle.ForeColor = UiTheme.Muted;
            pageSubtitle.Margin = new Padding(0);
            pageSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            pageHeader.Controls.Add(pageTitle, 0, 0);
            pageHeader.Controls.Add(pageSubtitle, 0, 1);
            pageLayout.Controls.Add(pageHeader, 0, 0);
            this.Controls.Add(pageLayout);
            welcomePanel = new Panel();
            welcomePanel.Name = "welcomePanel";
            welcomePanel.Dock = DockStyle.Fill;
            welcomePanel.BackColor = UiTheme.Surface;
            welcomePanel.Padding = new Padding(18);
            welcomePanel.Margin = new Padding(0);
            welcomeText = new Label();
            welcomeText.Name = "welcomeText";
            welcomeText.Text = "Chọn một chức năng từ thanh điều hướng để bắt đầu.";
            welcomeText.AutoSize = false;
            welcomeText.Dock = DockStyle.Fill;
            welcomeText.Font = UiTheme.SectionFont;
            welcomeText.ForeColor = UiTheme.Ink;
            welcomeText.Margin = new Padding(0);
            welcomeText.TextAlign = ContentAlignment.MiddleLeft;
            welcomePanel.Controls.Add(welcomeText);
            pageLayout.Controls.Add(welcomePanel, 0, 1);
            UiTheme.Apply(this);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TableLayoutPanel pageLayout = null!;
        private TableLayoutPanel pageHeader = null!;
        private Label pageTitle = null!;
        private Label pageSubtitle = null!;
        private Panel welcomePanel = null!;
        private Label welcomeText = null!;
    }
}
