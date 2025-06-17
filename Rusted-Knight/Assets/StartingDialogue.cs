using UnityEngine;

public class StartingDialogue : MonoBehaviour
{
    public IInteractable dialogue;

    void Update()
    {
        if (interactionPossible() && isInteractKey())
        {
            dialogue.Interact();
        }
    }
    bool isInteractKey()
    {
        bool isInteractKey = Input.GetKeyUp("return") || Input.GetKeyUp("space");
        isInteractKey = isInteractKey || Input.GetKeyUp("e") || Input.GetKeyUp("z");
        return isInteractKey;
    }

    bool interactionPossible()
    {
        return dialogue != null;
    }

}
