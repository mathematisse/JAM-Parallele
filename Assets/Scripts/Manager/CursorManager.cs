using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Camera tcamera;
    public ISelectable selected;
    public Vector2 mousePosition;
    public bool isUpgrading = false;
    public GameRuntimeUI gameRuntime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = tcamera.ScreenToWorldPoint(Input.mousePosition);
        if (isUpgrading)
        {
            gameRuntime.tooltipElement.transform.position = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        }
        else
        {
            gameRuntime.tooltipElement.transform.position = new Vector2(-1000, -1000);
        }
    }

    public void ShowToolTip(string text)
    {
        gameRuntime.tooltipElement.visible = true;
        gameRuntime.tooltipLabel.text = text;
    }

    public void HideToolTip()
    {
        gameRuntime.tooltipLabel.text = "";
        gameRuntime.tooltipElement.visible = false;
    }

    public void SetToolTipColor(bool canAfford)
    {
        if (canAfford)
        {
            gameRuntime.tooltipLabel.style.color = Color.green;
        }
        else
        {
            gameRuntime.tooltipLabel.style.color = Color.red;
        }
    }
}
