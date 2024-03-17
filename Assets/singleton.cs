using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                /*if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;
                    instance = go.AddComponent<T>();
                }*/
            }
            return instance;
        }
    }
}