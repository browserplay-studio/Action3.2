using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponElement : MonoBehaviour
{
    public Action OnClick = null;

    [SerializeField] private Text weaponLabel = null;
    [SerializeField] private Image weaponImage = null;
    [SerializeField] private Text priceLabel = null;
    [SerializeField] private Image buttonImage = null;
    [SerializeField] private Button button = null;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void UpdateVisual(WeaponItem weaponItem, bool isSelected, bool isOpened)
    {
        weaponLabel.text = weaponItem.Name;
        weaponImage.sprite = weaponItem.Sprite;

        priceLabel.text = isOpened ? (isSelected ? "Выбран" : "Отложен") : $"Цена: {weaponItem.Price}";
        buttonImage.color = isOpened ? (isSelected ? Color.green : Color.blue) : Color.red;
    }

    private void OnButtonClick()
    {
        OnClick();
    }
}
