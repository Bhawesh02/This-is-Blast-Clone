﻿using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer m_trailRenderer;
    
    private BrickColors m_brickColorToHit;
    private Brick m_brickToHit;
    private Vector3 m_brickPosition;
    private bool m_fired;
    private Vector3 m_directionToTarget;
    public BrickColors BrickColorToHit => m_brickColorToHit;
    public Brick BrickToHit => m_brickToHit;

    public void Init(BrickColors brickColor, Brick brick, Vector3 position)
    {
        m_fired = false;
        m_brickColorToHit = brickColor;
        m_brickToHit = brick;
        m_brickPosition = m_brickToHit.transform.position;
        transform.position = position;
    }

    public void Fire()
    {
        if (!m_brickToHit)
        {
            return;
        }
        m_trailRenderer.Clear();
        m_directionToTarget = m_brickPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(m_directionToTarget);
        transform.rotation = rotation;
        m_fired = true;
    }

    private void Update()
    {
        if (!m_fired)
        {
            return;
        }
        m_directionToTarget = m_brickPosition - transform.position;
        m_directionToTarget.Normalize();
        transform.position += m_directionToTarget * (GameConfig.Instance.projectTravleSpeed * Time.deltaTime);
    }
    
    public void OnReturnToPool()
    {
        m_fired = false;
        m_trailRenderer.Clear();
    }
}