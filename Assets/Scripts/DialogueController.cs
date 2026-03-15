using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera cam;
    [SerializeField] SpriteRenderer dialogueBackground;
    [SerializeField] float textBoxHeight = -1.65f;
    [SerializeField] GameObject dialgoueOptionPrefab;
    private string[] choices;
    public float textSpeed = 1f;
    private float textTimer = 0;
    private string currentDialogue = "";
    private bool textPaused = true;
    private NPCController currentNPC = null;
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

            else if (currentDialogue.Length > 0 && currentDialogue[0] == '[')
            {
                loadChoices();
            }

        }
        //Debug.Log(currentDialogue[0].Equals("@"));

    }

    public void interactPressed(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && this.gameObject.activeInHierarchy)
        {
            if (currentDialogue.Length < 1 && choices.Length > 0)
            {
                playerController.movable = true;
                dialogueBox.SetActive(false);
                dialogueText.text = "";
                if (currentNPC != null)
                {
                    currentNPC.gameObject.SetActive(false);
                    currentNPC = null;
                }
                
            }
            else if (textPaused)
            {
                resumeText();
            }
            else
            {
                while (currentDialogue.Length > 0 && currentDialogue[0] != '@' && currentDialogue[0] != '[')
                {
                    dialogueText.text += currentDialogue[0];
                    currentDialogue = currentDialogue.Remove(0, 1);

                }
                if (currentDialogue.Length > 0 && currentDialogue[0] == '@')
                {
                    currentDialogue = currentDialogue.Remove(0, 5);
                }
                if (currentDialogue.Length > 0 && currentDialogue[0] == '[')
                {
                    loadChoices();
                }
                textPaused = true;
            }
        }
    }

    private void loadChoices()
    {
        choices = currentDialogue.Split("[");
        currentDialogue = "";
        foreach(string c in choices)
        {
            Debug.Log(c);
        }

        int numChoices = choices.Length / 2;
        GameObject[] choiceList = new GameObject[numChoices];
        for(int i = 0; i < numChoices; i += 1)
        {
            choiceList[i] = Instantiate(dialgoueOptionPrefab, new Vector3(transform.position.x - 3 + (30/choices.Length * i), cam.transform.position.y, 0), cam.transform.rotation);
            choiceList[i].GetComponent<DialogueChoice>().setSprite(choices[2*i], choices[2*i+1]);
            choiceList[i].SetActive(true);
            
        }



    }

    public void resumeText()
    {
        dialogueText.text = "";
        textPaused = false;
        
    }

    public void startDialogue(string name)
    {
        if (textPaused && !dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(true);
            Debug.Log(cam.GetComponent<Rigidbody2D>().position.x);
            dialogueBackground.transform.position = new Vector3(cam.GetComponent<Rigidbody2D>().position.x, cam.GetComponent<Rigidbody2D>().position.y - textBoxHeight, -5);
            //dialogueBox.transform.position = new Vector3(cam.GetComponent<Rigidbody2D>().position.x, cam.GetComponent<Rigidbody2D>().position.y, -5);
            StreamReader reader = new StreamReader("Assets\\Dialogue\\" + name + ".txt");
            Debug.Log(name);
            //dialogueText.text = reader.ReadToEnd();
            currentDialogue = reader.ReadToEnd();
            reader.Close();
            playerController.movable = false;
            textPaused = false;

        }
    }

    public void startDialogue(string name, NPCController npc)
    {
        if (textPaused && !dialogueBox.activeInHierarchy)
        {
            currentNPC = npc;
            dialogueBox.SetActive(true);
            Debug.Log(cam.GetComponent<Rigidbody2D>().position.x);
            dialogueBackground.transform.position = new Vector3(cam.GetComponent<Rigidbody2D>().position.x, cam.GetComponent<Rigidbody2D>().position.y - textBoxHeight, -5);
            //dialogueBox.transform.position = new Vector3(cam.GetComponent<Rigidbody2D>().position.x, cam.GetComponent<Rigidbody2D>().position.y, -5);
            StreamReader reader = new StreamReader("Assets\\Dialogue\\" + name + ".txt");
            Debug.Log(name);
            //dialogueText.text = reader.ReadToEnd();
            currentDialogue = reader.ReadToEnd();
            reader.Close();
            playerController.movable = false;
            textPaused = false;

        }
    }
}
