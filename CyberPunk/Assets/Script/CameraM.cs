using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraM : MonoBehaviour
{

    public GameObject target;

    public float cameraspped;

    public float offsetZ;

    Vector3 targetpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        targetpos = new Vector3(target.transform.position.x, target.transform.position.y, -10);

        transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * cameraspped);
    }
}
