using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataManager 
{
    public string TryReadData(string key);

    public void WriteData(string key, string value);
}
