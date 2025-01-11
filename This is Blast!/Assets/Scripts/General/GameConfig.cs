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
    
    
    
    [Header("Brick")]
    public BrickElementData brickElementData;
    
    [Range(0, 0.1f)]
    public float brickScaleDownDuration;
    public Ease brickScaleDownEase;
    
    [Range(0, 0.1f)]
    public float brickMoveDownDuration;
    public Ease brickMoveDownEase;
    
    [Header("Shooter")]
    public ShooterElementData shooterElementData;
    public float shooterMoveSpeed = 5f;
    public Ease shooterMoveEase;
    public float shooterWaitingStateYIncrement = 0.2f;

    public Projectile projectilePrefab;

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