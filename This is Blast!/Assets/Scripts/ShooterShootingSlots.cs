
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
        public bool hasBrickToShotAt;
    };
    
    
    [Header("Line Settings")]
    [SerializeField]
    private Vector3 m_startPosition; 
    [SerializeField]
    private Vector3 m_endPosition;

    private List<ShootingSlotData> m_shootingSlotDatas = new ();
    
    private void Awake()
    {
        GameplayEvents.OnShooterShootingStateExit += EmptyShootingSlot;
        GameplayEvents.OnShooterFindingBrickToShotAt += HandleOnShooterFindingBrickToShootAt;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnShooterShootingStateExit -= EmptyShootingSlot;
        GameplayEvents.OnShooterFindingBrickToShotAt -= HandleOnShooterFindingBrickToShootAt;
    }
    public void ClearSlots()
    {
        foreach (ShootingSlotData shootingSlotData in m_shootingSlotDatas)
        {
            Destroy(shootingSlotData.slotTransform.gameObject);
        }
        m_shootingSlotDatas.Clear();
    }    
    public void SpawnSlots(int slotsToSpawn)
    {
        Transform slotPrefab = GameConfig.Instance.shooterShootingSlotPrefab;
        for (int slotIndex = 0; slotIndex < slotsToSpawn; slotIndex++)
        {
            m_shootingSlotDatas.Add(new ShootingSlotData()
            {
                slotTransform = Instantiate(slotPrefab, transform),
                isOccupied = false
            });
        }
        ArrangeSlots();
        
    }
    
    private void ArrangeSlots()
    {
        if (m_startPosition == m_endPosition)
        {
            return;
        }
        float step = 1.0f / (m_shootingSlotDatas.Count - 1);
        for (int slotIndex = 0; slotIndex < m_shootingSlotDatas.Count; slotIndex++)
        {
            Transform slotTransofrm = m_shootingSlotDatas[slotIndex].slotTransform;
            slotTransofrm.localPosition =
                Vector3.Lerp(m_startPosition, m_endPosition, step * slotIndex);
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
    
    private void HandleOnShooterFindingBrickToShootAt(Shooter shooter, bool brickFound)
    {
        for (int shootingSlotIndex = 0; shootingSlotIndex < m_shootingSlotDatas.Count; shootingSlotIndex++)
        {
            ShootingSlotData shootingSlotData = m_shootingSlotDatas[shootingSlotIndex];
            if (shootingSlotData.occupiedShooter == shooter)
            {
                shootingSlotData.hasBrickToShotAt = brickFound;
                m_shootingSlotDatas[shootingSlotIndex] = shootingSlotData;
                break;
            }
        }

        if (brickFound)
        {
            return;
        }
        foreach (ShootingSlotData shootingSlotData in m_shootingSlotDatas)
        {
            if (shootingSlotData.hasBrickToShotAt)
            {
                return;
            }
        }
        GameplayEvents.SendOnNoShooterHasBrickToShootAt();
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
