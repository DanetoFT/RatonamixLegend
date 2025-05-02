using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;


public class DragMouse : MonoBehaviour
{
    public float Force = 500f;
    public LayerMask draggable;
    private Rigidbody2D Rb;
    private CheeseCatcher catcher;
    public Queso queso;
    public Ratoncillo raton;

    

    private void Start()
    {
        catcher = FindAnyObjectByType<CheeseCatcher>();
    }

    private void FixedUpdate()
    {
        if (Rb != null && Rb.bodyType == RigidbodyType2D.Dynamic)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 direction = (mousePos - Rb.transform.position);
            Rb.linearVelocity = direction * Force * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rb = GetRigidbodyFromMouseClick();
            if (Rb != null && catcher != null)
            {
                catcher.StopProcessingQueso(Rb.gameObject);
                raton.canRotate = true;
                raton.target = queso.gameObject.transform;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Rb != null)
            {
                Rb.linearVelocity = Vector2.zero;
                Rb.angularVelocity = 0f;
            }
            Rb = null;
        }
    }

    private Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero, Mathf.Infinity, draggable);
        return hit.collider?.GetComponent<Rigidbody2D>();
    }
}