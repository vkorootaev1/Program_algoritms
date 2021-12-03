using System;
using System.Diagnostics;

namespace ConsoleApp12
{

    class Program
    {
        static int[,] Plus(int[,] arr1, int[,] arr2, int n) // Фунция сложения 
        {
            int[,] temp = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] = arr1[i, j] + arr2[i, j];
                }
            }
            return temp;
        }
        static int[,] Minus(int[,] arr1, int[,] arr2, int n) // Функция вычитания
        {
            int[,] temp = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] = arr1[i, j] - arr2[i, j];
                }
            }
            return temp;
        }
        static int[,] MultiPly(int[,] arr1, int[,] arr2) // стандартный метод перемножения матриц 
        {
            int[,] arr3 = new int[arr1.GetLength(0), arr2.GetLength(1)];
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 0; j < arr2.GetLength(1); j++)
                {
                    for (int k = 0; k < arr2.GetLength(0); k++)
                    {
                        arr3[i, j] += arr1[i, k] * arr2[k, j];
                    }
                }
            }
            return arr3;
        }
        static int[,] Str(int[,] arr1, int[,] arr2, int n) // Метод Штрассена
        {
            if (n <= 64)        // Если размерность матриц меньше или равна 64, то перемножаем стандартным методом
            {
                return MultiPly(arr1, arr2);
            }
            int[,] arr3 = new int[n, n];
            int k = n / 2;
            int[,] a11 = new int[k, k];
            int[,] a12 = new int[k, k];
            int[,] a21 = new int[k, k];
            int[,] a22 = new int[k, k];
            int[,] b11 = new int[k, k];
            int[,] b12 = new int[k, k];
            int[,] b21 = new int[k, k];
            int[,] b22 = new int[k, k];
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    a11[i, j] = arr1[i, j];
                    a12[i, j] = arr1[i, k + j];
                    a21[i, j] = arr1[k + i, j];
                    a22[i, j] = arr1[k + i, k + j];
                    b11[i, j] = arr2[i, j];
                    b12[i, j] = arr2[i, k + j];
                    b21[i, j] = arr2[k + i, j];
                    b22[i, j] = arr2[k + i, k + j];
                }
            }
            int[,] p1 = Str(a11, Minus(b12, b22, k), k);
            int[,] p2 = Str(Plus(a11, a12, k), b22, k);
            int[,] p3 = Str(Plus(a21, a22, k), b11, k);
            int[,] p4 = Str(a22, Minus(b21, b11, k), k);
            int[,] p5 = Str(Plus(a11, a22, k), Plus(b11, b22, k), k);
            int[,] p6 = Str(Minus(a12, a22, k), Plus(b21, b22, k), k);
            int[,] p7 = Str(Minus(a11, a21, k), Plus(b11, b12, k), k);

            int[,] c11 = Minus(Plus(Plus(p5, p4, k), p6, k), p2, k);
            int[,] c12 = Plus(p1, p2, k);
            int[,] c21 = Plus(p3, p4, k);
            int[,] c22 = Minus(Minus(Plus(p5, p1, k), p3, k), p7, k);
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    arr3[i, j] = c11[i, j];
                    arr3[i, j + k] = c12[i, j];
                    arr3[k + i, j] = c21[i, j];
                    arr3[k + i, k + j] = c22[i, j];
                }
            }
            return arr3;
        }
        static void Print(int[,] a) // Функция вывода массива на экран
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write("{0} ", a[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void Filling(int[,] arr) // Заполнение матриц случайными числами
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Random rnd = new Random();
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = rnd.Next(-10, 10);
                }
            }
        }
        public static bool Power2(int a) // Функция проверки на то,  является ли размерность степенью 2
        {
            if (a == 2) return true;
            else if (a % 2 == 0) return Power2(a / 2);
            else return false;
        }
        public static int[,] NewRang(int[,] arr, int n) // Заполняет матрицу 0 , там где это необходимо (после преобразования размера матрицы для метода Штрассена)
        {
            if (Power2(arr.GetLength(0)))
            {
                return arr;
            }
            else
            {
                int n1 = arr.GetLength(0);
                int[,] arr2 = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        arr2[i, j] = 0;
                    }
                }
                for (int i = 0; i < n1; i++)
                {
                    for (int j = 0; j < n1; j++)
                    {
                        arr2[i, j] = arr[i, j];
                    }
                }
                return arr2;
            }
        }
        public static int[,] Obr(int[,] arr, int n) // Функция , которая копирует результирующую матрицу после метода Штрассена таким образом, чтобы на выходе была размерность матрицы введенная вначале.
        {
            int[,] mas = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mas[i, j] = arr[i, j];
                }
            }
            return mas;
        }
        public static int NewDem(int n) // Ищет новую размерность матрицы для использования метода Штрассена(ищет ближающую степень 2 с округлением в большую сторону)
        {
            if (!(Power2(n)))
                return 1 << (int)Math.Log2(n) + 1;
            else return n;
            }
        public static long Summa(int [,]arr) // Функция для подсчета суммы элементов.Нужна для того,чтобы сверить правильно ли работает метод Штрассена по сравнению со стандартным методом.
        {
            long summa = 0;
            int n = arr.GetLength(0);
            for(int i =0; i < n;i++)
            {
                for(int j=0;j < n;j++)
                {
                    summa +=arr[i, j];
                }
            }
            return summa;
        }
        static public void Hand_Fill(ref int[,] arr) // Функция для ввода элементов матрицы с клавиатуры
        {
            int n = arr.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine("Введите arr[" + i + "," + j + "]");
                    arr[i, j] = int.Parse(Console.ReadLine());
                }
            }
        }

        static void Main(string[] args) // функция Main 
        {
            Stopwatch st = new Stopwatch(); // Создаем экземляры класса Stopwatch для того,чтобы узнать какое время затратила та или иная функция
            Stopwatch st1 = new Stopwatch();
            Console.WriteLine("Введите размерность матрицы: ");
            int n = int.Parse(Console.ReadLine());
            int[,] arr1 = new int[n, n];
            int[,] arr2 = new int[n, n];
            Console.WriteLine("Нажмите \"1\", если хотите ввести элементы вручную");
            Console.WriteLine("Нажмите \"2\", если хотите ввести элементы с помощью генератора чисел");
            Console.WriteLine("Выберите тип ввода:");
            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    {
                        Console.WriteLine("Вы выбрали ручной ввод!");
                        Console.WriteLine("Введите элементы матрицы A:");
                        Hand_Fill( ref arr1);
                        Console.WriteLine("Введите элементы матрицы B:");
                        Hand_Fill(ref arr2);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Вы выбрали автоматический ввод!");
                        Filling(arr1);
                        Filling(arr2);
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Вы ввели не тот символ");
                        break;
                    }
                    
            }
            Console.WriteLine("Нажмите \"1\", если хотите выводить все матрицы на экран");
            Console.WriteLine("Нажмите \"2\", если не хотите выводить все матрицы на экран");
            Console.WriteLine("Выберите тип вывода матриц на экране:");
            int b = int.Parse(Console.ReadLine());
            bool f=false;
            if(b == 1)
            {
                f = true;
            }
            if (f)
            {
                Console.WriteLine("Матрица A:");
                Print(arr1);
            }
            if (f)
            {
                Console.WriteLine("Матрица B:");
                Print(arr2);
            }
            st.Start(); // точка начала подсчета времени для метода Штрассена
            var newdem = NewDem(n);
            Console.WriteLine("Размерность матриц для метода Штрассена: " + newdem);
            var arr1_1 = NewRang(arr1, newdem);
            var arr2_1 = NewRang(arr2, newdem);
            var arr3 = Str(arr1_1, arr2_1, newdem);
            var mas = Obr(arr3, n);
            st.Stop(); // точка окончания подсчета времени для метода Штрассена
            Console.WriteLine("Время,затраченное на перемножение матриц методом Штрассена: " + (float)st.ElapsedMilliseconds / 1000 + " seconds");
            if (f)
            {
                Console.WriteLine("Результирующая матрица, полученная методом Штрассена: ");
                Print(mas);
            }
            Console.WriteLine("\n");
            var sum1 = Summa(mas);
            Console.WriteLine("Сумма элементов нужна для того, чтобы сверить правильность подсчета матриц методом Штрассена и стандартным!Если число сошлось, то методы считают правильно!");
            Console.WriteLine("Сумма элементов полученная методом Штрассена: " + sum1);
            Console.WriteLine("\n");
            st1.Start(); // точка начала подсчета времени для стандартного метода
            var arr4 = MultiPly(arr1, arr2);
            st1.Stop(); // точка окончания подсчета времени для стандартного метода
            Console.WriteLine("Время,затраченное на перемножение матриц тривиальным методом: " + (float)st1.ElapsedMilliseconds / 1000 + " seconds");
            if (f)
            {
                Console.WriteLine("Результирующая матрица, полученная стандартным методом:");
                Print(arr4);
            }
            var sum2 = Summa(arr4);
            Console.WriteLine("Сумма элементов нужна для того, чтобы сверить правильность подсчета матриц методом Штрассена и стандартным!Если число сошлось, то методы считают правильно!");
            Console.WriteLine("Сумма элементов,полученная стандартным методом: " + sum2);
        }
    }
}
