using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class singleTimerManager : MonoBehaviour
{
    static singleTimerManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }

        else { 
            
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
