using UnityEngine;
using System.Collections.Generic;

public class VenomTrap : MonoBehaviour
{
    [Header("Configuración")]
    public float attractionForce = 50f;
    public float stopDistance = 0.1f;
    public LayerMask mouseLayer;

    private bool isMouseAttracted = false;
    private Rigidbody2D mouseRb;
    private Transform mouseTransform;
    private Ratoncillo raton;
    private CheeseCatcher cheeseCatcher;
    private DragMouse dragMouse;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsInMouseLayer(other.gameObject) || isMouseAttracted) return;

        // Asignar referencias
        raton = other.GetComponent<Ratoncillo>();
        cheeseCatcher = other.GetComponent<CheeseCatcher>();
        mouseRb = other.GetComponent<Rigidbody2D>();
        mouseTransform = other.transform;

        // Bloquear ratón
        if (raton != null)
        {
            raton.target = null;
            raton.mouseMove = false;
            raton.canRotate = false;
            raton.isTrapped = true;
        }

        // Detener quesos
        if (cheeseCatcher != null)
        {
            foreach (var queso in new List<GameObject>(cheeseCatcher.activeCoroutines.Keys))
            {
                cheeseCatcher.StopProcessingQueso(queso);
            }
        }

        
        dragMouse = Camera.main.GetComponent<DragMouse>();
        // if (dragMouse != null) dragMouse.enabled = false; /

        //  físicas
        if (mouseRb != null)
        {
            mouseRb.linearVelocity = Vector2.zero;
            mouseRb.isKinematic = false;
        }

        isMouseAttracted = true;

    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        if (!isMouseAttracted || mouseRb == null || mouseTransform == null) return;

        Vector2 direction = (transform.position - mouseTransform.position).normalized;
        float distance = Vector2.Distance(mouseTransform.position, transform.position);

        if (distance > stopDistance)
        {
            mouseRb.linearVelocity = direction * attractionForce * Time.fixedDeltaTime;
        }
        else
        {
            mouseRb.linearVelocity = Vector2.zero;
            mouseRb.isKinematic = true;
        }
    }

    private bool IsInMouseLayer(GameObject obj)
    {
        return (mouseLayer.value & (1 << obj.layer)) != 0;
    }

    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsInMouseLayer(other.gameObject))
        {
            ResetMouseState();
        }
    }

    [System.Obsolete]
    private void OnDestroy()
    {
        ResetMouseState();
    }

    [System.Obsolete]
    private void ResetMouseState()
    {
        isMouseAttracted = false;
        if (mouseRb != null)
        {
            mouseRb.isKinematic = false;
            mouseRb.linearVelocity = Vector2.zero;
        }
        if (raton != null) raton.isTrapped = false;
        if (dragMouse != null) dragMouse.enabled = true;

        mouseRb = null;
        mouseTransform = null;
        raton = null;
    }

    [System.Obsolete]
    public void ReleaseMouse()
    {
        if (mouseRb != null)
        {
            mouseRb.isKinematic = false;
            mouseRb.linearVelocity = Vector2.zero;
        }

        if (raton != null)
        {
            raton.isTrapped = false;
        }

        if (dragMouse != null)
        {
            dragMouse.enabled = true;
        }

        isMouseAttracted = false;
        mouseRb = null;
        mouseTransform = null;
        raton = null;
    }
}
