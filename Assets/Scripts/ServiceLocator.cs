using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ServiceLocator 
{
    private readonly Dictionary<string, IService> serviceCollection = new Dictionary<string, IService>();

    public static ServiceLocator Instance { get; private set; }

    public static void Init()
    {
        if (Instance == null)
        {
            Instance = new ServiceLocator();
        }
    }

    public void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if (serviceCollection.ContainsKey(key))
        {
            return;
        }

        serviceCollection.Add(key, service);
    }

    public void Unregister<T>() where T : IService
    {
        string key = typeof (T).Name;
        if (!serviceCollection.ContainsKey(key))
        {
            return;
        }
        serviceCollection.Remove(key);
    }

    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!serviceCollection.ContainsKey(key))
        {
            throw new InvalidOperationException();
        }

        return (T)serviceCollection[key];
    }
}
