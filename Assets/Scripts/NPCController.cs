using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Camera cam;
    [SerializeField] float scaleSpeed;
    [SerializeField] DialogueController dc;

    private string currentText;
    private bool grow = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (grow)
        {
            if (transform.localScale.x < 0.5f)
            {
                //transform.localScale = transform.localScale * scaleSpeed * Time.deltaTime;
                transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, transform.localScale.y + Time.deltaTime, 0);
            }
            else
            {
                grow = false;
                dc.startDialogue(currentText, this);
            }
        }
    }

    public void beginGrowWithDialogue(string dialogue)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            transform.position = new Vector3(cam.GetComponent<Rigidbody2D>().position.x, cam.GetComponent<Rigidbody2D>().position.y + 1.5f, -5);
            currentText = dialogue;
            grow = true;
            transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            this.gameObject.SetActive(true);
        }
    }
}
