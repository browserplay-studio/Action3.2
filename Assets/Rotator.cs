using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Sprite[] Sprites = null;
    public SpriteRenderer Rend = null;

    public Transform pointer = null;

    public int spriteIndex = 0;

    public float angle = 0;

    private void Update()
    {
        Vector2 dir = pointer.position - transform.position;

        angle = -Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

        if (angle < 0) angle += 360;

        spriteIndex = Mathf.RoundToInt(angle / 45) % 8;

        Rend.sprite = Sprites[spriteIndex];
    }
}
