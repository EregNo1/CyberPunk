using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor_arrowB : MonoBehaviour
{
    [SerializeField] Texture2D cursor_ArrowB;
    [SerializeField] Texture2D cursor_Default;


    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor_ArrowB, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(cursor_Default, Vector2.zero, CursorMode.ForceSoftware);
    }
}
