using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transform.position.x;
        Debug.Log(xPos);
        transform.position = target.position + offset;
               
    }
}
