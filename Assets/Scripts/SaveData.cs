using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string ToJson()
    {
        return JsonUtility.ToJson(this, true);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    public static void Load(ISavable savable, string path)
    {
        bool success = true;
        string filePath = Path.Combine(Application.persistentDataPath, path);
        if (File.Exists(filePath))
        {
            Type type = savable.GetSaveDataType();
            var data = Activator.CreateInstance(type);
            (data as SaveData).LoadFromJson(File.ReadAllText(filePath));

            foreach (var field in type.GetFields())
            {
                var value = field.GetValue(data);
                if (value == null)
                {
                    Debug.LogWarning($"{field.Name} failed to load from path: {path}");
                    success = false;
                }
                var targetField = savable.GetType().GetField(field.Name);
                try
                {
                    targetField.SetValue(savable, value);
                }
                catch (ArgumentException)
                {
                    Debug.LogError($"Field {field.Name} type does not match! Save path: {path}");
                }
            }
        }
        if (!success) return;
        savable.OnFinishLoad();
    }

    public static void Save(ISavable savable, string path)
    {
        Type type = savable.GetSaveDataType();
        var data = Activator.CreateInstance(type);
        foreach (var field in type.GetFields())
        {
            var targetField = savable.GetType().GetField(field.Name);
            if(targetField == null)
            {
                Debug.LogError($"Field '{field.Name}' does not exist on type {savable.GetType()}");
            }
            field.SetValue(data, targetField.GetValue(savable));
        }
        File.WriteAllText(Path.Combine(Application.persistentDataPath, path), (data as SaveData).ToJson());
    }
}

public interface ISavable
{
    /// <summary>
    /// Gets the type of the SaveData class that describes the data that will be saved to file. 
    /// The fields in the data class should have the same name as the fields in the implementing class
    /// </summary>
    /// <returns></returns>
    Type GetSaveDataType();
    /// <summary>
    /// Called when the ISavable is done loading
    /// </summary>
    void OnFinishLoad();
}
