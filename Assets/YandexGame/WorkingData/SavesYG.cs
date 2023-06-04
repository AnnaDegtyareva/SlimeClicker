
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

        public SavesYG()
        {
            FirstLaunch = true;
            slimeOpen = new bool[8];
            allSlimes = new int[8];
            world = 0;
        }
    }
}
