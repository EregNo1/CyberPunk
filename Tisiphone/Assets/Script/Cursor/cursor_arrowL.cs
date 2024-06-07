using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor_arrowL : MonoBehaviour
{
    [SerializeField] Texture2D cursor_ArrowL;
    [SerializeField] Texture2D cursor_Default;


    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor_ArrowL, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(cursor_Default, Vector2.zero, CursorMode.ForceSoftware);
    }
}
