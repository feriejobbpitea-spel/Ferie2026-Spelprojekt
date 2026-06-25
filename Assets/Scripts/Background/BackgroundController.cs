using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;
    private GameObject cam;
    public float parallaxEffects;

    private void Awake()
    {
        cam = Camera.main.gameObject;
    }

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
