using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton<T> : MonoBehaviour 
{
    public static Singelton<T> Instance;
    protected virtual void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }    
}
