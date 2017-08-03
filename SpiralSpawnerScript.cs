using UnityEngine;
using System.Collections;

public class SpiralSpawnerScript : SpawnerScript{
    public GameObject SpiralProjectileEmitter;
    public GameObject SphereMagenta;
    public float Forward_force;
    public GameObject SpiralSpawner;

    public override void Teleport(){
        SpiralSpawner.transform.position = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.9f, 1.4f), 14.5f);
    }

    void Update(){
        timer += Time.deltaTime;
        if ((delay * difficultyMulti) <= timer && active)
        {
            GameObject Temp_Projectile_Handler;
            Temp_Projectile_Handler = Instantiate(SphereMagenta, SpiralProjectileEmitter.transform.position, SpiralProjectileEmitter.transform.rotation) as GameObject;
            Rigidbody Temp_RigidBody;
            Temp_RigidBody = Temp_Projectile_Handler.GetComponent<Rigidbody>();
			Temp_RigidBody.AddForce(transform.forward * (Forward_force* speedMulti));
            Destroy(Temp_Projectile_Handler, 8f);
            timer = 0;
            Teleport();
        }
    }
}
