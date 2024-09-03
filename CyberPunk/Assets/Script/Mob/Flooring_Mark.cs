using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flooring_Mark : MonoBehaviour
{
    public float duration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
    }

}
