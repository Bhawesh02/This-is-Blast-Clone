public abstract class BrickSpawnSlot : Slot
{
    protected BrickConfigData m_currentBrickConfigData;

    public BrickConfigData CurrentBrickConfig => m_currentBrickConfigData;

    public abstract override void HandleClick();

    public abstract override void HandleDrag();
    
    private void Awake()
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
    protected void SpawnBrickOntoSlot()
    {
        if (m_isOccupied)
        {
            SlotElement slotElement = m_slotElement;
            EmptySlot();
            ConfigBrickAndOccupySlot(slotElement);
        }
        else
        {
            Brick newBrick = (Brick)Instantiate(GameConfig.Instance.brickElementData.elementPrefab);
            ConfigBrickAndOccupySlot(newBrick);
        }
    }
    private void ConfigBrickAndOccupySlot(SlotElement slotElement)
    {
        Brick brickElement = (Brick)slotElement;
        brickElement.Config(m_currentBrickConfigData, this, m_grid);
        OccupySlot(slotElement);
    }
}