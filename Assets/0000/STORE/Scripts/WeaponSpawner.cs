using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    private void Start()
    {
        WeaponStorage storage = FindObjectOfType<WeaponStorage>();

        for (int i = 0, length = storage.SelectedWeapons.Count; i < length; i++)
        {
            WeaponItem w = storage.SelectedWeapons[i];

            if (!w.Prefab)
            {
                Debug.LogWarning($"Укажите префаб для оружия - {w.Name}");
                continue;
            }

            GameObject weapon = Instantiate(w.Prefab, transform.position, transform.rotation, transform);
        }
    }
}
