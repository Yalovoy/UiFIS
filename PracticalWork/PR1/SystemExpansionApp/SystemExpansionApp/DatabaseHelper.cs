using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SystemExpansionApp
{
    public class DatabaseHelper
    {
        private string connectionString = @"Data Source=dmitriyalovoy;Initial Catalog=SystemExpansionDB;Integrated Security=True;";

        public DatabaseHelper()
        {

        }

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetAllProposals()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                        id AS [ID],
                        department AS [Подразделение],
                        proposal_name AS [Предложение],
                        priority AS [Приоритет],
                        FORMAT(cost, 'N0') + ' ₽' AS [Стоимость],
                        CONVERT(NVARCHAR, implementation_date, 104) AS [Срок реализации],
                        justification AS [Обоснование],
                        status AS [Статус]
                    FROM proposal 
                    ORDER BY 
                        CASE priority 
                            WHEN 'Высокий' THEN 1
                            WHEN 'Средний' THEN 2
                            WHEN 'Низкий' THEN 3
                        END,
                        cost DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public List<string> GetDepartments()
        {
            List<string> departments = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT department_name FROM departments ORDER BY department_name";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        departments.Add(reader["department_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке подразделений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return departments;
        }

        public List<string> GetPriorities()
        {
            List<string> priorities = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT priority_name FROM priorities ORDER BY id";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        priorities.Add(reader["priority_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке приоритетов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return priorities;
        }

        public List<string> GetStatuses()
        {
            return new List<string> { "Новое", "На рассмотрении", "В работе", "Утверждено", "Отклонено", "Завершено" };
        }

        public bool AddProposal(string department, string proposalName, string priority, decimal cost,
                               DateTime implementationDate, string justification, string status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO proposal 
                        (department, proposal_name, priority, cost, implementation_date, justification, status)
                    VALUES (@department, @name, @priority, @cost, @date, @justification, @status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@department", department);
                    cmd.Parameters.AddWithValue("@name", proposalName);
                    cmd.Parameters.AddWithValue("@priority", priority);
                    cmd.Parameters.AddWithValue("@cost", cost);
                    cmd.Parameters.AddWithValue("@date", implementationDate);
                    cmd.Parameters.AddWithValue("@justification", justification ?? "");
                    cmd.Parameters.AddWithValue("@status", status);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении предложения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateProposal(int id, string department, string proposalName, string priority, decimal cost,
                                  DateTime implementationDate, string justification, string status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE proposal SET 
                        department = @department,
                        proposal_name = @name,
                        priority = @priority,
                        cost = @cost,
                        implementation_date = @date,
                        justification = @justification,
                        status = @status
                    WHERE id = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@department", department);
                    cmd.Parameters.AddWithValue("@name", proposalName);
                    cmd.Parameters.AddWithValue("@priority", priority);
                    cmd.Parameters.AddWithValue("@cost", cost);
                    cmd.Parameters.AddWithValue("@date", implementationDate);
                    cmd.Parameters.AddWithValue("@justification", justification ?? "");
                    cmd.Parameters.AddWithValue("@status", status);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении предложения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteProposal(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM proposal WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении предложения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataRow GetProposalDetails(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                        id,
                        department,
                        proposal_name,
                        priority,
                        cost,
                        implementation_date,
                        justification,
                        status,
                        created_date
                    FROM proposal WHERE id = @id";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                        return dt.Rows[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public DataTable GetStatistics()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                        department AS [Подразделение],
                        COUNT(*) AS [Количество предложений],
                        SUM(cost) AS [Общая стоимость],
                        AVG(cost) AS [Средняя стоимость]
                    FROM proposal 
                    GROUP BY department
                    ORDER BY [Общая стоимость] DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении статистики: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }   
    }
}