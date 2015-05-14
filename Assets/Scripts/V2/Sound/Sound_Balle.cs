using UnityEngine;
using System.Collections;

public class HitBall : MonoBehaviour
{

    public AudioClip voiceBall;
    

       void Start()
    {

    }


    void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.tag == "Canvas")
        {
             AudioClip.Play(voiceBall);;
         
        }

    }
}