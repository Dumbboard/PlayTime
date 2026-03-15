using Unity.VisualScripting;
using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    public string Dialogue;
    public Sprite sprite;
    [SerializeField] Sprite[] options;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void setSprite(string dialogue, string sprite){
        this.Dialogue = dialogue;
        foreach(Sprite s in options)
        {
            if (s.name.Equals(sprite + "_0"))
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = s;//Sprite.Create(t, new Rect(new Vector2(800, 600), new Vector2(800, 600)), new Vector2(transform.position.x, transform.position.y));
            }
        }     
    }

}
