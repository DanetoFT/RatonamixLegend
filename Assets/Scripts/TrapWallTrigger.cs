using UnityEngine;

public class TrapWallTrigger : MonoBehaviour
{
    public TrapWall trapWall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & trapWall.playerLayer) != 0)
        {
            trapWall.TriggerFall();
        }
    }
}