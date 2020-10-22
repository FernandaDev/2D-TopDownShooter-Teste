﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    private static T instance;

    private static bool _applicationIsQuitting = false;

    public static T GetInstance()
    {
        if (_applicationIsQuitting) return null;

        if(instance == null)
        {
            instance = FindObjectOfType<T>();

            if(instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                instance = obj.AddComponent<T>();
            }
        }

        return instance;
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this as T)
        {
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }
}
