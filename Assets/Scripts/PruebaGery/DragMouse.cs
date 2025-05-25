using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private bool justStartedDragging = false;

    private void Start()
    {
        catcher = FindAnyObjectByType<CheeseCatcher>();
    }

    private void FixedUpdate()
    {
        if (Rb != null && Rb.bodyType == RigidbodyType2D.Dynamic && Input.GetMouseButton(0))
        {
            if (justStartedDragging)
            {
                justStartedDragging = false;
                return;
            }

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
                    Invoke("moverRaton", .5f);
                    raton.target = queso != null ? queso.gameObject.transform : null;
                }

                justStartedDragging = true;
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

    void moverRaton()
    {
        raton.canRotate = true;
        raton.mouseMove = true;
        raton.canMove = true;
    }

    private Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero, Mathf.Infinity, draggable);
        return hit.collider?.GetComponent<Rigidbody2D>();
    }
}