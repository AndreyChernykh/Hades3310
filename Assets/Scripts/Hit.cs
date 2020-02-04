using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {
    public ParticleSystem particle;

    void Start() {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }

    public void EmitParticles() {
        particle.Play();
    }
}
