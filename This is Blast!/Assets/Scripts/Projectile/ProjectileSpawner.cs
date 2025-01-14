using UnityEngine;

public class ProjectileSpawner : MonoSingleton<ProjectileSpawner>
{
    private Projectile m_projectilePrefab;
    private ProjectilePool m_projectilePool;
    private ParticleSystem m_projectileHitParticle;
    private ParticlePool m_projectileHitParticlePool;
    
    
    protected override void Init()
    {
        m_projectilePrefab = GameConfig.Instance.projectilePrefab;
        m_projectileHitParticle = GameConfig.Instance.projectileHitParticle;
        MakePool();
    }

    private void MakePool()
    {
        m_projectilePool = new ProjectilePool(m_projectilePrefab,this);
        m_projectileHitParticlePool = new ParticlePool(m_projectileHitParticle, transform);
    }

    public Projectile GetProjectile()
    {
       Projectile projectile = m_projectilePool.GetItem();
       projectile.gameObject.SetActive(true);
       return projectile;
    }

    public void ReturnProjectile(Projectile projectile)
    {
        projectile.OnReturnToPool();
        OnGameObjectReturnToPool(projectile.gameObject);
        m_projectilePool.ReturnItem(projectile);
    }

    public ParticleSystem GetProjectileHitParticle()
    {
        ParticleSystem hitParticleSystem = m_projectileHitParticlePool.GetItem();
        hitParticleSystem.gameObject.SetActive(true);
        StopCoroutine(CoroutineUtils.Delay(hitParticleSystem.main.duration, () =>
        {
            ReturnProjectileHitParticle(hitParticleSystem);
        }));
        return hitParticleSystem;
    }

    public void ReturnProjectileHitParticle(ParticleSystem particleSystem)
    {
        OnGameObjectReturnToPool(particleSystem.gameObject);
        m_projectileHitParticlePool.ReturnItem(particleSystem);
    }
    
    private static void OnGameObjectReturnToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.localPosition = Vector3.zero;
    }
}