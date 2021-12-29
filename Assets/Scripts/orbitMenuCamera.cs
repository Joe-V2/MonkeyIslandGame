using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitMenuCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform focalPoint;
    public float orbitSpeed;

    private void Awake()
    {
        //this.gameObject.transform.rotation = Quaternion.LookRotation(focalPoint.position, focalPoint.up);
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.RotateAround(focalPoint.position, focalPoint.up, orbitSpeed * Time.deltaTime); 
    }
}
