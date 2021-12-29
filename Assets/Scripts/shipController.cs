using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour
{
    public Transform root;
    public Rigidbody body;
    public float thrustScale = 1f;


    // Start is called before the first frame update
    void Start()
    {
        if (root == null )root = gameObject.transform;
        if (root == null )body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        body.AddForceAtPosition((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal")) * thrustScale, transform.position, ForceMode.Acceleration);
    }
}
