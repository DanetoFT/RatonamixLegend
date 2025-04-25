using UnityEngine;

public class DamageColliderFuego : MonoBehaviour
{
    [Tooltip("Referencia al script StoveTrap del objeto padre")]
    public TrampaFuego parentTrap;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (parentTrap != null)
        {
            parentTrap.TriggerEntered(other);
        }
        else
        {
            Debug.LogWarning("[FireTrigger] No se asign� el StoveTrap en el inspector.");
        }
    }
}