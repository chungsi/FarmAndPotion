using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class FileManager
{
  public static bool WriteToFile(string _fileName, string _fileContents)
  {
    var fullPath = Path.Combine(Application.persistentDataPath, _fileName);

    try
    {
      File.WriteAllText(fullPath, _fileContents);
      Debug.Log($"Writing to a save file at {fullPath}");
      return true;
    }
    catch (Exception e)
    {
      Debug.LogError($"Failed to write to {fullPath} with exception {e}");
      return false;
    }
  }

  public static bool LoadFromFile(string _fileName, out string result)
  {
    var fullPath = Path.Combine(Application.persistentDataPath, _fileName);

    try
    {
      result = File.ReadAllText(fullPath);
      Debug.Log($"Loaded a save file from {fullPath}");
      return true;
    }
    catch (Exception e)
    {
      Debug.LogError($"Failed to read from {fullPath} with exception {e}");
      result = "";
      return false;
    }
  }
}