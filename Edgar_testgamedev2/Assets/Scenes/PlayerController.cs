using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TiltedTopDownController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    private bool isRunning = false;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 inputDirection;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator animator;
    

    void Start()
    {

        if (mainCamera == null)
            mainCamera = Camera.main;
        
        rb = GetComponent<Rigidbody>();

        animator = GetComponentInChildren<Animator>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 direction = point - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        
        isRunning = Input.GetKey(KeyCode.LeftShift);

        float inputMagnitude = inputDirection.magnitude;
        animator.SetFloat("Speed", inputMagnitude);
        animator.SetBool("isRunning", isRunning && inputMagnitude > 0.1f);



        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void FixedUpdate()
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
        

    }
}
