using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms.UI;

/// <summary>Shared visual tokens and control styles for the desktop application.</summary>
internal static class UiTheme
{
    internal static readonly Color Background = Color.FromArgb(245, 247, 250);
    internal static readonly Color Surface = Color.White;
    internal static readonly Color Ink = Color.FromArgb(27, 43, 57);
    internal static readonly Color Muted = Color.FromArgb(103, 119, 133);
    internal static readonly Color Border = Color.FromArgb(222, 229, 235);
    internal static readonly Color Accent = Color.FromArgb(17, 115, 91);
    internal static readonly Color AccentHover = Color.FromArgb(12, 91, 72);
    internal static readonly Color AccentSoft = Color.FromArgb(230, 244, 238);
    internal static readonly Color Sidebar = Color.FromArgb(22, 42, 49);
    internal static readonly Color SidebarHover = Color.FromArgb(35, 62, 68);
    internal static readonly Color Danger = Color.FromArgb(174, 50, 57);
    internal static readonly Font BodyFont = new("Segoe UI", 10F);
    internal static readonly Font StrongFont = new("Segoe UI", 10F, FontStyle.Bold);
    internal static readonly Font HeadingFont = new("Segoe UI", 21F, FontStyle.Bold);
    internal static readonly Font SectionFont = new("Segoe UI", 12F, FontStyle.Bold);
    internal static readonly Font AmountFont = new("Segoe UI", 25F, FontStyle.Bold);

    internal static void Apply(Control root)
    {
        foreach (Control control in root.Controls)
        {
            switch (control)
            {
                case Button button:
                    StyleButton(button);
                    break;
                case DataGridView grid:
                    StyleGrid(grid);
                    break;
                case TextBox text:
                    text.BorderStyle = BorderStyle.FixedSingle;
                    text.BackColor = text.ReadOnly ? Background : Surface;
                    text.ForeColor = Ink;
                    text.ReadOnlyChanged += (_, _) =>
                        text.BackColor = text.ReadOnly ? Background : Surface;
                    break;
                case ComboBox combo:
                    combo.FlatStyle = FlatStyle.Flat;
                    combo.BackColor = Surface;
                    combo.ForeColor = Ink;
                    combo.IntegralHeight = false;
                    combo.DropDownHeight = 240;
                    break;
                case NumericUpDown number:
                    number.BorderStyle = BorderStyle.FixedSingle;
                    number.BackColor = Surface;
                    number.ForeColor = Ink;
                    break;
            }
            Apply(control);
        }
    }

    internal static void StyleButton(Button button)
    {
        string kind = button.Tag as string ?? "secondary";
        button.FlatStyle = FlatStyle.Flat;
        button.UseVisualStyleBackColor = false;
        button.Cursor = Cursors.Hand;
        button.Font = StrongFont;
        button.FlatAppearance.BorderSize = kind == "primary" || kind == "nav" ? 0 : 1;
        button.FlatAppearance.BorderColor = kind == "danger" ? Color.FromArgb(238, 204, 208) : Border;
        button.BackColor = kind switch
        {
            "primary" => Accent,
            "nav" => Sidebar,
            "danger" => Color.FromArgb(255, 246, 246),
            _ => Surface
        };
        button.ForeColor = kind switch
        {
            "primary" or "nav" => Color.White,
            "danger" => Danger,
            _ => Ink
        };
        button.FlatAppearance.MouseOverBackColor = kind switch
        {
            "primary" => AccentHover,
            "nav" => SidebarHover,
            "danger" => Color.FromArgb(252, 232, 233),
            _ => Background
        };
        button.FlatAppearance.MouseDownBackColor = kind == "nav" ? Accent : AccentSoft;
    }

    internal static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Surface;
        grid.BorderStyle = BorderStyle.None;
        grid.EnableHeadersVisualStyles = false;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.GridColor = Border;
        grid.RowHeadersVisible = false;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = 44;
        grid.RowTemplate.Height = 42;
        grid.AllowUserToResizeRows = false;
        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Background, ForeColor = Muted, Font = StrongFont,
            SelectionBackColor = Background, SelectionForeColor = Ink,
            Padding = new Padding(10, 0, 10, 0)
        };
        grid.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Surface, ForeColor = Ink, Font = BodyFont,
            SelectionBackColor = AccentSoft, SelectionForeColor = Ink,
            Padding = new Padding(10, 0, 10, 0)
        };
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 253);
        grid.DataBindingComplete += (_, _) =>
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.MinimumWidth = column is DataGridViewCheckBoxColumn ? 80 : 70;
                column.HeaderText = column.DataPropertyName switch
                {
                    "Id" or "CategoryId" or "CustomerId" => "Mã",
                    "CategoryName" => "Nhóm hàng",
                    "RoleName" => "Vai trò",
                    "Description" => "Mô tả",
                    "CustomerName" => "Khách hàng",
                    "PhoneNumber" => "Điện thoại",
                    "Address" => "Địa chỉ",
                    "RewardPoints" => "Điểm thưởng",
                    "MembershipRank" => "Hạng thành viên",
                    _ => column.HeaderText
                };
            }
        };
    }
}
