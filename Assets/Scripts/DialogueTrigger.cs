using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueController dc;
    [SerializeField] TextAsset[] dialogues;
    [SerializeField] NPCController npc;
    public int EncounterType = 0;
    public bool interactable = true;
    
    public void NPCInteract()
    {
        Debug.Log(dialogues.Length);
        if (dialogues.Length > 0)
        {
            //dc.startDialogue(dialogues[0].name);
            npc.beginGrowWithDialogue(dialogues[0].name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            switch (EncounterType)
            {
                case 1:

                    break;
                    
                case 2:

                    break;

                default:
                    interactable = true;
                    break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactable = false;
    }
}
