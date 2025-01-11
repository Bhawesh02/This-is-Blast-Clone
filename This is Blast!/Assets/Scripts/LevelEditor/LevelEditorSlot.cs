public class LevelEditorSlot : Slot
{
    private BrickConfigData m_currentBrickConfigData;

    public BrickConfigData CurrentBrickConfig => m_currentBrickConfigData;


    private void Start()
    {
        m_currentBrickConfigData = new BrickConfigData()
        {
            slotCoord = Coord
        };
    }

    public void InitBrickData(BrickConfigData brickConfigData)
    {
        m_currentBrickConfigData = brickConfigData;
        SpawnBrickOntoSlot();
    }
    
    public override void HandleClick()
    {
        UpdateCurrentBrickConfig();
    }
    
    public override void HandleDrag()
    {
        UpdateCurrentBrickConfig();
    }

    private void UpdateCurrentBrickConfig()
    {
        BrickColors brickColor = LevelEditor.Instance.brickColorToApply;
        int brickStrength = LevelEditor.Instance.brickStrength;
        m_currentBrickConfigData.brickColor = brickColor;
        m_currentBrickConfigData.brickStrenght = brickStrength;
        SpawnBrickOntoSlot();
    }

    private void SpawnBrickOntoSlot()
    {
        if (m_isOccupied)
        {
            OccupySlot(m_slotElement);
        }
        else
        {
            Brick newBrick = (Brick)Instantiate(GameConfig.Instance.brickElementData.elementPrefab);
            OccupySlot(newBrick);
        }
    }

    public override void OccupySlot(SlotElement slotElement)
    {
        EmptySlot();
        base.OccupySlot(slotElement);
        Brick brickElement = (Brick)slotElement;
        brickElement.Config(m_currentBrickConfigData, this);
        brickElement.SetCanHandleInput(false);
    }
}