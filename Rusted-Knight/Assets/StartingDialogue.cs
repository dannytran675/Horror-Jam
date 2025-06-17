using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
public class StartingDialogue : NPC
{
    void Update()
    {
        if (isInteractKey())
        {
            Interact();
        }
    }
    bool isInteractKey()
    {
        bool isInteractKey = Input.GetKeyUp("return") || Input.GetKeyUp("space");
        isInteractKey = isInteractKey || Input.GetKeyUp("e") || Input.GetKeyUp("z");
        return isInteractKey;
    }
}
