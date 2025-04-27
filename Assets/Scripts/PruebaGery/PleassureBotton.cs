using UnityEngine;
using System.Collections;

public class PleassureBotton : MonoBehaviour
{
    [Header("Configuración Animación")]
    public LayerMask mouseLayer;
    public Animator plateAnimator;
    public Animator doorAnimator;
    private Animator ButtonAnimator;
    public float doorDelay;

    [Header("Objeto para click")]
    public LayerMask clickableLayer; 
    public string clickableTag = "Clickable"; 
    

    private bool isMouseOnPlate = false;
    private bool isPermanentlyOpen = false;
    private Coroutine doorCoroutine;
   

    private void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {          
         CheckClickableObject(true);           
           
        }
        else if (Input.GetMouseButtonUp(0))
        {
            CheckClickableObject(false);
        }
    }

    private void CheckClickableObject(bool mouseDown)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero, Mathf.Infinity, clickableLayer);

        if (hit.collider != null && hit.collider.CompareTag(clickableTag))
        {
            ButtonAnimator = hit.collider.GetComponent<Animator>();

           
            if (ButtonAnimator != null)
            {
                ButtonAnimator.SetBool("Pressed", mouseDown);
            }

            
            if (mouseDown && isMouseOnPlate && !isPermanentlyOpen)
            {
                if (doorCoroutine != null)
                    StopCoroutine(doorCoroutine);

                doorCoroutine = StartCoroutine(OpenDoor());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((mouseLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            isMouseOnPlate = true;
            plateAnimator.SetBool("Pressed", true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((mouseLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            isMouseOnPlate = false;
            plateAnimator.SetBool("Pressed", false);            
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(doorDelay);
        doorAnimator.SetBool("Pressed",true);
        isPermanentlyOpen = true;
    }
}