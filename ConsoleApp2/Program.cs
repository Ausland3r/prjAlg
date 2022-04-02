// ИЛЬЧУК АЛЕКСАНДР

using System;
using prjList;



namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            s = s.Replace(" ", ""); // Удаление пробелов

            MyList list = new MyList(s.Length+1);
            MyList listLvl = new MyList(s.Length+1);

            ExpressionCut(list, listLvl, s);

            Console.WriteLine(Algorytm(list, listLvl));
        }



        public static double Algorytm(MyList list, MyList lvl)
        //Функция рассчётов
        {
            while (list.arrSpace[0].Value == "(")
            //Пока в начале массива стоит скобка, продолжать счёт
            {
                //Найти максимальное значение веса в lvl
                int maxIndex = FirstMax(lvl);

                //Найти индексы всех операторов и операндов, учавствующих в рассчёте выражения внутри скобок
                int next1 = list.arrSpace[maxIndex].Next;
                int next2 = list.arrSpace[next1].Next;
                int next3 = list.arrSpace[next2].Next;
                //Посчитать скобки содержащие первое максимальное значение (от max-1 до max+3)
                double res = 0;
                double b = Convert.ToDouble(list.arrSpace[maxIndex].Value);
                double a = Convert.ToDouble(list.arrSpace[next2].Value);
                string Symbol = list.arrSpace[next1].Value;
                switch (Symbol)
                {
                    case "+":
                        {
                            res = b + a;
                            break;
                        }
                    case "-":
                        {
                            res = b - a;
                            break;
                        }
                    case "*":
                        {
                            res = b * a;
                            break;
                        }
                    case "/":
                        {
                            res = b / a;
                            break;
                        }
                }
                //Заменить max-1 на результат вычисления скобки
                //list.Add(new MyList.SItem(res.ToString(), maxIndex-1));
                //Заменить все элементы от max до max+3 на нулевые и поменять значения Next для элементов списков

                list.arrSpace[maxIndex-1].Next = list.arrSpace[next3].Next;
                lvl.arrSpace[maxIndex-1].Next = lvl.arrSpace[next3].Next;

                for (int i = maxIndex; i <= next3; i++)
                    {
                        list.arrSpace.SetValue(null, i);
                        lvl.arrSpace.SetValue(null, i);
                    }

                list.arrSpace[maxIndex - 1].Value = res.ToString();
            }

            return Convert.ToDouble(list.arrSpace[0].Value);
        }

        public static void ExpressionCut(MyList list, MyList lvl, string Expr)
        //Разбиение строки на списки
        {
            int count = 0;
            int level = 0;
            string Num = "";
            for (int i = 0; i < Expr.Length; i++)
            {
                if ("*/+-)".Contains(Expr[i]))
                {
                    if (Num != "")
                    {
                        level++;
                        list.Add(new MyList.SItem(Num, count + 1));
                        lvl.Add(new MyList.SItem(Convert.ToString(level), count + 1));
                        Num = "";
                        count++;
                    }
                    level--;
                    list.Add(new MyList.SItem(Convert.ToString(Expr[i]), count + 1));
                    lvl.Add(new MyList.SItem(Convert.ToString(level), count + 1));
                    count++;
                }
                else if (Expr[i] != '(')
                {
                    Num += Expr[i];
                }
                else
                {
                    level++;
                    list.Add(new MyList.SItem(Convert.ToString(Expr[i]), count + 1));
                    lvl.Add(new MyList.SItem(Convert.ToString(level), count + 1));
                    count++;
                }
            }
        }





        public static int FirstMax(MyList lvl)
        // Ищет первое максимальное значение веса в списке
        {
            int index = 0;
            int max = 0;
            for (int i = 0; i < lvl.Length(); i++)
            {
                int currValue = Convert.ToInt32(lvl.arrSpace[i].Value);

                if (currValue > max)
                {
                    max = currValue;
                    index = i;
                }
            }

            return index;
        }
    }
}