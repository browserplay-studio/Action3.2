using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Wallet
{
    public event Action<int> OnAmountChanged = null;

    [SerializeField] private int amount = 0;

    private const string saveKey = "WALLET";

    public int Amount
    {
        get => amount;
        set
        {
            amount = value;
            ForceEvent();
        }
    }

    private void ForceEvent()
    {
        OnAmountChanged?.Invoke(amount);
    }

    public void Initialize()
    {
        Load();

        ForceEvent();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(this);

        PlayerPrefs.SetString(saveKey, json);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);

            Wallet w = JsonUtility.FromJson<Wallet>(json);

            amount = w.Amount;
        }
    }
}
