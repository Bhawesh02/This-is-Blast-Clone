using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameConfig : GenericConfig<GameConfig>
{
    [Serializable]
    private struct BrickColorMaterialMap
    {
        public BrickColors brickColor;
        public Material brickMaterial;
    }
    
    [SerializeField] private List<BrickColorMaterialMap> m_brickColorMaterialData;
    
    public BrickElementData brickElementData;

    public Material GetBrickMaterial(BrickColors brickColor)
    {
        foreach (BrickColorMaterialMap brickColorMaterialMap in m_brickColorMaterialData)
        {
            if (brickColorMaterialMap.brickColor == brickColor)
            {
                return brickColorMaterialMap.brickMaterial;
            }
        }
        return null;
    }
}