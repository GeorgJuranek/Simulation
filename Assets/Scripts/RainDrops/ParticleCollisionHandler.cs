using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    ParticleSystem particles;

    [SerializeField]
    GameObject stain;

    [SerializeField]
    public LayerMask groundLayermask;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (particles != null)
            MakeParticleVanishOnGround(other);

    }

    private void MakeParticleVanishOnGround(GameObject other)
    {
        if (((1 << other.layer) & groundLayermask) == 0) return; //Checks if layermask is the deposited -> in this case collision with ground

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numParticleCollision = particles.GetCollisionEvents(other, collisionEvents);

        ParticleSystem.Particle[] rainParticles = new ParticleSystem.Particle[particles.main.maxParticles];
        int numParticlesAlive = particles.GetParticles(rainParticles);



        for (int i = 0; i < numParticleCollision; i++)
        {
            Vector3 collisionPosition = collisionEvents[i].intersection;

            for (int j = 0; j < numParticlesAlive; j++)
            {
                if (Vector3.Magnitude(rainParticles[j].position - collisionPosition) < 0.3f)
                {
                    Instantiate(stain, collisionPosition, stain.transform.rotation);

                    rainParticles[j].remainingLifetime = 0; //Kills the particle
                    particles.SetParticles(rainParticles); // Update particle system
                    break;
                }

            }
        }

    }
}


