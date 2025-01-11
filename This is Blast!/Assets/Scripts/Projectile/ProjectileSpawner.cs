using UnityEngine;

public class ProjectileSpawner : MonoSingleton<ProjectileSpawner>
{
    private Projectile m_projectile;
    private ProjectilePool m_projectilePool;
    protected override void Init()
    {
        m_projectile = GameConfig.Instance.projectilePrefab;
        MakePool();
    }

    private void MakePool()
    {
        m_projectilePool = new ProjectilePool(m_projectile,this);
    }

    public Projectile GetProjectile(Vector3 position)
    {
       Projectile projectile = m_projectilePool.GetItem();
       projectile.transform.position = position;
       return projectile;
    }

    public void ReturnProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        m_projectilePool.ReturnItem(projectile);
    }
}