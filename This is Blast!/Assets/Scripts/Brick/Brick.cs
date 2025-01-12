using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class Brick : SlotElement
{
    [SerializeField] private MeshRenderer m_modlePrefab;
    
    private BrickConfigData m_brickConfigData;
    private List<MeshRenderer> m_modlesSpawned = new List<MeshRenderer>();
    private Vector2Int m_nextSlotCoord;

    private bool m_isTargeted;
    
    public BrickElementData BrickElementData => (BrickElementData)m_elementData;
    public BrickConfigData BrickConfigData => m_brickConfigData;
    public bool IsTargeted => m_isTargeted;
    public Vector2Int CurrentSlotCoord => m_occupiedSlot.Coord;
    
    
    public void SetIsTargeted()
    {
        m_isTargeted = true;
    }
    private void Awake()
    {
        GameplayEvents.OnBrickSlotEmpty += HandleOnBrickSlotEmpty;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnBrickSlotEmpty -= HandleOnBrickSlotEmpty;
    }

    public void Config(BrickConfigData brickConfigData, Slot slot, MyGrid grid)
    {
        Config(GameConfig.Instance.brickElementData, slot, grid);
        m_brickConfigData = brickConfigData;
        transform.SetParent(m_occupiedSlot.transform);
        transform.localPosition = m_elementData.positionOnSlot;
        ConfigModels();
        CalculateNextSlotCoord();
    }

    private void ConfigModels()
    {
        if (m_modlesSpawned.Count != m_brickConfigData.brickStrenght)
        {
            SpawnModels();
        }
        ChangeModelsMaterials();
    }
    
    private void SpawnModels()
    {
        foreach (MeshRenderer meshRenderer in m_modlesSpawned)
        {
            Destroy(meshRenderer.gameObject);
        }
        m_modlesSpawned.Clear();
        Vector3 modelPosition = transform.position;
        MeshRenderer modelSpawned;
        for (int modleIndex = 0; modleIndex < m_brickConfigData.brickStrenght; modleIndex++)
        {
            modelSpawned = Instantiate(m_modlePrefab,modelPosition, Quaternion.identity, transform);
            m_modlesSpawned.Add(modelSpawned);
            modelPosition.y += BrickElementData.modleYIncrement;
        }        
    }

    private void ChangeModelsMaterials()
    {
        Material materialToApply = GameConfig.Instance.GetBrickMaterial(m_brickConfigData.brickColor);
        foreach (MeshRenderer meshRenderer in m_modlesSpawned)
        {
            meshRenderer.material = materialToApply;
        }
    }
    
    private void CalculateNextSlotCoord()
    {
        Vector2Int currentCoord = m_occupiedSlot.Coord;
        m_nextSlotCoord = currentCoord;
        if (currentCoord.y == 0)
        {
            return;
        }
        m_nextSlotCoord.y -= 1;
    }
    public override void HandleClick()
    {
        //Nothing
    }
    public override void HandleDrag()
    {
        //Nothing
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (!projectile)
        {
            return;
        }
        HandleOnProjectileCollision(projectile);
    }
    
    private void HandleOnProjectileCollision(Projectile projectile)
    {
        if (projectile.BrickColorToHit != m_brickConfigData.brickColor)
        {
            return;
        }
        int lastIndex = m_modlesSpawned.Count - 1;
        Transform nextModleTransform = m_modlesSpawned[lastIndex].transform;
        m_modlesSpawned.RemoveAt(lastIndex);
        nextModleTransform.DOScale(Vector3.zero, GameConfig.Instance.brickScaleDownDuration)
            .SetEase(GameConfig.Instance.brickScaleDownEase)
            .OnComplete(() =>
            {
                m_isTargeted = false;
            if (m_modlesSpawned.Count == 0)
            {
                m_occupiedSlot.EmptySlot();
                Destroy(gameObject);
            }
            });
        m_brickConfigData.brickStrenght--;
    }
    
    private void HandleOnBrickSlotEmpty(Vector2Int coord)
    {
        if (m_nextSlotCoord != coord || m_occupiedSlot.Coord == coord)
        {
            return;
        }
        Slot newSlot = m_grid.GetSlot(coord);
        if (newSlot.IsOccupied)
        {
            return;
        }
        m_occupiedSlot.EmptySlot();
        Vector3 localPosition = transform.localPosition;
        transform.SetParent(newSlot.transform);
        transform.DOLocalMove(localPosition, GameConfig.Instance.brickMoveDownDuration).SetEase(GameConfig.Instance.brickMoveDownEase).OnComplete(
            () =>
            {
                newSlot.OccupySlot(this);
                CalculateNextSlotCoord();
            });
    }
}