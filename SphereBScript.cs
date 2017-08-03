using UnityEngine;
using System.Collections;

public class SphereBScript : MonoBehaviour {
    public GameObject particleEffect; // drag our effect prefab here
    public GameObject spawnParticle;
	public float rotationSpeed = 90f;
	public Rigidbody rb;
    public Right_WandController controller;
    public bool TestGravity = false;

    void Update(){
		transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider coll){
		if (coll.tag == "SphereDestroyer") {
            GameObject explosion = Instantiate(particleEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject); // destroy the object
            Destroy(explosion, 3); // delete the effect after 3 seconds
        }

		if (coll.tag == "LaserDestroyer") {				//If colliding with the shield
			rotationSpeed = 0f;
			if (TestGravity) {
				rb.useGravity = true;
			}
		}

        if (coll.tag == "ParticleActivator") {       //Spawns particle effect when the sphere go through the portal
            GameObject spawnSmoke = Instantiate(spawnParticle, transform.position, Quaternion.identity) as GameObject;
            Destroy(spawnSmoke, 3); // delete the effect after 3 seconds
        }

    }
}
