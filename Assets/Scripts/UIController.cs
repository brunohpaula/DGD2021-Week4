using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text message1;
    public Text message2;
    public Text message3;

    public PlayerBehaviour player;


    public UIDialogue dialogueBox;

    public GameObject gameOverMessage;

    // Start is called before the first frame update
    void Start()
    {      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayGameOver()
    {
        gameOverMessage.SetActive(true);
    }

    public void DisplayEvent(DialogueLine d)
    {
        dialogueBox.gameObject.SetActive(true);

        dialogueBox.currentText.text = d.text;       

        dialogueBox.speakerImg.sprite = Resources.Load<Sprite>("Art/DialogueImages/" + d.imgAsset);

    }

    public void HideDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }
}
