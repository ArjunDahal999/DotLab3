using System;
using System.Data.SqlClient;
namespace database
{
    internal class Program
    {
        public static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StdDb;Integrated Security=True;Connect Timeout=30;";

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. View All Students");
                Console.WriteLine("5. Delete Entire Database");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter roll:");
                        int roll = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter address:");
                        string address = Console.ReadLine();
                        CreateStudent(name, roll, address);
                        break;
                    case "2":
                        Console.WriteLine("Enter student ID to update:");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter updated name:");
                        string updatedName = Console.ReadLine();
                        Console.WriteLine("Enter updated roll:");
                        int updatedRoll = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter updated address:");
                        string updatedAddress = Console.ReadLine();
                        UpdateStudent(updateId, updatedName, updatedRoll, updatedAddress);
                        break;
                    case "3":
                        Console.WriteLine("Enter student ID to delete:");
                        int deleteId = int.Parse(Console.ReadLine());
                        DeleteStudent(deleteId);
                        break;
                    case "4":
                        GetAllStudent();
                        break;
                    case "5":
                        DeleteEntireDB();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }


        static void DeleteStudent(int id)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                string query = " DELETE FROM Students WHERE Id = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                var result = command.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? $"Student deleted successfully with id {id}." : "Failed to delete student.");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {

                if (connection != null)
                {
                    connection.Close();
                }
            }

        }
        static void CreateStudent(string name, int roll, string address)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                string query = "INSERT INTO Students (Name, Roll, Address) VALUES (@Name, @Roll , @Address)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Roll", roll);
                command.Parameters.AddWithValue("@Address", address);

                int result = command.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? "Student created successfully." : "Failed to create student.");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {

                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        static void GetAllStudent()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                var query = " SELECT * FROM Students ";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Roll: {reader["Roll"]} , Address: {reader["Address"]}");
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {

                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        static void UpdateStudent(int id, string name, int roll, string address)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                string query = "UPDATE Students SET Name = @Name, Roll = @Roll, Address = @Address WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Roll", roll);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Id", id);

                int result = command.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? "Student Updated successfully." : "Failed to create student.");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {

                if (connection != null)
                {
                    connection.Close();
                }
            }

        }

        static void DeleteEntireDB()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                string query = "DROP TABLE Students";
                SqlCommand command = new SqlCommand(query, connection);
                var result = command.ExecuteNonQuery();
                Console.WriteLine(result);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {

                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        static void CreateStudentTable()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE Students (
                    Id INT IDENTITY(1, 1) PRIMARY KEY,
                    Name VARCHAR(255),
                    Address CHAR(255),
                    Roll INT
                );";

                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


    }



}
