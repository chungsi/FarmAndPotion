using System;
using UnityEngine;
using Yarn.Unity;

public class CustomVariableStorage : VariableStorageBehaviour
{
  SaveData sd;

  void Start() {
    // get data from file or db
    // check that it exists?
    sd = new SaveData();

    if (FileManager.LoadFromFile("TestSaveData.dat", out var json))
    {
      sd.LoadFromJson(json);
      Debug.Log("[Custom Variable Storage] Loaded save file");
    }
    else if (FileManager.WriteToFile("TestSaveData.dat", sd.ToJson()))
    {
      Debug.Log("[Custom Variable Storage] Created new save file");
    }
  }

  void OnDestroy() {
    FileManager.WriteToFile("TestSaveData.dat", sd.ToJson());
  }

  public override bool TryGetValue<T>(string varName, out T result)
  {
    Debug.Log($"[Custom Variable Storage] trying to get value {varName}...");
    // var vars = "";
    if (sd.yarnVars.TryGetValue(varName, out SaveData.YarnPrimitives vars))
    {
      if (typeof(T) == typeof(string)) {
        result = (T)Convert.ChangeType(vars.strVal, typeof(T));
        return true;
      }
      else if (typeof(T) == typeof(float)) {
        result = (T)Convert.ChangeType(vars.floatVal, typeof(T));
        return true;
      }
      else if (typeof(T) == typeof(bool)) {
        result = (T)Convert.ChangeType(vars.boolVal, typeof(T));
        return true;
      }
    }

    result = default(T);
    return false;
  }
  public override void SetValue(string varName, string stringValue)
  {
    if (!Contains(varName))
    {
      Debug.Log($"Setting bool save data {varName} to {stringValue}");
      sd.yarnVars.Add(varName, new SaveData.YarnPrimitives { strVal=stringValue });
    }
  }
  public override void SetValue(string varName, float floatValue)
  {
    if (!Contains(varName))
    {
      Debug.Log($"Setting bool save data {varName} to {floatValue}");
      sd.yarnVars.Add(varName, new SaveData.YarnPrimitives { floatVal=floatValue });
    }
  }
  public override void SetValue(string varName, bool boolValue)
  {
    if (!Contains(varName))
    {
      Debug.Log($"Setting bool save data {varName} to {boolValue}");
      sd.yarnVars.Add(varName, new SaveData.YarnPrimitives { boolVal=boolValue });
    }
  }
  public override void Clear()
  {
    sd.LoadFromJson(""); // pass an empty string to overwrite?
  }
  public override bool Contains(string varName)
  {
    Debug.Log($"Checking if variable {varName} already exists in our save data...");
    return sd.yarnVars.TryGetValue(varName, out SaveData.YarnPrimitives vars);
  }
}
