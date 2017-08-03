using UnityEngine;
using System.Collections;

public class ThrowSpawnerScript : SpawnerScript
{
    public GameObject ThrowProjectileEmitter;
    public GameObject SphereCyan;
    public GameObject ThrowSpawner;

    void Update () {
        timer += Time.deltaTime;
        if ((delay * difficultyMulti) <= timer && active)
        {
            GameObject Temp_Projectile_Handler;
            Temp_Projectile_Handler = Instantiate(SphereCyan, ThrowProjectileEmitter.transform.position, ThrowProjectileEmitter.transform.rotation) as GameObject;
            Rigidbody Temp_RigidBody;
            Temp_RigidBody = Temp_Projectile_Handler.GetComponent<Rigidbody>();
            Temp_RigidBody.AddForce(transform.forward * Sphere_force);
            Destroy(Temp_Projectile_Handler, 10f);
            timer = 0;
            Teleport();
        }
    }

    public override void Teleport(){
        ThrowSpawner.transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(0.35f, 0.6f), 15f);
    }
}
