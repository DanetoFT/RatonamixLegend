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
    private bool isActivated=false;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((mouseLayer.value & (1 << other.gameObject.layer)) != 0)// una movida q hay q hcaer para que detecte la capa del raton y lo chace con los index de los layers
        {
            isActivated = true;
            plateAnimator.SetBool("Pressed", true);
            if (doorCourutine != null)
            {
                StopCoroutine(doorCourutine);
            }
            doorCourutine = StartCoroutine(DelayDoor(true));
            
        }
    }

     IEnumerator DelayDoor(bool pressed)
    {
        yield return new WaitForSeconds(doorDelay);
        doorAnimator.SetBool("Pressed", pressed);
    }
}

