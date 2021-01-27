using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.Networking;
public class DialogueSystem : MonoBehaviour
{

    private List<DialogueLine> allDialogueEvents;

    private string[] allLinesFromFile;
    

    // Start is called before the first frame update
    void Awake()
    {


        #if UNITY_WEBGL
            StartCoroutine("ReadFileAsync");            
        #else
            ReadExternalFile();
        #endif
    }


    IEnumerator ReadFileAsync()
    {
        UnityWebRequest www = UnityWebRequest.Get(Application.dataPath + "/StreamingAssets/gametext.txt");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            allLinesFromFile = www.downloadHandler.text.Split("\n".ToCharArray());
            // Or retrieve results as binary data            
        }
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



        #if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            StreamReader r = new StreamReader(Application.dataPath + "/StreamingAssets/gametext.txt");
            allLinesFromFile = r.ReadToEnd().Split("\n".ToCharArray());
        #endif

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
