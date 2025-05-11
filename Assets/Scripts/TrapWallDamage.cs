using UnityEngine;

public class TrapWallDamage : MonoBehaviour
{
    public TrapWall trapWall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        trapWall.CheckKill(other);
    }
}
