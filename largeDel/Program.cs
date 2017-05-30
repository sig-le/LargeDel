using System;
using System.Collections.Generic;
using System.IO;

namespace largeDel
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.Write("ディレクトリを指定してください>>>");
            string folderPath = Console.ReadLine();

            if (Directory.Exists(folderPath) != true)
            {
                Console.WriteLine("ディレクトリは存在しませんでした。");

                return;
            }

            if (folderPath.EndsWith("\\") != true || folderPath.EndsWith("/") != true)
            {
                folderPath += "\\";
            }
            //指定ディレクトリ以下のファイルをすべて取得
            IEnumerable<string> files = Directory.EnumerateFiles(folderPath, "*", SearchOption.TopDirectoryOnly);

            //"-large"で終わるファイルから"-large"を削除する
            foreach (string fname in files)
            {
                if (fname.EndsWith("-large") == true)
                {
                    string fileName = Path.GetFileNameWithoutExtension(fname);
                    string extention = Path.GetExtension(fname);
                    string originalExtention = extention.Replace("-large", string.Empty);
                    try
                    {
                        File.Move(fname, folderPath + fileName + originalExtention);
                        Console.WriteLine(fileName + extention + "\t->\t" + fileName + originalExtention);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine(fileName + extention + "\t->\t変換後に同名のファイルがあるためスキップしました");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
            Console.WriteLine("終了しました");
            Console.ReadLine();
        }
    }
}
