using System;
using System.Text.RegularExpressions;
using System.Linq;
using DemoLibrary;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

namespace ConsoleUI
{
    

    class MainClass
    {
        public static void Main(string[] args)
        {
            // dictionary
            MyDictionary dict = new MyDictionary();

            dict.Put("Monday", 1);
            dict.Put("Tuesday", 2);
            dict.Put("Wednesday", 3);
            dict.Put("Thursday", 4);
            dict.Put("Friday", 5);
            dict.Put("Saturday", 6);
            dict.Put("Sunday", 7);

            string target = "Tuesday";
            Console.WriteLine($"{target} is " + dict[target]);

            Console.WriteLine("total pairs : " + dict.Length);
            Console.WriteLine("size : " + dict.Memory + " (bytes)");

            dict.TearDown();


            // list
            MyList<int> myList = new MyList<int>();
            myList.Append(1);
            myList.Append(2);
            myList.Append(3);
            myList.Append(4);
            myList.Append(5);
            myList.Pop(3);
            Console.WriteLine(myList[3]);


            // extensions
            foreach (int i in myList.GetArray())
            {
                Console.WriteLine(i);
            }


            // factory
            int a = MyClass.FacrotyMethod<int>();
            Console.WriteLine(a == 0);
        }

    }

    static class Extensions
    {
        public static T[] GetArray<T>(this MyList<T> list)
        {
            return list.GetCopy();
        }
    }
}