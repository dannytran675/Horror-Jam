using UnityEngine;

public class StartingDialogue : NPC
{
    void Update()
    {
        if (isInteractKey())
        {
            Interact();
        }
    }

    void Start()
    {
        Interact();
    }
    bool isInteractKey()
    {
        bool isInteractKey = Input.GetKeyUp("return") || Input.GetKeyUp("space");
        isInteractKey = isInteractKey || Input.GetKeyUp("e") || Input.GetKeyUp("z");
        return isInteractKey;
    }
}
