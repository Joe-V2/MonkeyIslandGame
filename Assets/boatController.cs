using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatController : MonoBehaviour
{

    public SphereCollider interactRadius;
    public Collider hullCollider;
    public CharacterController controller;
    public Transform viewRelative;
    public Transform riderPosition;
    public Transform riderView;

    public float speed;
    private bool sailing = false;
    private float camLerp = 0;
    private bool camLerping = false;
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if (!sailing)
            {
                other.gameObject.transform.parent = riderPosition;
                other.gameObject.transform.localPosition = Vector3.zero;
                other.gameObject.GetComponent<PlayerMovement>().enabled = false;
                sailing = true;

            }
            else
            {
                other.gameObject.transform.parent = null;
                other.gameObject.GetComponent<PlayerMovement>().enabled = true;
                sailing = false;
            }
        }
    }


    private void LerpCamera(bool goingIn)
    {
        if ((camLerp <= 1))
            if (goingIn)
            {
                camLerp += Time.deltaTime;
                viewRelative.localPosition = Vector3.Lerp(viewRelative.position, riderView.position, camLerp);
                viewRelative.localRotation = Quaternion.Lerp(viewRelative.rotation, riderView.rotation, camLerp);
            }
            else
            {
                camLerp += Time.deltaTime;
                viewRelative.position = Vector3.Lerp(riderView.position, viewRelative.position, camLerp);
                viewRelative.rotation = Quaternion.Lerp(viewRelative.rotation, riderView.rotation, 1-camLerp);
            }
        else
        {
            camLerp = 0;
            camLerping = false;
        }
    }

    private void Update()
    {
        if (camLerping)
            LerpCamera(true);
        if(sailing)
        {



            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            var forward = viewRelative.forward;
            var right = viewRelative.right;

            forward.y = right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 move = right * x + forward * z;


            controller.Move(move * speed * Time.deltaTime);
            if (move != Vector3.zero)
                this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 10);
        }
    }

}
