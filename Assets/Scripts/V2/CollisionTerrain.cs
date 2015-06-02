using UnityEngine;
using System.Collections;

public class CollisionTerrain : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("testCollision");


            // display sprite on dome

            Vector3 averageContactPoint = Vector3.zero;
            Vector3 averageContactNormal = Vector3.zero;

            foreach (ContactPoint contact in other.contacts)
            {
                averageContactPoint += contact.point;
                averageContactNormal += contact.normal;
                print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
                Debug.DrawLine(contact.point, contact.point + contact.normal * 4, Color.green, 2.0f, false);
            }

            averageContactPoint = averageContactPoint / other.contacts.Length;
            averageContactNormal = averageContactNormal / other.contacts.Length;

            Debug.DrawLine(averageContactPoint, averageContactPoint + averageContactNormal * 4, Color.red, 5.0f, false);

        }
    }
}
