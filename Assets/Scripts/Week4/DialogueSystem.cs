using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DialogueSystem : MonoBehaviour
{

    private List<DialogueLine> allDialogueEvents;
 

    // Start is called before the first frame update
    void Start()
    {        
        ReadExternalFile();
    }


    public DialogueLine GetEvent(int id)
    {
        // more readable, simple loop
        foreach (DialogueLine line in allDialogueEvents)
        {
            if (line.id == id)
            {
                return line;
            }
        }

        return null;
        
        //this one also works using a predicate and List.Find (the result will be the same in the end)
        //return allDialogueEvents.Find(x => x.id == id);

    }

    void ReadExternalFile()
    {
        allDialogueEvents = new List<DialogueLine>();

        StreamReader r = new StreamReader(Application.dataPath + "/StreamingAssets/gametext.txt");

        string[] allLinesFromFile = r.ReadToEnd().Split("\n".ToCharArray());

        for (int i = 1; i < allLinesFromFile.Length; i++)
        {
            string[] dialogueLineFromFile = allLinesFromFile[i].Split("\t".ToCharArray());

            DialogueLine newLine = ScriptableObject.CreateInstance<DialogueLine>();

            newLine.id = int.Parse(dialogueLineFromFile[0]);            
            newLine.speaker = dialogueLineFromFile[1];            
            newLine.text = dialogueLineFromFile[2];            
            newLine.nextEvent = int.Parse(dialogueLineFromFile[3]);            
            newLine.lastEventInSequence = (dialogueLineFromFile[4] == "Y");            
            newLine.imgAsset = dialogueLineFromFile[5];            
            newLine.audioAsset = dialogueLineFromFile[6];            

            allDialogueEvents.Add(newLine);
        }

    }


    


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
