using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    float speed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
