using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    private const int MAX_SHOOTING_SLOT = 10;

    public int levelNumber;
    public List<BrickConfigData> brickConfigDatas = new();
    public List<ShooterSpawnRowData> shooterSpawnRowDatas = new();
    [Range(1,MAX_SHOOTING_SLOT)]
    public int numberToShootingSlotsToSpawn = 4;
    
    public int GetTotalNumberOfBricks()
    {
        int numOfBricks = 0;
        foreach (BrickConfigData brickConfigData in brickConfigDatas)
        {
            numOfBricks += brickConfigData.brickStrenght;
        }

        return numOfBricks;
    }
}