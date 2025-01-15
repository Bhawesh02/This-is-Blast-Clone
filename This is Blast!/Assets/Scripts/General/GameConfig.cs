using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class GameConfig : GenericConfig<GameConfig>
{
    [Serializable]
    private struct BrickColorMaterialMap
    {
        public BrickColors brickColor;
        public Material brickMaterial;
    }

    [Header("Level")] 
    public Vector2 refrenceResolution;
    public List<LevelData> levelDatas;
    public Transform shooterShootingSlotPrefab;
    public float levelCompletedScreenShowDelay = 2f;
    
    [Header("Brick")]
    [SerializeField] private List<BrickColorMaterialMap> m_brickColorMaterialData;
    public BrickElementData brickElementData;
    
    [Range(0, 1f)]
    public float brickScaleUpDuration;
    public Ease brickScaleUpEase = Ease.InBack;

    [Range(0, 0.5f)]
    public float brickScaleDownDuration;
    public Ease brickScaleDownEase = Ease.Linear;
    
    [Range(0, 0.1f)]
    public float brickMoveDownDuration;
    public Ease brickMoveDownEase = Ease.Linear;
    public HapticType brickDestroyedHaptic;
    
    [Header("Shooter")]
    public ShooterElementData shooterElementData;
    [Range(0, 1f)]
    public float shooterScaleUpDuration = 0.5f;
    public Ease shooterScaleUpEase = Ease.InBack;
    public float shooterMoveSpeed = 5f;
    public Ease shooterMoveEase = Ease.Linear;
    public float shooterWaitingStateYIncrement = 0.2f;
    [Range(0, 1f)]
    public float shooterFireDelay = 0.1f;
    
    [Header("Shooter")]
    public float projectTravleSpeed = 6f;
    public Projectile projectilePrefab;
    public ParticleSystem projectileHitParticle;

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