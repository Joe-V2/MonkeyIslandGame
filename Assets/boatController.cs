using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boatController : MonoBehaviour
{

    public SphereCollider interactRadius;
    public Collider hullCollider;
    public CharacterController controller;
    public ThirdPersonCameraController camController;
    public Transform ship;
    public Transform viewRelative;
    public Transform riderPosition;
    public Transform riderView;

    public float speed;
    private bool sailing = false;
    private float camLerp = 0;
    private bool camLerping = false;
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(!sailing)
                KeyHintController.instance.ShowHint(KeyCode.E, "Go Sailing");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!sailing)
                {
                    KeyHintController.instance.ShowHint(KeyCode.E, "", false);
                    camController.player = controller.transform;
                    camController.transform.parent = this.gameObject.transform;
                    viewRelative.position = riderView.position;
                    viewRelative.rotation = riderView.rotation;

                    other.gameObject.transform.parent = riderPosition;
                    other.gameObject.transform.localPosition = Vector3.zero;
                    other.gameObject.GetComponent<PlayerMovement>().enabled = false;

                    sailing = true;

                }
                else
                {
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<PlayerMovement>().enabled = true;

                    camController.transform.parent = other.transform;
                    camController.player = camController.transform.parent;
                    camController.gameObject.transform.localPosition = camController.defaultPos;
                    camController.gameObject.transform.localRotation = camController.defaultRot;
                    sailing = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            KeyHintController.instance.ShowHint(KeyCode.E, "", false);
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
                ship.rotation = Quaternion.Slerp(ship.rotation, Quaternion.LookRotation(move), Time.deltaTime * 10);

        }
    }

}
