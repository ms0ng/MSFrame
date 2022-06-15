using System.Collections;
using System.IO;
using UnityEngine;
using System.Xml.Serialization;

namespace MSFrame.SaveAndLoad
{
    /// <summary>
    /// Save datas as a file.以文件的形式存储数据
    /// </summary>
    public static class SaveAndLoad
    {
        public static void Save<T>(string saveKey, T saveObj)
        {
            if (saveKey.Equals(string.Empty)) return;
            if (saveObj == null) return;
            string filePath = $"{Application.persistentDataPath}/Save/{saveKey}";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            using (var stream = new MemoryStream())
            {
                //BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.UTF8);    //Obsolete
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, saveObj);
                File.WriteAllBytes(filePath, stream.ToArray());
            }
        }

        public static T Load<T>(string saveKey)
        {
            if (saveKey.Equals(string.Empty)) return default;
            T res = default;
            string filePath = $"{Application.persistentDataPath}/Save/{saveKey}";
            if (!Exist(filePath)) return default;
            using (var stream = new MemoryStream(File.ReadAllBytes(filePath)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                res = (T)serializer.Deserialize(stream);
            }
            return res;
        }

        public static bool Exist(string saveKey)
        {
            return File.Exists($"{Application.persistentDataPath}/Save/{saveKey}");
        }

        public static void Delete(string saveKey)
        {
            string filePath = $"{Application.persistentDataPath}/Save/{saveKey}";
            if (File.Exists(filePath))
                File.Delete(filePath);
            else if (Directory.Exists(filePath))
                Directory.Delete(filePath, true);
        }

        public static void DeleteAll()
        {
            string dirPath = $"{Application.persistentDataPath}/Save/";
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(dirPath));
            var files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                file.Delete();
            }
            var dirs = directoryInfo.GetDirectories();
            foreach (var dir in dirs)
            {
                dir.Delete(true);
            }
        }
    }
}