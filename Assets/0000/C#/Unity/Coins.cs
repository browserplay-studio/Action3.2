
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
 


    void Start()
    {
        ShowRewardedAd();
    }

    public void ShowRewardedAd()
    {
    Application.ExternalCall("ShowRewardedAd");

    }




}









