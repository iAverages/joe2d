using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private Queue<string> sentences;
    



    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue()
    {
        Debug.Log("started conversation with " + dialogue.npc_name);
    }
    
}
