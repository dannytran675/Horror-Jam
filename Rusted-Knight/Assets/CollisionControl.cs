using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public bool colliding;

    void Update()
    {
        if (Input.GetKeyUp("space") && colliding)
        {
            print("Space key was released");
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
    
}
