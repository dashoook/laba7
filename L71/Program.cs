using System;
using System.Collections;
using System.Threading;

namespace L71
{
    class Program
    {
        public static void Main(string[] args)
        {

            Animal a1 = new Animal(355, "Horse", 200, 32, "big");
            Animal a2 = new Animal(35, "Mouse", 10, 42, "very small");
            Animal a3 = new Animal(555, "Cow", 230, 12, "big");
            Animal a4 = new Animal(3, "Hen", 40, 3, "small");
            Animal a5 = new Animal(735, "Lion", 200, 10, "big");
            Animal a6 = new Animal(25, "Cat", 50, 1, "small");
            Animal a7 = new Animal(315, "Giraffe", 206, 24, "big");
            Animal a8 = new Animal(1735, "Elephant", 350, 35, "very big");
            Animal a9 = new Animal(35, "Rat", 200, 2, "very small");
            Animal a10 = new Animal(1035, "Lion", 250, 17, "big");
            Animal[] n = new Animal[10];
            n[0] = a1;
            n[1] = a2;
            n[2] = a3;
            n[3] = a4;
            n[4] = a5;
            n[5] = a6;
            n[6] = a7;
            n[7] = a8;
            n[8] = a9;
            n[9] = a10;
            Array.Sort(n);
            Console.WriteLine("Сортування за вагою:\n");
            Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}","Тварина", "Вага", "Зрiст", "Вiк", "Розмiр");
            for (int i = 0; i<n.Length; i++)
            {
                Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}", n[i].Name, n[i].Weight, n[i].Height, n[i].Age, n[i].Size);
            }
            Console.WriteLine("Сортування за зростом:\n");
            Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}", "Тварина", "Вага", "Зрiст", "Вiк", "Розмiр");
            Array.Sort(n, new Animal.SortByHeight());
            foreach (Animal elem in n) elem.Info1();
              
        }
    }


    public class Animal : IComparable
    {
        public string Name;
        public int Weight;
        public int Age;
        public int Height;
        public string Size;

        public Animal(int W, string N, int H, int A, string S)
        {
            Name = N;
            Weight = W;
            Height = H;
            Age = A;
            Size = S;
        }


        public class SortByHeight : IComparer
        {

            int IComparer.Compare(object ob1, object ob2)
            {
                Animal p1 = (Animal)ob1;
                Animal p2 = (Animal)ob2;
                if (p1.Height > p2.Height) return 1;
                if (p1.Height < p2.Height) return -1;
                return 0;
            }
        }
        public int CompareTo(object obj)
        {
            Animal a = obj as Animal;
            if (a != null)
            {
                if (this.Weight < a.Weight)
                    return -1;
                else if (this.Weight > a.Weight)
                    return 1;
                else
                    return 0;
            }
            else
            {
                throw new Exception("Параметр повинен бути типу Animal!");
            }

        }

        public void Info1()
        {
            Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}", Name, Weight, Height, Age, Size);
        }

        public class Animals : IEnumerable
        {
            public Animal[] an;
            public Animals(Animal[] array)
            {
                an = new Animal[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    an[i] = array[i];
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }
            public ANNN GetEnumerator()
            {
                return new ANNN(an);
            }
        }
        public class ANNN : IEnumerator
        {
            public Animal[] an;
            int pos = -1;
            public ANNN(Animal[] list)
            {
                an = list;
            }
            public bool MoveNext()
            {
                pos++;
                return (pos < an.Length);
            }
            public void Reset()
            {
                pos = -1;
            }
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            public Animal Current
            {
                get
                {
                    try
                    {
                        return an[pos];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}

