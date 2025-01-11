using System;
using System.Collections.Generic;
using DG.Tweening;
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
    public Projectile projectilePrefab;
    
    [Header("Brick Tween")]
    
    [Range(0, 0.1f)]
    public float brickScaleDownDuration;
    public Ease brickScaleDownEase;
    
    [Range(0, 0.1f)]
    public float brickMoveDownDuration;
    public Ease brickMoveDownEase;
    
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