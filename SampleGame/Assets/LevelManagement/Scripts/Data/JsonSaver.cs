﻿using System.IO;
using UnityEngine;

namespace LevelManagement.Data
{
    public class JsonSaver
    {
        private static readonly string _filename = "saveData1.sav";

        public static string GetSaveFilename()
        {
            return Application.persistentDataPath + "/" + _filename;
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            string saveFilename = GetSaveFilename();

            FileStream fileStream = new FileStream(saveFilename, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        public bool Load(SaveData data)
        {
            string loadFilename = GetSaveFilename();
            if (File.Exists(loadFilename))
            {
                using (StreamReader reader = new StreamReader(loadFilename))
                {
                    string json = reader.ReadToEnd();
                    JsonUtility.FromJsonOverwrite(json, data);
                }
                return true;
            }
            return false;
        }

        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }
    }
}