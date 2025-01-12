using System;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const string BRICK_TAG = "Brick";
    
    private BrickColors m_brickColorToHit;
    private Brick m_brickToHit;
    private Vector3 m_brickPosition;
    private bool m_fired;
    public BrickColors BrickColorToHit => m_brickColorToHit;
    
    public void Init(BrickColors brickColor, Brick brick, Vector3 position)
    {
        m_brickColorToHit = brickColor;
        m_brickToHit = brick;
        m_brickPosition = m_brickToHit.transform.position;
        m_fired = false;
        transform.position = position;
    }

    public void Fire()
    {
        if (!m_brickToHit)
        {
            return;
        }
        Quaternion rotation = Quaternion.LookRotation(m_brickPosition - transform.position);
        transform.rotation = rotation;
        m_fired = true;
    }

    private void Update()
    {
        if (!m_fired)
        {
            return;
        }
        Vector3 directionToTarget = m_brickPosition - transform.position;
        transform.Translate(directionToTarget.normalized * (GameConfig.Instance.projectTravleSpeed * Time.deltaTime));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(BRICK_TAG))
        {
            return;
        }

        m_fired = false;
        ProjectileSpawner.Instance.ReturnProjectile(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_brickPosition);
    }
}