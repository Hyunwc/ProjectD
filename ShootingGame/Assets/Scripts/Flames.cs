using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    ParticleSystem ps;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Effect Collision : {other.name}");
    }
}
