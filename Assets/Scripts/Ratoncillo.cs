using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using NavMeshPlus.Extensions;

public class Ratoncillo : MonoBehaviour
{
    public float tiempoCaida;

    public Transform target;

    private Animator animator;

    private Transform mouseTransform;

    NavMeshAgent agent;

    private Rigidbody2D rb;

    private Vector2 targetDirection;

    [SerializeField] GameObject ratoncet;

    public float rotationSpeed;
    public bool mouseMove;
    public bool canRotate;
    public float speed;

    public Vector2 directionToCheese;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        canRotate = false;
        mouseMove = false;
        canRotate = true;
        rb = GetComponent<Rigidbody2D>();
        //mouseTransform = ratoncet.transform;
    }

    private void FixedUpdate()
    {
        if (canRotate)
        {
            UpdateTargetRotation();
            RotateTowardsTarget();
        }

        if (mouseMove)
        {
            Movement();
        }
    }

    void Movement()
    {
        Vector2 ratonToCheeseVector = target.position - transform.position;
        directionToCheese = ratonToCheeseVector.normalized;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void UpdateTargetRotation()
    {
        targetDirection = directionToCheese;
    }

    void RotateTowardsTarget()
    {
        if(targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.SetRotation(rotation);
    }

    /*void Rotation()
    {


        Vector3 rotate = (agent.steeringTarget - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, rotate);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rotation);

        float angle = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
        mouseTransform.eulerAngles = new Vector3(0, 0, angle -90);
    }*/

    void CancelMove()
    {
        mouseMove = false;
        canRotate = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Fall")
        {
            animator.SetBool("Falling", true);
            Invoke("CancelMove", tiempoCaida);
        }
    }
}
