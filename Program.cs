using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ConsoleBaseStudent
{
    internal class Program
    {
        
        private static string connectingString = ConfigurationManager.ConnectionStrings["StudentBase"].ConnectionString;
        private static SqlConnection sqlConnection = null;
        static void Main(string[] args)
        {
            #region Подключение и открытие
            sqlConnection = new SqlConnection(connectingString);
            sqlConnection.Open();
            Console.WriteLine("Приветствую в Базе Студентов");

            SqlDataReader sqlReader = null;
            string command = "";
            #endregion 

            //Цикл работы программы
            while (true)
            {
                Console.Write(">>>");
                command = Console.ReadLine();

                if(command.ToLower().Equals("exit"))
                {
                    if(sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                    if (sqlReader != null)
                    {
                        sqlReader.Close();  
                    }
                    break;

                }

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                switch (command.Split(' ')[0].ToLower())
                {
                    case "select":
                        sqlReader = sqlCommand.ExecuteReader();

                        while(sqlReader.Read())
                        {
                            Console.WriteLine($"{sqlReader["Id"]} {sqlReader["FIO"]}" + $"{sqlReader["Birthday"]}"
                                + $"{sqlReader["Univercity"]}" + $"{sqlReader["Group_cource"]}" + $"{sqlReader["Cource"]}"
                                + $"{sqlReader["Average_score"]}");
                            Console.WriteLine(new string('-',60));

                        }
                        if (sqlReader != null)
                        {
                            sqlReader.Close();
                        }

                        break;

                    case "insert":

                        break;

                    case "update":

                        break;

                    case "delete":

                        break;
                        default:
                        Console.WriteLine($"Команда {command} была введена не корректна");

                        break;
                }

            }
            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();


        }
    }
}
