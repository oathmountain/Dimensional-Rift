using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
    public GameObject ProjectileEmitter;
    public GameObject SphereBlue;
    public float Sphere_force;
    public float timer;
    public GameObject Spawner;
    public bool active;
    public float delay;
    public static float difficultyMulti;
    public static float speedMulti;

    public static void setMulti(int i)			//Set the difficulty multiplier for all spawners
    {
        if (i == 1) 					//Tutorial
        {
            difficultyMulti = 1f;
            speedMulti = 0.85f;
        }
		else if (i == 2)				//Easy
        {
            difficultyMulti = 1.4f;
            speedMulti = 0.8f;
        }
        else if (i == 3)				//Normal
        {
            difficultyMulti = 1.0f;
            speedMulti = 1f;
        }
        else if (i == 4)				//Hard
        {
            difficultyMulti = 0.8f;
            speedMulti = 1.5f;
        }
		else if (i == 5)				//Insane
		{
			difficultyMulti = 0.6f;
            speedMulti = 2f;
        }
    }

    public virtual void Teleport() {
        Spawner.transform.position = new Vector3(Random.Range(-1.25f, 1.3f), Random.Range(0.9f, 2.2f), 14.6f);
    }

    public void setActive(){
         active = false;
    }

    public void setActive(float spawnDelay){
        active = true;
        delay = spawnDelay;
        timer = 0; 
    }

	public float getTimer() {
		return timer;
	}
           
	void Update () {
            timer += Time.deltaTime;
            if ((delay * difficultyMulti) <= timer && active)
        { //
            Teleport();
                GameObject Temp_Projectile_Handler;
                Temp_Projectile_Handler = Instantiate(SphereBlue, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
                Rigidbody Temp_RigidBody;
                Temp_RigidBody = Temp_Projectile_Handler.GetComponent<Rigidbody>();
                Temp_RigidBody.AddForce(transform.forward * (Sphere_force* speedMulti));
                Destroy(Temp_Projectile_Handler, 8f);
                timer = 0;
            }
	}
    void Start() {
		setMulti(DifficultyLevel.difficulty);		//Get the difficulty level from before	
    }
}
