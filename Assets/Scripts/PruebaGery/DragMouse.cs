using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;

public class DragMouse : MonoBehaviour
{
    public float Force = 500f;
    public LayerMask draggable;
    public Rigidbody2D Rb;
    private CheeseCatcher catcher;
    public Queso queso;
    public Ratoncillo raton;

    public Texture2D mainCursor;
    public Texture2D grabCursor;
    public bool cambioCursor;

    private void Start()
    {
        catcher = FindAnyObjectByType<CheeseCatcher>();
    }

    private void FixedUpdate()
    {
        if (Rb != null && Rb.bodyType == RigidbodyType2D.Dynamic && Input.GetMouseButton(0))
        {
            Vector3 suma = new Vector3(0.3f, -0.3f, 0);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + suma;
            mousePos.z = 0;
            Vector3 direction = (mousePos - Rb.transform.position);
            Rb.linearVelocity = direction * Force * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (raton != null && raton.isTrapped) return;

        if (Input.GetMouseButtonDown(0))
        {
            Rb = GetRigidbodyFromMouseClick();
            if (Rb != null)
            {
                Cursor.SetCursor(grabCursor, Vector2.zero, UnityEngine.CursorMode.Auto);
                cambioCursor = false;

                Rb.bodyType = RigidbodyType2D.Dynamic;

                if (catcher != null)
                {
                    catcher.StopProcessingQueso(Rb.gameObject);
                }

                if (raton != null)
                {
                    raton.canRotate = true;
                    raton.mouseMove = true;
                    raton.target = queso != null ? queso.gameObject.transform : null;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Rb != null)
            {
                Cursor.SetCursor(mainCursor, Vector2.zero, UnityEngine.CursorMode.Auto);
                cambioCursor = true;
                Rb.linearVelocity = Vector2.zero;
                Rb.angularVelocity = 0f;
                Rb.bodyType = RigidbodyType2D.Kinematic;
                Rb = null;
            }
        }
    }

    private Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero, Mathf.Infinity, draggable);
        return hit.collider?.GetComponent<Rigidbody2D>();
    }
}
