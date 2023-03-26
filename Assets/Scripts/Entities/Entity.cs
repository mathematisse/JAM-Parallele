using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum WalkDirection { Stop, Left, Right }

    public bool OnUpsideDown;
    public WalkDirection Direction;
    public WalkDirection LastDirection;

    public float WalkSpeed = 0.1f;

    private bool IsSpriteLookingLeft = true;
    protected Animator animator;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        if (Direction == WalkDirection.Right) WalkRight();
        if (Direction == WalkDirection.Stop) WalkStop();
    }

    protected void FixedUpdate()
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

    public void HideSprite()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    public void ShowSprite()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
    
    public void PassPortal()
    {
        OnUpsideDown = !OnUpsideDown;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y * -1, transform.localScale.z);
    }
    public void WalkLeft()
    {
        if (!IsSpriteLookingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            IsSpriteLookingLeft = true;
        }
        LastDirection = WalkDirection.Right;
        Direction = WalkDirection.Left;
        animator.SetBool("Walking", true);
    }

    public void WalkStop()
    {
        if (Direction == WalkDirection.Stop) return;
        LastDirection = Direction;
        Direction = WalkDirection.Stop;
        animator.SetBool("Walking", false);
    }
    public void WalkRight()
    {
        if (IsSpriteLookingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            IsSpriteLookingLeft = false;
        }
        LastDirection = WalkDirection.Left;
        Direction = WalkDirection.Right;
        animator.SetBool("Walking", true);
    }

    public void WalkReverse()
    {
        if (LastDirection == WalkDirection.Left)
        {
            WalkLeft();
        }
        if (LastDirection == WalkDirection.Right)
        {
            WalkRight();
        }
    }
    public void WalkTo(GameObject target)
    {
        if (transform.position.x > target.transform.position.x)
        {
            WalkLeft();
        }
        else
        {
            WalkRight();
        }
    }
}
