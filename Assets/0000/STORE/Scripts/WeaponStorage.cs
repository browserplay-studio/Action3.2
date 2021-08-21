using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponCollection
{
    public List<string> SelectedIds = new List<string>();
    public List<string> OpenedIds = new List<string>();
}

public class WeaponStorage : MonoBehaviour
{
    public List<WeaponItem> SelectedWeapons = new List<WeaponItem>();

    public Wallet Wallet = new Wallet();

    public List<WeaponItem> Weapons = new List<WeaponItem>();

    private List<WeaponItem> openedWeapons = new List<WeaponItem>();

    private const string saveKey = "WEAPONS";

    private void Start()
    {
        Wallet.Initialize();
    }

    private void Awake()
    {
        Load();
    }

    public bool Buy(WeaponItem weaponItem)
    {
        if (Wallet.Amount >= weaponItem.Price)
        {
            Wallet.Amount -= weaponItem.Price;

            // save
            Wallet.Save();

            openedWeapons.Add(weaponItem);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsOpened(WeaponItem weaponItem)
    {
        return openedWeapons.Contains(weaponItem);
    }

    public bool IsSelected(WeaponItem weaponItem)
    {
        return SelectedWeapons.Contains(weaponItem);
    }

    public void Select(WeaponItem weaponItem)
    {
        // ограничение на 3 выбранные пушки
        //if (SelectedWeapons.Count == 3)
        //{
        //    SelectedWeapons.RemoveAt(0);
        //}

        SelectedWeapons.Add(weaponItem);
    }

    public bool Deselect(WeaponItem weaponItem)
    {
        return SelectedWeapons.Remove(weaponItem);
    }

    public void Save()
    {
        WeaponCollection collection = new WeaponCollection();

        for (int i = 0; i < SelectedWeapons.Count; i++)
        {
            collection.SelectedIds.Add(SelectedWeapons[i].Id);
        }

        for (int i = 0; i < openedWeapons.Count; i++)
        {
            collection.OpenedIds.Add(openedWeapons[i].Id);
        }

        string json = JsonUtility.ToJson(collection);

        PlayerPrefs.SetString(saveKey, json);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);

            WeaponCollection collection = JsonUtility.FromJson<WeaponCollection>(json);

            for (int i = 0; i < collection.SelectedIds.Count; i++)
            {
                WeaponItem weaponItem = Weapons.Find(w => w.Id == collection.SelectedIds[i]);

                if (weaponItem)
                {
                    SelectedWeapons.Add(weaponItem);
                }
            }

            for (int i = 0; i < collection.OpenedIds.Count; i++)
            {
                WeaponItem weaponItem = Weapons.Find(w => w.Id == collection.OpenedIds[i]);

                if (weaponItem)
                {
                    openedWeapons.Add(weaponItem);
                }
            }
        }
        else
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                WeaponItem weaponItem = Weapons[i];

                if (weaponItem.Price == 0)
                {
                    openedWeapons.Add(weaponItem);
                }
            }
        }
    }
}
