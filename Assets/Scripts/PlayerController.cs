using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 1.0f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CameraController mainCamController;

    private float horizontal;
    private float vertical;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void  FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
        mainCamController.moveCamera(horizontal, vertical);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
}
