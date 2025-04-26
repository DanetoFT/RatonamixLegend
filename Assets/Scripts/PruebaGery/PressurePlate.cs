using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Configuración")]
    public LayerMask mouseLayer;
    public Animator plateAnimator;
    public Animator doorAnimator;
    public float doorDelay;

    private Coroutine doorCourutine;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((mouseLayer.value & (1 << other.gameObject.layer)) != 0)// una movida q hay q hcaer para que detecte la capa del raton y lo chace con los index de los layers
        {

            plateAnimator.SetBool("Pressed", true);
            if (doorCourutine != null)
            {
                StopCoroutine(doorCourutine);
            }
            doorCourutine = StartCoroutine(DelayDoor(true));
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((mouseLayer.value & (1 << other.gameObject.layer)) != 0)
        {

            plateAnimator.SetBool("Pressed", false);
            
        }
        doorCourutine = StartCoroutine(DelayDoor(false));
    }
    IEnumerator DelayDoor(bool pressed)
    {
        yield return new WaitForSeconds(doorDelay);
        doorAnimator.SetBool("Pressed", pressed);
    }
}

