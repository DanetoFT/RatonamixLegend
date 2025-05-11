using UnityEngine;

public class TrapWall : MonoBehaviour
{
    public float dropDistance = 1f;
    public float dropSpeed = 10f;
    public LayerMask playerLayer;

    private bool isDropping = false;
    private bool hasDropped = false;
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + Vector3.down * dropDistance;
    }

    void Update()
    {
        if (isDropping && !hasDropped)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, dropSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                hasDropped = true;
                Debug.Log("Trampa cayó");
            }
        }
    }

    public void TriggerFall()
    {
        if (hasDropped) return;

        isDropping = true;
        Debug.Log("Trampa activada");
    }

    public void CheckKill(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Debug.Log("Jugador aplastado");
        }
    }
}