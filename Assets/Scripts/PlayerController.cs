using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 1.0f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CameraController mainCamController;
    public bool movable = true;
    private DialogueTrigger currentDialogueTrigger;
    private float horizontal;
    private float vertical;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void  FixedUpdate()
    {
        if (movable)
        {
            rb.linearVelocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
            mainCamController.moveCamera(horizontal, vertical);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            mainCamController.moveCamera(0f, 0f);
        }
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void NPCInteract(InputAction.CallbackContext context)
    {
        Debug.Log("pressed");
        if(context.phase == InputActionPhase.Started && currentDialogueTrigger != null)
        {
            currentDialogueTrigger.NPCInteract();
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("collisoin");
        if (collision.gameObject.tag.Equals("DialogueTrigger"))
        {
            currentDialogueTrigger = collision.gameObject.GetComponent<DialogueTrigger>();
            Debug.Log("added");
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("left");
        currentDialogueTrigger = null;
    }
}
