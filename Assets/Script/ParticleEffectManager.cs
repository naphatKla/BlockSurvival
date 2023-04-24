using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleEffectManager : Singleton<ParticleEffectManager>
{

    void Awake()
    {
        Instance.enabled = true;
    }

    public void PlayParticleEffect(ParticleSystem particleEffect, Vector3 position)
    {
        Destroy(Instantiate(particleEffect, position, Quaternion.identity), particleEffect.main.startLifetime.constantMax);
    }
}
