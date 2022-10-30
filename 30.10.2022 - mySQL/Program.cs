using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SQLiteLAB
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string ConnectParametr = "";
            string SQLCommand = "";
            string nameOfFile = "";
            string nameOfTable = "";
            int QuantityOfRows = 0;
            bool NoArguments = false;

            string MyStringToChange = "'***'";
            try
            {
                MyStringToChange = "'" + args[0] + "'";
            }
            catch
            {
                Console.WriteLine("Нет аргументов командной строки");
                NoArguments = true;
            }

            Console.Write("Введите имя файла: ");
            nameOfFile = Console.ReadLine();

            Console.Write("Введите имя таблицы: ");
            nameOfTable = Console.ReadLine();

            Console.Write("Введите колличество строк: ");
            QuantityOfRows = int.Parse(Console.ReadLine());

            int choise = 0;

            if (choise < 0 || choise > 6)
            {
                Console.WriteLine("Ошибка ввода");
                return;
            }

            while (choise != 7)
            {
                Console.WriteLine();
                Console.WriteLine("1. Создание таблицы");
                Console.WriteLine("2. Заполнение таблицы");
                Console.WriteLine("3. Обновление данных в таблице");
                Console.WriteLine("4. Вывести все данные в таблице");
                Console.WriteLine("5. Удалить таблицу");
                Console.WriteLine("6. Удалить строку по условию");
                Console.WriteLine("7. Выход");

                choise = int.Parse(Console.ReadLine());

                switch (choise)
                {
                    case 1:

                        ConnectParametr = $@"Data source = {nameOfFile}" + ".db; Version = 3;Mode=ReadWriteCreate;";

                        SQLCommand = $"CREATE TABLE {nameOfTable} " +
                                        "(id INTEGER PRIMARY KEY ASC AUTOINCREMENT, " +
                                        "name VARCHAR (1, 50), display  VARCHAR (1, 50), " +
                                        "keyboard VARCHAR (1, 50), mouse VARCHAR (1, 50));";

                        SQLiteConnection _sqlite = new SQLiteConnection(ConnectParametr);
                        _sqlite.Open();
                        SQLiteCommand cmd = _sqlite.CreateCommand();
                        cmd.CommandText = (SQLCommand);
                        cmd.ExecuteReader();
                        _sqlite.Close();

                        Console.WriteLine($"Таблица {nameOfTable} создана в файле {nameOfFile}");

                        break;
                    case 2:

                        ConnectParametr = $@"Data source = {nameOfFile}" + ".db; Version = 3;Mode=ReadWriteCreate;";

                        for (int i = 0; i < QuantityOfRows; i++)
                        {
                            SQLCommand = $"INSERT INTO {nameOfTable} " +
                                "(name, display, keyboard, mouse) " +
                                $"VALUES ( 'WorkPC{i}', 'Disp{i}', 'Keyboard{i}', 'Mouse{i}');";

                            SQLiteConnection _sqlite2 = new SQLiteConnection(ConnectParametr);
                            _sqlite2.Open();
                            SQLiteCommand cmd2 = _sqlite2.CreateCommand();
                            cmd2.CommandText = (SQLCommand);
                            cmd2.ExecuteReader();
                            _sqlite2.Close();
                        }

                        Console.WriteLine($"Таблица {nameOfTable} заполнена");

                        break;
                    case 3:

                        ConnectParametr = $@"Data source = {nameOfFile}" + ".db; Version = 3;Mode=ReadWriteCreate;";

                        int ChoiseToChange = 0;
                        string NumberOfRow = "";
                        string MyChange = "";
                       // string MyStringToChange = "";

                        Console.Write("Введите номер строки: ");
                        NumberOfRow = Console.ReadLine();
                                                    
                        Console.WriteLine("Введите что вы хотите изменить: ");
                        Console.WriteLine("1. Name");
                        Console.WriteLine("2. Display");
                        Console.WriteLine("3. Keyboard");
                        Console.WriteLine("4. Mouse");
                        Console.WriteLine("5. Exit");

                        ChoiseToChange = int.Parse(Console.ReadLine());

                        switch (ChoiseToChange)
                        {
                            case 1:
                                MyChange = "name";
                                break;
                            case 2:
                                MyChange = "display";
                                break;
                            case 3:
                                MyChange = "keyboard";
                                break;
                            case 4:
                                MyChange = "mouse";
                                break;
                            case 5:
                                break;
                        }

                        if (NoArguments == true) {
                            Console.Write("Введите новое значение: ");
                            MyStringToChange = Console.ReadLine();
                        }

                        SQLCommand = $"UPDATE {nameOfTable}" + $" SET {MyChange} = '{MyStringToChange}'" + $" WHERE id = {NumberOfRow};";

                        SQLiteConnection _sqlite3 = new SQLiteConnection(ConnectParametr);
                        _sqlite3.Open();
                        SQLiteCommand cmd3 = _sqlite3.CreateCommand();
                        cmd3.CommandText = (SQLCommand);
                        cmd3.ExecuteReader();
                        _sqlite3.Close();

                        Console.WriteLine($"Параметр {MyChange} в строке {NumberOfRow} изменён");

                        break;
                    case 4:

                        ConnectParametr = $@"Data source = {nameOfFile}" + ".db; Version = 3;Mode=ReadWriteCreate;";

                        SQLCommand = $"SELECT * FROM {nameOfTable};";

                        SQLiteConnection _sqlite4 = new SQLiteConnection(ConnectParametr);
                        _sqlite4.Open();
                        SQLiteCommand cmd4 = _sqlite4.CreateCommand();
                        cmd4.CommandText = (SQLCommand);
                        cmd4.ExecuteReader();
                        _sqlite4.Close();

                        Console.WriteLine($"Данные в таблице {nameOfTable} отображены");

                        break;
                    case 5:

                        ConnectParametr = $@"Data source = {nameOfFile}" + ".db; Version = 3;Mode=ReadWriteCreate;";

                        SQLCommand = $"DROP TABLE {nameOfTable};";

                        SQLiteConnection _sqlite5 = new SQLiteConnection(ConnectParametr);
                        _sqlite5.Open();
                        SQLiteCommand cmd5 = _sqlite5.CreateCommand();
                        cmd5.CommandText = (SQLCommand);
                        cmd5.ExecuteReader();
                        _sqlite5.Close();

                        Console.WriteLine($"Таблица {nameOfTable} удалена");

                        break;
                    case 6:

                        ConnectParametr = $@"Data source = {nameOfFile}" + ".db; Version = 3;Mode=ReadWriteCreate;";

                        string StringToDelete = "";
                        Console.Write("Введите номер строки для удаления: ");
                        StringToDelete = Console.ReadLine();

                        if(int.Parse(StringToDelete) < 0 || int.Parse(StringToDelete) > QuantityOfRows)
                        {
                            Console.WriteLine("Ошибка ввода");
                            break;
                        }

                       SQLCommand = $"DELETE FROM {nameOfTable} WHERE id = {StringToDelete};";

                       SQLiteConnection _sqlite6 = new SQLiteConnection(ConnectParametr);
                        _sqlite6.Open();
                        SQLiteCommand cmd6 = _sqlite6.CreateCommand();
                        cmd6.CommandText = (SQLCommand);
                        cmd6.ExecuteReader();
                        _sqlite6.Close();

                        Console.WriteLine($"Строка {StringToDelete} удалена");

                        break;
                    case 7:
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}