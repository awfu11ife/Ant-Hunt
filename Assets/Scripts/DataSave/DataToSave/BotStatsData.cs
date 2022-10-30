namespace SaveData
{
    [System.Serializable]
    public class BotStatsData
    {
        public float CurrentSpeed;
        public int CurrentInventoryCapacity;
        public int CurrentMaxBotsAmount;
        public int CurrentNumberOfBots;

        public BotStatsData()
        {
            CurrentSpeed = 5;
            CurrentInventoryCapacity = 1;
            CurrentMaxBotsAmount = 10;
            CurrentNumberOfBots = 0;
        }
    }
}
