using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Camera tcamera;
    public ISelectable selected;
    public Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = tcamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
