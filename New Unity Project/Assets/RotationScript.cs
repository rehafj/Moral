using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    Vector3 v3;
    float speed = 0.01f;
    public bool canRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        v3 = gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            gameObject.transform.Rotate(0, 90 * speed, 0);
        }
    }

    public void EnableRotation()
    {
        canRotate = true;

    }

    public void DisableRotation()
    {
        canRotate = false;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);    }
}
