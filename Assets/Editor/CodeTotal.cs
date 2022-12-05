using UnityEngine;
using UnityEditor;
using System.IO;

public class CodeTotal
{
    static int code_num = 0;
    static int file_num = 0;

    static string all_path = "Assets";
    [MenuItem("Tool/CodeTotal/CS")]
    static void TotalCodeCs()
    {
        code_num = 0;
        file_num = 0;
        TotalCs(all_path);
        Debug.LogError("Code CS Line Num = " + code_num + "\nCS File Num = " + file_num);
    }

    /// <summary>
    /// 获取指定文件夹的cs文件
    /// </summary>
    /// <param name="path">文件夹路径</param>
    static void TotalCs(string path)
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*.cs");


            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                string name = files[i].Name.Split('.')[0];
                code_num += ReadFile(path + "\\" + files[i].Name);
                file_num += 1;

            }
            foreach (DirectoryInfo NextFolder in direction.GetDirectories())
            {
                TotalCs(path+ "\\"+NextFolder.Name);
            }
        }
        else
        {
            Debug.LogError("No " + path + " Error!");
        }
    }

    /// <summary>
    /// 获取文件的行数
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    static int ReadFile(string path)
    {
        StreamReader sr = new StreamReader(path);
        int num = 0;
        while (!sr.EndOfStream)
        {
            sr.ReadLine();
            num++;
        }
        sr.Close();
        return num;
    }
}

