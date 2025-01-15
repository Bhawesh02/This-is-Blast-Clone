using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterShootingState : ShooterState
{
    private List<Brick> m_bricksToShootAt = new();
    private Coroutine m_shootingCoroutine;
    private int m_projectileCount;
    private bool m_canShoot = true;
    
    public ShooterShootingState(Shooter shooter, ShooterStates shooterState) : base(shooter, shooterState)
    {
    }

    public override void OnEnter()
    {
        m_projectileCount = m_shooter.ShooterProjectileCount;
        m_shootingCoroutine = m_shooter.StartCoroutine(ShootBrick());
        m_canShoot = true;
        GameplayEvents.OnLevelCompleted += HandleOnLevelOver;
        GameplayEvents.OnLevelFailed += HandleOnLevelOver;
    }

    public override void OnExit()
    {
        m_shooter.StopCoroutine(m_shootingCoroutine);
        GameplayEvents.SendOnShooterShootingStateExit(m_shooter);
        GameplayEvents.OnLevelCompleted -= HandleOnLevelOver;
        GameplayEvents.OnLevelFailed -= HandleOnLevelOver;
    }
    
    
    private void HandleOnLevelOver()
    {
        m_canShoot = false;
    }

    public override void OnDestroyed()
    {
        //Nothing
    }

    private IEnumerator ShootBrick()
    {
        WaitForSeconds projectileFireDelayWait = new WaitForSeconds(GameConfig.Instance.shooterFireDelay);
        while (m_projectileCount > 0)
        {
            if (!m_canShoot)
            {
                yield break;
            }
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
                GameplayEvents.SendOnShooterFindingBrickToShotAt(m_shooter, false);
                yield return projectileFireDelayWait;
                continue;
            }
            GameplayEvents.SendOnShooterFindingBrickToShotAt(m_shooter, true);
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
        if (!brick || brick.IsMoving)
        {
            return;
        }
        brick.SetIsTargeted();
        m_shooter.LookAtPoint(brick.transform.position);
        Projectile projectile = ProjectileSpawner.Instance.GetProjectile();
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