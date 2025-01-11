using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BrickColors m_brickColorToHit;

    public BrickColors BrickColorToHit => m_brickColorToHit;
    
    public void SetBrickColorToHit(BrickColors brickColor)
    {
        m_brickColorToHit = brickColor;
    }
}