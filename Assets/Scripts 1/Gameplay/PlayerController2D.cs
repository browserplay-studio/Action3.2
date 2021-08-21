
using System;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public event Action OnCollisionStart = null;
    public event Action OnCollisionFinish = null;

    
   

    

   

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "[Start]")
        {
            OnCollisionStart?.Invoke();
        }
        else if (collision.name == "[Finish]")
        {
            OnCollisionFinish?.Invoke();
        }
    }
}


   
