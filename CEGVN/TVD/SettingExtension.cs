﻿using Autodesk.Revit.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace CEGVN.TVD
{
    public class SettingExtension
    {
        public static T GetSetting<T>(string filePath)
        {
            T result = default(T);
            try
            {
                bool flag = File.Exists(filePath);
                if (flag)
                {
                    using (StreamReader streamReader = File.OpenText(filePath))
                    {
                        JsonSerializer jsonSerializer = new JsonSerializer();
                        result = (T)((object)jsonSerializer.Deserialize(streamReader, typeof(T)));
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        public static T GetSetting<T>(string folder, string fileName)
        {
            T result = default(T);
            string text = SettingExtension.GetPath() + "\\TVDSetting";
            string text2 = text + "\\" + folder;
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            bool flag2 = !Directory.Exists(text2);
            if (flag2)
            {
                Directory.CreateDirectory(text2);
            }
            string path = text2 + "\\" + fileName;
            bool flag3 = File.Exists(path);
            if (flag3)
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    result = (T)((object)jsonSerializer.Deserialize(streamReader, typeof(T)));
                }
            }
            return result;
        }

        public static void SaveSetting<T>(T setting, string filePath)
        {
            string contents = string.Empty;
            contents = JsonConvert.SerializeObject(setting, Formatting.Indented);
            File.WriteAllText(filePath, contents);
        }

        public static void SaveSetting<T>(T setting, string folder, string fileName)
        {
            string text = SettingExtension.GetPath() + "\\TVDSetting\\" + folder;
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            string path = text + "\\" + fileName;
            string contents = string.Empty;
            contents = JsonConvert.SerializeObject(setting, Formatting.Indented);
            File.WriteAllText(path, contents);
        }

        public static string GetPath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return folderPath + "\\TVD";
        }

        public static string GetSettingPath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return folderPath + "\\TVD\\TVDSetting";
        }

        public static void WriteToTxtFile(string textFile, IList<string> texts)
        {
            string text = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            text = text + "\\" + textFile + ".txt";
            using (StreamWriter streamWriter = new StreamWriter(text, false))
            {
                foreach (string value in texts)
                {
                    streamWriter.WriteLine(value);
                }
            }
        }

        public static void WriteToTxtFile(string path, string textFile, IList<string> texts)
        {
            string path2 = path + "\\" + textFile + ".txt";
            using (StreamWriter streamWriter = new StreamWriter(path2, false))
            {
                foreach (string value in texts)
                {
                    streamWriter.WriteLine(value);
                }
            }
        }

        public static void WriteToTxtFile(string path, string textFile, IList<int> ints)
        {
            string path2 = path + "\\" + textFile + ".txt";
            using (StreamWriter streamWriter = new StreamWriter(path2, false))
            {
                foreach (int num in ints)
                {
                    streamWriter.WriteLine(num.ToString());
                }
            }
        }

        public static void WriteToTxtFile(string textFile, string text)
        {
            string text2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            text2 = text2 + "\\" + textFile + ".txt";
            using (StreamWriter streamWriter = new StreamWriter(text2, true))
            {
                streamWriter.WriteLine(text);
            }
        }

        public static void WriteToTxtFile(string path, string textFile, string text)
        {
            string path2 = path + "\\" + textFile;
            using (StreamWriter streamWriter = new StreamWriter(path2, true))
            {
                streamWriter.WriteLine(text);
            }
        }
    }
}
