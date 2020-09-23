using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    public GameObject GameObjectToSpawn;

    private void Awake()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            ParticleCollisionEvent collisionEvent = collisionEvents[i];
            Vector3 pos = collisionEvent.intersection;
            Vector3 normal = collisionEvent.normal;

            GameObject spawnedGO = Instantiate(GameObjectToSpawn, pos, Quaternion.identity);
            
            Destroy(spawnedGO, 2);
        }

        FindObjectOfType<AudioManager>().PlaySound("Droplet");
    }
}