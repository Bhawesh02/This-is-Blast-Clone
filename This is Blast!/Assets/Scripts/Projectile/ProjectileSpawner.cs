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

    public Projectile GetProjectile()
    {
       Projectile projectile = m_projectilePool.GetItem();
       projectile.gameObject.SetActive(true);
       return projectile;
    }

    public void ReturnProjectile(Projectile projectile)
    {
        projectile.OnReturnToPool();
        projectile.gameObject.SetActive(false);
        projectile.transform.localPosition = Vector3.zero;
        m_projectilePool.ReturnItem(projectile);
    }
}