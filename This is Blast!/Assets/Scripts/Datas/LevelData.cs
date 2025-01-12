using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public List<BrickConfigData> brickConfigDatas = new();
    public List<ShooterSpawnRowData> shooterSpawnRowDatas = new();

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