using System.Collections.Generic;
using UnityEngine;

// A class to collect all the save data
[System.Serializable]
public class SaveData
{
  [System.Serializable]
  public struct YarnPrimitives
  {
  //   public struct name
  //   {
      public string strVal;
      public float floatVal;
      public bool boolVal;
  //   }
  }

  // public List<YarnVars> m_YarnVars = new List<YarnVars>();
  public Dictionary<string, YarnPrimitives> yarnVars = new Dictionary<string, YarnPrimitives>();

  public string ToJson()
  {
    return JsonUtility.ToJson(this);
  }

  public void LoadFromJson(string _json)
  {
    JsonUtility.FromJsonOverwrite(_json, this);
  }

  // public struct SomeDataToSave {}
  // A struct for the yarn variables?
}

// Interface for making things saveable
public interface ISaveable
{
  void PopulateSaveData(SaveData _saveData);
  void LoadFromSaveData(SaveData _saveData);
}