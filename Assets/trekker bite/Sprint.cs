using UnityEngine;

public class Sprint : MonoBehaviour
{
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float normalSpeed = 3f;
    public float sprintSpeed = 6f;
    private Oxygen oxygen;

    [HideInInspector] public bool isSprinting = false;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        oxygen = GetComponent<Oxygen>();

    }

    void Update()
    {
        
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        isSprinting = Input.GetKey(sprintKey) && oxygen != null && oxygen.currentOxygen > 0;
    }

    void FixedUpdate()
    {
        float speed = isSprinting ? sprintSpeed : normalSpeed;
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
