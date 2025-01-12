
using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ShooterShootingSlots : MonoBehaviour
{
    private struct ShootingSlotData
    {
        public Transform slotTransform;
        public bool isOccupied;
        public Shooter occupiedShooter;
    };
    
    
    [Header("Line Settings")]
    [SerializeField]
    private Vector3 m_startPosition; 
    [SerializeField]
    private Vector3 m_endPosition;

    private List<ShootingSlotData> m_shootingSlotDatas = new ();
    private int m_childCount;
    
    private void Awake()
    {
        ArrangeChildren();
        for (int childIndex = 0; childIndex < m_childCount; childIndex++)
        {
            m_shootingSlotDatas.Add(new ShootingSlotData()
            {
                slotTransform = transform.GetChild(childIndex),
                isOccupied = false
            });
        }

        GameplayEvents.OnShooterShootingStateExit += EmptyShootingSlot;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnShooterShootingStateExit -= EmptyShootingSlot;
    }

    [Button]
    private void ArrangeChildren()
    {
        if (m_startPosition == m_endPosition)
        {
            return;
        }
        m_childCount = transform.childCount;
        if (m_childCount == 0)
        {
            return;
        }
        float step = 1.0f / (m_childCount - 1);
        for (int childIndex = 0; childIndex < m_childCount; childIndex++)
        {
            Transform child = transform.GetChild(childIndex);
            child.localPosition = Vector3.Lerp(m_startPosition, m_endPosition, step * childIndex);
        }
    }

    public Transform GetNextFreeShootingSlot(Shooter shooter)
    {
        for (int shootingSlotindex = 0; shootingSlotindex < m_shootingSlotDatas.Count; shootingSlotindex++)
        {
            ShootingSlotData shootingSlotData = m_shootingSlotDatas[shootingSlotindex];
            if (shootingSlotData.isOccupied)
            {
                continue;
            }
            shootingSlotData.isOccupied = true;
            shootingSlotData.occupiedShooter = shooter;
            m_shootingSlotDatas[shootingSlotindex] = shootingSlotData;
            return shootingSlotData.slotTransform;
        }

        return null;
    }

    private void EmptyShootingSlot(Shooter shooter)
    {
        for (int dataIndex = 0; dataIndex < m_shootingSlotDatas.Count; dataIndex++)
        {
            ShootingSlotData shootingSlotData = m_shootingSlotDatas[dataIndex];
            if (shootingSlotData.occupiedShooter != shooter)
            {
                continue;
            }
            shootingSlotData.isOccupied = false;
            shootingSlotData.occupiedShooter = null;
            m_shootingSlotDatas[dataIndex] = shootingSlotData;
        }
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (m_startPosition != m_endPosition)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + m_startPosition, transform.position + m_endPosition);
        }
    }
#endif
    
}
