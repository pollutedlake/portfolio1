using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : class
{
    private static readonly object singletonLock = new object();
    // Start is called before the first frame update
    public static Singleton<T> Instance
    {
        get
        {
            if (!instance)
            {
                lock (singletonLock)
                {
                    instance = FindObjectOfType<Singleton<T>>();
                    if (!instance)
                    {
                        GameObject go = new GameObject();
                        instance = go.AddComponent<Singleton<T>>();
                        go.name = "GameManager";
                    }
                }
            }
            return instance;
        }
    }

    private static Singleton<T> instance;
}
