namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            //Phần gọi form hiển thị
            var categoryForm = new FormCategoryManagement();
            var roleForm = new FormRoleManagement();

            roleForm.Show();

            Application.Run(categoryForm);
        }
    }
}