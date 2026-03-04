using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialogueBox;
    public float textSpeed = 1f;
    private float textTimer = 0;
    private string currentDialogue;
    private bool textPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startDialogue("testingDialogue");
        Debug.Log("started!!");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDialogue.Length > 0 && !textPaused)
        {
            textTimer += Time.deltaTime;
            if (textTimer > textSpeed)
            {
                dialogueText.text += currentDialogue[0];
                currentDialogue = currentDialogue.Remove(0, 1);
                textTimer = 0;
            }

            if (currentDialogue.Length > 0 && currentDialogue[0] == '@')
            {
                //Debug.Log(currentDialogue[0] == '@');
                //2 lines per enter in the dialogue files and one for the @ 
                currentDialogue = currentDialogue.Remove(0, 5);
                textPaused = true;
            }

        }
        //Debug.Log(currentDialogue[0].Equals("@"));

    }

    public void interactPressed(InputAction.CallbackContext context)
    {
        if (textPaused)
        {
            resumeText();
        }
    }

    public void resumeText()
    {
        dialogueText.text = "";
        textPaused = false;
    }

    public void startDialogue(string name)
    {
        //dialogueBox.SetActive(true);
        StreamReader reader = new StreamReader("Assets\\Dialogue\\" + name + ".txt");
        //Debug.Log(reader.ReadToEnd());
        //dialogueText.text = reader.ReadToEnd();
        currentDialogue = reader.ReadToEnd();
        reader.Close();
    }
}
