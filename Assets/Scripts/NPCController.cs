using Unity.VisualScripting;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Camera cam;
    [SerializeField] float slideSpeed = 2;
    [SerializeField] DialogueController dc;

    private string currentText;
    private float slide = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (slide > 0)
        {
            if (transform.position.y > cam.GetComponent<Rigidbody2D>().position.y + 1.5f)
            {
                Debug.Log(transform.position.y - cam.GetComponent<Rigidbody2D>().position.y + 1.5f);
                transform.position = new Vector3(transform.position.x, transform.position.y - slide * Time.deltaTime, 0);
                slide = Vector3.Distance(transform.position, cam.GetComponent<Rigidbody2D>().position);
                //transform.localScale = transform.localScale * scaleSpeed * Time.deltaTime;
                //transform.position = new Vector3(transform.position.x, transform.position.y - (slideSpeed * Time.deltaTime) * 8/ (transform.position.y - cam.GetComponent<Rigidbody2D>().position.y + 1.5f), 0);

            }
            else
            {
                slide = 0;
                dc.startDialogue(currentText, this);
            }
        }
    }

    public void beginSlideWithDialogue(string dialogue)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            transform.position = new Vector3(cam.GetComponent<Rigidbody2D>().position.x, cam.GetComponent<Rigidbody2D>().position.y + 9f, -5);
            currentText = dialogue;
            slide = slideSpeed;
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            this.gameObject.SetActive(true);
        }
    }
}
