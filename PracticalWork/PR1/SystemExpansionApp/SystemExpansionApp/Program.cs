using System;
using System.Windows.Forms;

namespace SystemExpansionApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DatabaseHelper dbHelper = new DatabaseHelper();
            if (dbHelper.TestConnection())
            {
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к базе данных. Проверьте настройки SQL Server.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}