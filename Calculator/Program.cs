using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ideagen Remote Coding Test");
            Console.WriteLine(string.Format("1 + 1 = {0}", Calculate("1 + 1")));
            Console.WriteLine(string.Format("2 * 2 = {0}", Calculate("2 * 2")));
            Console.WriteLine(string.Format("1 + 2 + 3 = {0}", Calculate("1 + 2 + 3")));
            Console.WriteLine(string.Format("6 / 2 = {0}", Calculate("6 / 2")));
            Console.WriteLine(string.Format("11 + 23 = {0}", Calculate("11 + 23")));
            Console.WriteLine(string.Format("11.1 + 23 = {0}", Calculate("11.1 + 23")));
            Console.WriteLine(string.Format("1 + 1 * 3 = {0}", Calculate("1 + 1 * 3")));
            Console.WriteLine(string.Format("( 11.5 + 15.4 ) + 10.1 = {0}", Calculate("( 11.5 + 15.4 ) + 10.1")));
            Console.WriteLine(string.Format("23 - ( 29.3 - 12.5 ) = {0}", Calculate("23 - ( 29.3 - 12.5 )")));
            Console.WriteLine(string.Format("( 1 / 2 ) - 1 + 1 = {0}", Calculate("( 1 / 2 ) - 1 + 1")));
            Console.WriteLine(string.Format("10 - ( 2 + 3 * ( 7 - 5 ) ) = {0}", Calculate("10 - ( 2 + 3 * ( 7 - 5 ) )")));

            Console.ReadLine();
        }

        public static double Calculate(string sum)
        {
            List<string> myList = sum.Split(' ').ToList();

            SolveBracket(myList);

            SolveArithmetic(myList);

            return Convert.ToDouble(myList[0]);
        }

        private static void SolveBracket(List<string> myList)
        {
            List<string> bracketOperator = myList.FindAll(x => x.Equals("("));

            if (bracketOperator.Count >= 1)
            {
                for (int i = 0; i <= bracketOperator.Count; i += 2)
                {
                    int startBracket = myList.FindLastIndex(a => a.Contains("("));
                    int endBracket = myList.FindLastIndex(a => a.Contains(")"));

                    if (i != bracketOperator.Count)
                    {
                        double num1 = Convert.ToDouble(myList[startBracket + 1]);
                        double num2 = Convert.ToDouble(myList[startBracket + 3]);

                        string oper = myList[startBracket + 2];

                        double ansBracket = 0;

                        if (oper == "/")
                            ansBracket = num1 / num2;
                        if (oper == "*")
                            ansBracket = num1 * num2;
                        if (oper == "+")
                            ansBracket = num1 + num2;
                        if (oper == "-")
                            ansBracket = num1 - num2;

                        myList[startBracket + 1] = ansBracket.ToString();
                        myList.RemoveAt(startBracket);
                        myList.RemoveAt(startBracket + 1);
                        myList.RemoveAt(startBracket + 1);
                        myList.RemoveAt(startBracket + 1);
                    }
                    else
                    {
                        List<string> tempList = myList.GetRange(startBracket + 1, endBracket - 3);
                        myList.RemoveRange(startBracket, endBracket - 1);

                        SolveArithmetic(tempList);

                        myList.Add(tempList[0]);
                    }
                }
            }
        }

        private static void SolveArithmetic(List<string> myList)
        {
            int indexDiv = myList.FindIndex(a => a.Contains("/"));
            int indexMulti = myList.FindIndex(a => a.Contains("*"));
            int indexAdd = myList.FindIndex(a => a.Contains("+"));
            int indexMinus = myList.FindIndex(a => a.Contains("-"));

            if (indexDiv < indexMulti)
            {
                SolveDivision(myList);
                SolveMultiplication(myList);
            }
            else
            {
                SolveMultiplication(myList);
                SolveDivision(myList);
            }

            if (indexAdd < indexMinus)
            {
                SolveAddition(myList);
                SolveMinus(myList);
            }
            else
            {
                SolveMinus(myList);
                SolveAddition(myList);
            }
        }

        private static void SolveMultiplication(List<string> myList)
        {
            List<string> mulOperator = myList.FindAll(x => x.Equals("*"));

            foreach (var div in mulOperator)
            {
                int index = myList.FindIndex(a => a.Contains("*"));
                double num1 = Convert.ToDouble(myList[index - 1]);
                double num2 = Convert.ToDouble(myList[index + 1]);

                double ansMul = num1 * num2;

                myList[index - 1] = ansMul.ToString();
                myList.RemoveAt(index);
                myList.RemoveAt(index);
            }
        }

        private static void SolveDivision(List<string> myList)
        {
            List<string> divOperator = myList.FindAll(x => x.Equals("/"));

            foreach (var div in divOperator)
            {
                int index = myList.FindIndex(a => a.Contains("/"));
                double num1 = Convert.ToDouble(myList[index - 1]);
                double num2 = Convert.ToDouble(myList[index + 1]);

                double ansDiv = num1 / num2;

                myList[index - 1] = ansDiv.ToString();
                myList.RemoveAt(index);
                myList.RemoveAt(index);
            }
        }

        private static void SolveMinus(List<string> myList)
        {
            List<string> minOperator = myList.FindAll(x => x.Equals("-"));

            foreach (var div in minOperator)
            {
                int index = myList.FindIndex(a => a.Contains("-"));
                double num1 = Convert.ToDouble(myList[index - 1]);
                double num2 = Convert.ToDouble(myList[index + 1]);

                double ansMin = num1 - num2;

                myList[index - 1] = ansMin.ToString();
                myList.RemoveAt(index);
                myList.RemoveAt(index);
            }
        }

        private static void SolveAddition(List<string> myList)
        {
            List<string> addOperator = myList.FindAll(x => x.Equals("+"));

            foreach (var div in addOperator)
            {
                int index = myList.FindIndex(a => a.Contains("+"));
                double num1 = Convert.ToDouble(myList[index - 1]);
                double num2 = Convert.ToDouble(myList[index + 1]);

                double ansAdd = num1 + num2;

                myList[index - 1] = ansAdd.ToString();
                myList.RemoveAt(index);
                myList.RemoveAt(index);
            }
        }
    }
}
