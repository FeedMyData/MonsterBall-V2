using UnityEngine;
using System.Collections;

public class CollisionTerrain : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("testCollision");
        }
    }
}
