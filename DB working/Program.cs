using System;
using MySql.Data.MySqlClient;

namespace DB_working
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionStr = "server=localhost;user=root;password=root";  //підключення до серверу MySQL
            MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
            try
            {
                mySqlConnection.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            Console.WriteLine(mySqlConnection.State);
            Console.WriteLine();

            string commandLine = "SHOW DATABASES";  //відображення усіх БД на сервері
            MySqlCommand command = new MySqlCommand(commandLine, mySqlConnection);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine(reader[0].ToString());
            }
            reader.Close();
            mySqlConnection.ChangeDatabase("headphonesdb");  //підключення до БД

            //створення нової таблиці
            commandLine = "CREATE TABLE Headphones_List (id INT NOT NULL AUTO_INCREMENT, Name VARCHAR(100) NOT NULL, impedance TINYINT(2) UNSIGNED NOT NULL, microphone TINYINT(1) UNSIGNED NOT NULL DEFAULT 0, PRIMARY KEY(id))";
            command = new MySqlCommand(commandLine, mySqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            //додавання даних у таблицю
            commandLine = "INSERT Headphones_List (Name, impedance, microphone) VALUES ('Sony 12x', 32, 1);" +
                "INSERT Headphones_List (Name, impedance) VALUES ('Philips 31ps', 64);" +
                "INSERT Headphones_List (Name, impedance, microphone) VALUES ('Sony 2ts', 16, 0);" +
                "INSERT Headphones_List (Name, impedance, microphone) VALUES ('Creative Hider', 32, 1);";
            command = new MySqlCommand(commandLine, mySqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            //виведення даних таблиці
            commandLine = "SELECT * FROM Headphones_List";
            command = new MySqlCommand(commandLine, mySqlConnection);
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            Console.WriteLine();
            while (reader.Read())
            {
                Console.WriteLine($"\n{reader[0].ToString()}  {reader[1].ToString()}  -  {reader[2].ToString()}  {reader[3].ToString()}");
            }
            reader.Close();

            //видалення даних
            commandLine = "DELETE FROM Headphones_List WHERE Name='Sony 2ts'";
            command = new MySqlCommand(commandLine, mySqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            //додавання нового стовпця
            commandLine = "ALTER TABLE Headphones_List ADD price float(7,2) NOT NULL DEFAULT 0";
            command = new MySqlCommand(commandLine, mySqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            //оновлення даних
            commandLine = "UPDATE Headphones_List SET Name='Philips 31p', price = 1199.99 WHERE id=2";
            command = new MySqlCommand(commandLine, mySqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            
            //виведення даних таблиці
            commandLine = "SELECT * FROM Headphones_List";
            command = new MySqlCommand(commandLine, mySqlConnection);
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            Console.WriteLine();
            while (reader.Read())
            {
                Console.WriteLine($"\n{reader[0].ToString()}  {reader[1].ToString()}  -  {reader[2].ToString()}  {reader[3].ToString()}  {reader[4].ToString()}");
            }
            reader.Close();
            Console.ReadKey();
            commandLine = "DROP TABLE Headphones_List";
            command = new MySqlCommand(commandLine, mySqlConnection);
            command.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
