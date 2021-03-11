using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Windows";
            Console.WriteLine("Without Linq:");
            ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("With Linq");
            ShowLargeFilesWithLinq(path);

        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            //var query = from file in new DirectoryInfo(path).GetFiles()
            //            orderby file.Length descending
            //            select file;

            var query = new DirectoryInfo(path).GetFiles()
                        .OrderByDescending(f => f.Length)
                        .Take(5);


            foreach (var file in query)
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
            Console.ReadLine();
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());

            for (int i =0; i<5; i++)
            {
                Console.WriteLine($"{files[i].Name, -20} : {files[i].Length, 10:N0}");
            }

            //foreach (FileInfo file in files)
            //{
            //    Console.WriteLine($"{file.Name} : {file.Length}");

            //}
        
        }
    }
    public class FileInfoComparer : IComparer<FileInfo>
    {
        //returns -1 if 1st param is less than 2nd param
        // 1 if 1st param is greater than 2nd param 
        //0 if they're equal
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
