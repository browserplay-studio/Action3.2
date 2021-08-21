using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Weapon Item]", menuName = "SO/Weapon Item")]
public class WeaponItem : ScriptableObject
{
    public string Id;
    public string Name;
    public int Price;
    public Sprite Sprite;
    public GameObject Prefab;
}
