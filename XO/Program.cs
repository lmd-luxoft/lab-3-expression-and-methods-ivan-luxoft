using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
        static char win = '-';
        static string PlayerName1, PlayerName2;
        static char[] cells = new char[] { '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        static void show_cells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{cells[0]}-|-{cells[1]}-|-{cells[2]}-");
            Console.WriteLine($"-{cells[3]}-|-{cells[4]}-|-{cells[5]}-");
            Console.WriteLine($"-{cells[6]}-|-{cells[7]}-|-{cells[8]}-");
        }
        static void make_move(int num)
        {
            string raw_cell;
            int cell;
            if (num == 1)
                Console.Write(PlayerName1);
            else
                Console.Write(PlayerName2);
            do
            {
                Console.Write(",введите номер ячейки,сделайте свой ход:");

                raw_cell = Console.ReadLine();
            }
            while (!Int32.TryParse(raw_cell, out cell));
            while (IsUnCorrectInputCell(cell) || IsCellFill(cell))
            {
                do
                {
                    Console.Write("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");
                    raw_cell = Console.ReadLine();
                }
                while (!Int32.TryParse(raw_cell, out cell));
                Console.WriteLine();
            }
            if (num == 1)
                cells[cell - 1] = 'X';
            else
                cells[cell - 1] = 'O';
        }

        static bool IsCellFill(int cell)
        {
            return cells[cell - 1] == 'O' || cells[cell - 1] == 'X';
        }

        static bool IsUnCorrectInputCell(int cell)
        {
            return cell < 1 || cell > 9;
        }

        static char check()
        {
            if (IsDiagLineClose())
                return cells[0];

            for (int column = 0; column < 3; column++)
                if (IsHorizontalLineClose(column) || IsVerticalLineClose(column))
                    return cells[column];

            return '-';
        }

        static bool IsHorizontalLineClose(int column)
        {
            return cells[column * 3] == cells[column * 3 + 1] && cells[column * 3 + 1] == cells[column * 3 + 2];
        }

        static bool IsVerticalLineClose(int column)
        {
            return cells[column] == cells[column + 3] && cells[column + 3] == cells[column + 6];
        }

        static bool IsDiagLineClose()
        {
            return (cells[2] == cells[4] && cells[4] == cells[6]) || (cells[0] == cells[4] && cells[4] == cells[8]);
        }

        static void result()
        {
            if (win == 'X')
                Console.WriteLine($"{PlayerName1} вы  выиграли поздравляем {PlayerName2} а вы проиграли...");
            else if (win == 'O')
                Console.WriteLine($"{PlayerName2} вы  выиграли поздравляем {PlayerName1} а вы проиграли...");

        }

        static int PlayerShouldPlay(int gameStep)
        {
            return (gameStep - 1) % 2 + 1;
        }

        static void Main(string[] args)
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                PlayerName1 = Console.ReadLine();

                Console.Write("Введите имя второго игрока: ");
                PlayerName2 = Console.ReadLine();
                Console.WriteLine();
            } while (PlayerName1 == PlayerName2);

            show_cells();

            for (int move = 1; move <= 9; move++)
            {
                make_move(PlayerShouldPlay(move));

                show_cells();

                if (move >= 5)
                {
                    win = check();
                    if (IsWinnerFound())
                        break;
                }
            }

            result();
            Console.ReadKey();
        }

        static bool IsWinnerFound()
        {
            return win != '-';
        }
    }
}
