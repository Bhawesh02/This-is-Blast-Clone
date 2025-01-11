using UnityEngine;

public class ProjectilePool : PoolService<Projectile>
{
    private Projectile m_projectilePrefab;
    private ProjectileSpawner m_projectileSpawner;

    public ProjectilePool(Projectile projectilePrefab, ProjectileSpawner projectileSpawner)
    {
        m_projectilePrefab = projectilePrefab;
        m_projectileSpawner = projectileSpawner;
    }

    protected override Projectile CreateItem()
    {
        return GameObject.Instantiate(m_projectilePrefab, m_projectileSpawner.transform);
    }
}