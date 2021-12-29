using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public enum itemBehaviour
    {
        coin,
        powerup,
        trap
    }

    public itemBehaviour pickupType;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f * Mathf.Sin(Time.frameCount * 0.15f), transform.position.z);
        transform.rotation = Quaternion.Euler(-90,0,Time.frameCount * 3 % 360);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            switch (pickupType)
            {
                case itemBehaviour.coin:
                    this.gameObject.GetComponent<AudioSource>().Play();
                    this.gameObject.GetComponent<Renderer>().enabled = false;
                    this.gameObject.GetComponent<Collider>().enabled = false;
                    StartCoroutine(waitToDestroy(0));
                    break;
                default:
                    break;
            }
    }

    IEnumerator waitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        yield return new WaitUntil(() => !this.gameObject.GetComponent<AudioSource>().isPlaying);
        Destroy(this.gameObject);
    }
}
