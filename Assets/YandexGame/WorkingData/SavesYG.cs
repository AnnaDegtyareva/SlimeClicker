
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public bool[] slimeOpen;
        public int money;
        public int[] allSlimes;
        public bool FirstLaunch;
        public int world;
        public bool music;
        public bool sounds;
        public int foodPrice;

        public SavesYG()
        {
            FirstLaunch = true;
            slimeOpen = new bool[25];
            allSlimes = new int[25];
            world = 0;
            music = true;
            sounds = true;
            foodPrice = 25;
        }
    }
}
