using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOrientationManager : MonoBehaviour, IService
{
    public event EventHandler<Orientation> OnOrientationChange;
    private Orientation lastUpdateOrientation = Orientation.None;
    private Orientation currentOrientation;

    void Update()
    {
        CheckOrientation();
    }

    private void CheckOrientation()
    {
        if (Screen.width > Screen.height)
        {
            currentOrientation = Orientation.Horizontal;
        }
        else
        {
            currentOrientation = Orientation.Vertical;
        }
        if (currentOrientation != lastUpdateOrientation)
        {
            OnOrientationChange?.Invoke(this, currentOrientation);
            lastUpdateOrientation = currentOrientation;
        }
    }
}

public enum Orientation
{
    Horizontal,
    Vertical,
    None
}
