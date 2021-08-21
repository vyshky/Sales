using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace SaleApp
{
    public class Program
    {
        public static void Main()
        {
            const string CONN_STRING =
                "Server=mysql60.hostland.ru;Database=host1323541_sbd01;Uid=host1323541_itstep;Pwd=269f43dc;";
            var db = new MySqlConnection(CONN_STRING);
            db.Open();
            Console.WriteLine("Выбериет продукт, остатки которого хотите посмотреть");
            Console.WriteLine("1.Phone");
            Console.WriteLine("2.Car");
            var select = Console.ReadLine();

            var sql =
                $"SELECT count FROM tab_products_stock JOIN tab_products ON tab_products_stock.product_id = tab_products.id  WHERE product_id = {select};";
            var command = new MySqlCommand
            {
                CommandText = sql,
                Connection = db
            };
            var res = command.ExecuteReader();

            if (res.HasRows)
            {
                do
                {
                    res.Read();
                    var count = res.GetInt32("count");
                    ShowInfo($"count = {count}");
                }
                while (res.NextResult()) ;
            }
            else
            {
                ShoweError("Вернулясь пустая таблица");
            }

            db.Close();
        }

        static void ShoweError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]{message}");
            Console.ResetColor();
        }

        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS]{message}");
            Console.ResetColor();
        }

        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[INFO]{message}");
            Console.ResetColor();
        }
    }
}