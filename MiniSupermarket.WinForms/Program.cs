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
            var loginForm = new FormLogin();

            //roleForm.Show();

            Application.Run(loginForm);
        }
    }
}