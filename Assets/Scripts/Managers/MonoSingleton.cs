using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T Instance { get; protected set; }

    protected virtual void Awake()
    {
        SetSingletonMono();
    }

    protected abstract void SetSingletonMono();
}

public abstract class SingletonDestroyMono<T> : MonoSingleton<T> where T: MonoBehaviour
{
    protected override void SetSingletonMono()
    {
        Instance = this as T;
    }
}

public abstract class SingletonDontDestroyMono<T> : MonoSingleton<T> where T : MonoBehaviour
{
    protected override void SetSingletonMono()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
