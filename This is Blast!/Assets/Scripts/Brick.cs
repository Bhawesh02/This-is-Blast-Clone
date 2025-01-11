using System.Collections.Generic;
using UnityEngine;

public class Brick : SlotElement
{
    [SerializeField] private MeshRenderer m_modlePrefab;
    
    private BrickConfigData m_brickConfigData;
    private List<MeshRenderer> m_modlesSpawned = new List<MeshRenderer>();
    private bool m_canHandleInput = true;
    
    public BrickElementData BrickElementData => (BrickElementData)m_elementData;

    public void SetCanHandleInput(bool value)
    {
        m_canHandleInput = value;
    }
    
    public void Config(BrickConfigData brickConfigData, Slot slot)
    {
        m_occupiedSlot = slot;
        m_brickConfigData = brickConfigData;
        m_elementData = GameConfig.Instance.brickElementData;
        transform.SetParent(m_occupiedSlot.transform);
        transform.localPosition = m_elementData.positionOnGrid;
        ConfigModels();
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
    public override void HandleClick()
    {
        if (!m_canHandleInput)
        {
            return;
        }
        //TODO
    }
    public override void HandleDrag()
    {
        if (!m_canHandleInput)
        {
            return;
        }
        //TODO
    }
}