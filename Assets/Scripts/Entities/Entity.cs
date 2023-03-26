using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum WalkDirection { Stop, Left, Right }

    public GameObject PrefabSprite;
    public Vector3 PrefabOffset;

    public bool OnUpsideDown;
    public WalkDirection Direction;

    public float WalkSpeed = 0.1f;

    private GameObject ResultPrefab;
    private bool IsSpriteLookingLeft = true;

    void Start()
    {
        ReplaceSprite();
    }

    void FixedUpdate()
    {
        WalkUpdate();
    }

    void WalkUpdate()
    {
        int right = 0;
        if (Direction == WalkDirection.Right && !OnUpsideDown)
        {
            right = 1;
        }
        if (Direction == WalkDirection.Right && OnUpsideDown)
        {
            right = -1;
        }
        if (Direction == WalkDirection.Left && !OnUpsideDown)
        {
            right = -1;
        }
        if (Direction == WalkDirection.Left && OnUpsideDown)
        {
            right = 1;
        }

        transform.position += WalkSpeed * right * Vector3.right;
    }

    void ReplaceSprite()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        ResultPrefab = Instantiate(PrefabSprite, transform.position + PrefabOffset, transform.rotation);
        ResultPrefab.transform.parent = transform;
    }

    public void PassPortal()
    {
        OnUpsideDown = !OnUpsideDown;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y * -1, transform.localScale.z);
    }
    public void WalkLeft()
    {
        if (!IsSpriteLookingLeft)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        Direction = WalkDirection.Left;
    }

    public void WalkStop()
    {
        Direction = WalkDirection.Stop;
    }
    public void WalkRight()
    {
        if (IsSpriteLookingLeft)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        Direction = WalkDirection.Right;
    }
}
