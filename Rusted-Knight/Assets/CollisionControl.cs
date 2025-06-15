using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public bool colliding;

    void Update()
    {
        if (isInteractKey() && colliding)
        {
            print("Interactable Key Pressed");
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("INTERACTABLE!");
            colliding = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("EXIT INTERACTABLE!");
            colliding = false;
        }
    }

    bool isInteractKey()
    {
        bool isInteractKey = Input.GetKeyUp("return") || Input.GetKeyUp("space");
        isInteractKey = isInteractKey || Input.GetKeyUp("e") || Input.GetKeyUp("z");
        return isInteractKey;
    }
    
}
