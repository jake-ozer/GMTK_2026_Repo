using UnityEngine;

public class LavaSpout : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(.5f, .5f, 0) * Time.deltaTime;
    }
}
