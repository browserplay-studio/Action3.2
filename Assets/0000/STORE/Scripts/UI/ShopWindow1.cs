using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindow1 : MonoBehaviour
{
    [SerializeField] private WeaponElement elementPrefab = null;
    [SerializeField] private Transform elementsParent = null;

    private WeaponElement[] elements = null;

    private WeaponStorage storage = null;

    private void Awake()
    {
        storage = FindObjectOfType<WeaponStorage>();
    }

    private void Start()
    {
        RemoveChildrens(elementsParent);
        CreateElements();
        UpdateElements();
    }

    private void CreateElements()
    {
        elements = new WeaponElement[storage.Weapons.Count];

        for (int i = 0; i < elements.Length; i++)
        {
            WeaponElement e = Instantiate(elementPrefab, elementsParent);

            int k = i;
            e.OnClick = () => OnClick(k);

            elements[i] = e;
        }
    }

    private void UpdateElements()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            WeaponElement e = elements[i];

            WeaponItem w = storage.Weapons[i];
            bool isSelected = storage.IsSelected(w);
            bool isOpened = storage.IsOpened(w);
            e.UpdateVisual(w, isSelected, isOpened);
        }
    }

    private void OnClick(int index)
    {
        WeaponItem w = storage.Weapons[index];

        if (storage.IsSelected(w))
        {
            storage.Deselect(w);
            
            // save
            storage.Save();

            UpdateElements();
            return;
        }

        if (storage.IsOpened(w))
        {
            if (!storage.IsSelected(w))
            {
                storage.Select(w);

                // save
                storage.Save();
            }
        }
        else
        {
            bool bougth = storage.Buy(w);

            if (bougth)
            {
                storage.Select(w);

                // save
                storage.Save();
            }
        }

        UpdateElements();
    }

    private void RemoveChildrens(Transform parent)
    {
        Transform[] childrens = new Transform[parent.childCount];

        for (int i = 0; i < childrens.Length; i++)
        {
            childrens[i] = parent.GetChild(i);
        }

        for (int i = 0; i < childrens.Length; i++)
        {
            Destroy(childrens[i].gameObject);
        }
    }
}
