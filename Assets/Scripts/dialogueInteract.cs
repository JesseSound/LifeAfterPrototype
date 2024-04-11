using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yarn.Unity;

public class dialogueInteract : MonoBehaviour
{
    // Start is called before the first frame update

    bool canInteract;

    DialogueRunner dialogueRunner;
    LineView lineView;
    ThirdPersonController playerMovement;
    

   
    void Start()
    {
        canInteract = false;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        lineView = FindObjectOfType<LineView>();
        playerMovement = FindObjectOfType<ThirdPersonController>();
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && !dialogueRunner.IsDialogueRunning && Input.GetKey(KeyCode.E))
            dialogueRunner.StartDialogue(gameObject.name);

        if (dialogueRunner.IsDialogueRunning && Input.GetKeyDown(KeyCode.F)) { 
            lineView.UserRequestedViewAdvancement();

        }
        if (dialogueRunner.IsDialogueRunning)
        {
            playerMovement.enabled = false;
            canInteract = false;
        }
        else 
        {
            playerMovement.enabled = true;
        }

       


    }
}
