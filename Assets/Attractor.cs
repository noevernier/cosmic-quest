using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{

    public Rigidbody rigidBody;

    void FixedUpdate() {
        
        Attractor[] attractors = FindObjectsOfType<Attractor>();

        foreach (Attractor attractor in attractors)
        {
            if (attractor != this) {
                Attract(attractor);
            }
        }
    }

    void Attract(Attractor objToAttract) {

        Rigidbody rigidBodyToAttract = objToAttract.rigidBody;

        Vector3 direction = rigidBody.position - rigidBodyToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = (rigidBody.mass * rigidBodyToAttract.mass) / (distance * distance);

        Vector3 force = direction.normalized * forceMagnitude;

        rigidBodyToAttract.AddForce(force);
    }
}
