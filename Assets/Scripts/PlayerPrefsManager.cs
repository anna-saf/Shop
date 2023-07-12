using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : IDataManager 
{    
    public string TryReadData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string value = PlayerPrefs.GetString(key);
            string decryptionValue = ServiceLocator.Instance.Get<DataAESEncryption>().DecryptString(value);
            return decryptionValue;
        }
        return null;
    }

    public void WriteData(string key, string value)
    {
        string encryptionValue = ServiceLocator.Instance.Get<DataAESEncryption>().EncryptString(value);
        PlayerPrefs.SetString(key, encryptionValue);
        PlayerPrefs.Save();
    }


}
