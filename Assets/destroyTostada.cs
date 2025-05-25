using UnityEngine;

public class destroyTostada : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this);
    }
}
