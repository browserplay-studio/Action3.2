using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountLabel : MonoBehaviour
{
    [SerializeField] private string formatText = "Кристаллы: {0}";

    private Text label = null;

    private WeaponStorage storage = null;

    private void Awake()
    {
        label = GetComponent<Text>();

        storage = FindObjectOfType<WeaponStorage>();
    }

    private void OnEnable()
    {
        storage.Wallet.OnAmountChanged += OnWalletAmountChanged;
    }

    private void OnDisable()
    {
        storage.Wallet.OnAmountChanged -= OnWalletAmountChanged;
    }

    private void OnWalletAmountChanged(int amount)
    {
        label.text = string.Format(formatText, amount);
    }
}
