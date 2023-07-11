using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : IDataManager 
{    
    public string TryReadData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        return null;
    }

    public void WriteData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }


}
