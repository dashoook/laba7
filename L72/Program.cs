using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.IO;

namespace L72
{
    class Program
    {
        static void Main(string[] args)
        {

            Key.KKEY();

        }
    }
    class Users : IComparable
    {
        public string Surname;
        public string Group;
        public int Course;
        public string Login;
        public string Password;
        public int Number;
        public DateTime Data;
        public Users(string sur, string group, int course, string login, string pass, int number, DateTime date)
        {
            Surname = sur;
            Group = group;
            Course = course;
            Login = login;
            Password = pass;
            Number = number;
            Data = date;
        }
        public int CompareTo(object user)
        {
            Users p = (Users)user;
            if (this.Number > p.Number) return 1;
            if (this.Number < p.Number) return -1;
            return 0;
        }
        public void Studak(Users[] a)
        {
            Console.WriteLine("\nСортування за номером студентського:\nПрiзвище\t Номер cтудентського");
            Array.Sort(a);
            foreach (Users elem in a) elem.Inf();
        }
        public void Inf()
        {
            Console.WriteLine("{0,-20} {1, -10} ", Surname, Number);
        }

        public class SortByCourse : IComparer
        {

            int IComparer.Compare(object ob1, object ob2)
            {
                Users p1 = (Users)ob1;
                Users p2 = (Users)ob2;
                if (p1.Course > p2.Course) return 1;
                if (p1.Course < p2.Course) return -1;
                return 0;
            }
        }
        public void First(Users[] a)
        {
            Console.WriteLine("\nСортування за курсом:\nПрiзвище\t Номер курсу");
            Array.Sort(a, new Users.SortByCourse());
            foreach (Users elem in a) elem.Info();
        }
        public void Info()
        {
            Console.WriteLine("{0,-20} {1, -10} ", Surname, Course);
        }
        public class SortByDate : IComparer
        {

            int IComparer.Compare(object ob1, object ob2)
            {
                Users p1 = (Users)ob1;
                Users p2 = (Users)ob2;
                if (p1.Data > p2.Data) return 1;
                if (p1.Data < p2.Data) return -1;
                return 0;
            }
        }
        public void Second(Users[] a)
        {
            Console.WriteLine("\nСортування за курсом:\nПрiзвище\t Дата реєстрацiї:");
            Array.Sort(a, new Users.SortByDate());
            foreach (Users elem in a) elem.Info1();
        }
        public void Info1()
        {
            Console.WriteLine("{0,-15} {1, -10} ", Surname, Data);
        }
        public void Add()
        {
            Console.WriteLine("Write data:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);


        }
    }
    class Key
    {
        public static void KKEY()
        {
            FileStream file1 = File.OpenRead("person.txt");
            byte[] array = new byte[file1.Length];
            file1.Read(array, 0, array.Length);
            string textfromfile = System.Text.Encoding.Default.GetString(array);
            string[] s = textfromfile.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            file1.Close();
            Users[] a = new Users[s.Length / 7];
            int c = 0;
            while (a[c] != null)
            {
                ++c;
            }
            for (int i = 0; i < s.Length; i += 7)
            {
                a[c + i / 7] = new Users(s[i], s[i + 1], int.Parse(s[i + 2]), s[i + 3], s[i + 4], int.Parse(s[i + 5]), DateTime.Parse(s[i + 6]));
            }
            bool[] delete = new bool[100];
            Console.WriteLine("Add note: A");
            Console.WriteLine("Edit note: E");
            Console.WriteLine("Remove note: R");
            Console.WriteLine("Show notes: Enter");
            Console.WriteLine("Sort by number: N");
            Console.WriteLine("Sort by date: D");
            Console.WriteLine("Sort by studak: S");
            Console.WriteLine("Exit: Esc");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.E:
                    Key.Edit(a);
                    break;

                case ConsoleKey.N:
                    a[0].First(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.D:
                    a[0].Second(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.S:
                    a[0].Studak(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.Enter:
                    Key.Show(a);
                    break;

                case ConsoleKey.A:
                    Key.Add(a, c);
                    break;

                case ConsoleKey.R:
                    Key.Remove(a, delete);
                    break;

                case ConsoleKey.Escape:
                    break;
            }

        }
        public static void Show(Users[] a)
        {
            Console.WriteLine("{0,-15} {1, -15}\t {2, -10} {3, -20} {4,-20} {5,-20}{6,-20}", "Прiзвище", "Група", "Курс", "Логiн", "Пароль", "Номер", "Дата");

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);
                }
            }
            Key.KKEY();
        }
        public static void Add(Users[] a, int c)
        {
            Console.WriteLine("\nWrite data:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Key.Parse(elements, true, a, c);
            Key.KKEY();
        }

        private static void Save(Users m)
        {
            StreamWriter save = new StreamWriter("person.txt", true);

            save.WriteLine(m.Surname);
            save.WriteLine(m.Group);
            save.WriteLine(m.Course);
            save.WriteLine(m.Login);
            save.WriteLine(m.Password);
            save.WriteLine(m.Number);
            save.WriteLine(m.Data);
            save.Close();
        }

        public static void Parse(string[] elements, bool save, Users[] a, int counter)
        {


            for (int i = 0; i < elements.Length; i += 7)
            {
                a[counter + i / 5] = new Users(elements[i], elements[i + 1], int.Parse(elements[i + 2]), elements[i + 3], elements[i + 4], int.Parse(elements[i + 5]), DateTime.Parse(elements[i + 6]));
                if (save)
                {
                    Save(a[counter + i / 5]);
                }
            }

        }
        public static void Remove(Users[] a, bool[] delete)
        {
            Console.Write("\nSurname: ");

            string name = Console.ReadLine();

            bool[] write = new bool[a.Length];

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    if (a[i].Surname == name)
                    {
                        Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);

                        Console.WriteLine("\nDELETE? (Y/N)\n");

                        var key = Console.ReadKey().Key;

                        if (key == ConsoleKey.Y)
                        {

                            a[i] = null;
                            delete[i] = true;
                            Key.Show(a);

                        }
                        else
                        {
                            delete[i] = false;
                        }
                    }
                }
            }
            Key.KKEY();
        }
        public static void Edit(Users[] a)
        {

            Console.WriteLine("\nWhat do you want to edit?(Surname, Group, Course, Login, Password, Number)");
            string what = Console.ReadLine();
            switch (what)
            {
                case "Surname":
                    Console.WriteLine("What surname: ");
                    string name1 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Surname == name1)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);


                                Console.WriteLine("New surname: ");

                                string str = Console.ReadLine();

                                a[i].Surname = str;

                                Key.Show(a);
                            }
                        }

                    }
                    break;

                case "Group":
                    Console.WriteLine("What group: ");
                    string name2 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Group == name2)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);


                                Console.WriteLine("New group: ");

                                string str = Console.ReadLine();

                                a[i].Group = str;

                                Key.Show(a);
                            }
                        }

                    }
                    break;
                case "Login":
                    Console.WriteLine("What login: ");
                    string name3 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Login == name3)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);


                                Console.WriteLine("New login: ");

                                string str = Console.ReadLine();

                                a[i].Login = str;

                                Key.Show(a);
                            }
                        }

                    }
                    break;
                case "Password":
                    Console.WriteLine("What password: ");
                    string name4 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Password == name4)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);


                                Console.WriteLine("New password: ");

                                string str = Console.ReadLine();

                                a[i].Password = str;

                                Key.Show(a);
                            }
                        }

                    }
                    break;
                case "Number":
                    Console.WriteLine("What login: ");
                    int name5 = int.Parse(Console.ReadLine());
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Number == name5)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);


                                Console.WriteLine("New login: ");

                                int str = int.Parse(Console.ReadLine());

                                a[i].Number = str;

                                Key.Show(a);
                            }
                        }

                    }
                    break;
                case "Course":
                    Console.WriteLine("What login: ");
                    int name6 = int.Parse(Console.ReadLine());
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Course == name6)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].Surname, a[i].Group, a[i].Course, a[i].Login, a[i].Password, a[i].Number, a[i].Data);


                                Console.WriteLine("New login: ");

                                int str = int.Parse(Console.ReadLine());

                                a[i].Course = str;

                                Key.Show(a);
                            }
                        }

                    }
                    break;
            }
            Key.KKEY();
        }
        
    }
} 