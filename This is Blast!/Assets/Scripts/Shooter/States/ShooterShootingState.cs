using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterShootingState : ShooterState
{
    private List<Brick> m_bricksToShootAt = new();
    private Coroutine m_shootingCoroutine;
    private int m_projectileCount;
    public ShooterShootingState(Shooter shooter, ShooterStates shooterState) : base(shooter, shooterState)
    {
    }

    public override void OnEnter()
    {
        m_projectileCount = m_shooter.ShooterProjectileCount;
        m_shootingCoroutine = m_shooter.StartCoroutine(ShootBrick());
    }

    public override void OnExit()
    {
        m_shooter.StopCoroutine(m_shootingCoroutine);
    }
    
    private IEnumerator ShootBrick()
    {
        WaitForSeconds projectileFireDelayWait = new WaitForSeconds(GameConfig.Instance.projectileFireDelay);
        while (m_projectileCount > 0)
        {
            m_bricksToShootAt.Clear();
            foreach (Brick brick in GameManager.Instance.GetBricksToShootAt())
            {
                if (brick.BrickConfigData.brickColor == m_shooter.ShooterColor)
                {
                    m_bricksToShootAt.Add(brick);
                }
            }
            if (m_bricksToShootAt.Count == 0)
            {
                yield return projectileFireDelayWait;
                continue;
            }
            ShootNearestBrick();
            yield return projectileFireDelayWait;
        }
    }

    private void ShootNearestBrick()
    {
        Brick nearestBrick = null;
        float distanceToNearestBrick = Mathf.Infinity;
        float distanceToBrick;
        foreach (Brick brick in m_bricksToShootAt)
        {
            distanceToBrick = Vector3.Distance(m_shooter.transform.position, brick.transform.position);
            if (brick.IsTargeted || distanceToBrick > distanceToNearestBrick)
            {
                continue;
            }
            distanceToNearestBrick = distanceToBrick;
            nearestBrick = brick;
        }
        ShootAtBrick(nearestBrick);
    }

    private void ShootAtBrick(Brick brick)
    {
        if (!brick)
        {
            return;
        }
        Debug.Log($"Brick At Slot : {brick.CurrentSlotCoord}");
        brick.SetIsTargeted();
        m_shooter.LookAtPoint(brick.transform.position);
        Projectile projectile = ProjectileSpawner.Instance.GetProjectile();
        projectile.transform.position = m_shooter.transform.position;
        projectile.Init(m_shooter.ShooterColor, brick, m_shooter.transform.position);
        projectile.Fire();
        m_projectileCount--;
        m_shooter.UpdateProjectileCount(m_projectileCount);
    }
    
    public override void OnUpdate()
    {
        //Nothing
    }
    
    public override void OnClick()
    {
        //Nothing
    }
}