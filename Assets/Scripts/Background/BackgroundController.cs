using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;
    public GameObject cam;
    public float parallaxEffects;
   
    void Start()
    {
        startPos = transform.position.x;
    }


    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffects;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
