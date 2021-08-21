using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private WeaponStorage storage = null;

    private void Awake()
    {
        storage = FindObjectOfType<WeaponStorage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PickableCrystall>(out var pickableCrystall))
        {
            storage.Wallet.Amount += pickableCrystall.Amount;

            // save
            storage.Wallet.Save();

            Destroy(pickableCrystall.gameObject);
        }
    }
}
