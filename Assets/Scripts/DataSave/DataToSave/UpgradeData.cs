namespace SaveData
{
    [System.Serializable]
    public class UpgradeData
    {
        public int Price;
        public int Level;

        public UpgradeData()
        {
            Price = 0;
            Level = 1;
        }
    }
}
