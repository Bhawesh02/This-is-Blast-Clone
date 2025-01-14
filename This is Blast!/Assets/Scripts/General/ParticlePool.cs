using UnityEngine;

public class ParticlePool : PoolService<ParticleSystem>
{
    private ParticleSystem m_particleSystemPrefab;
    private Transform m_parentTransform;

    public ParticlePool(ParticleSystem particleSystemPrefab, Transform parentTransform)
    {
        m_particleSystemPrefab = particleSystemPrefab;
        m_parentTransform = parentTransform;
    }

    protected override ParticleSystem CreateItem()
    {
        return GameObject.Instantiate(m_particleSystemPrefab, m_parentTransform);
    }
}