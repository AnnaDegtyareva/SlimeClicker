using System.Collections;
using System.Collections.Generic;

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

        public int[] count;

        public bool[] slimeOpen = new bool[3];

        public SavesYG()
        {
            count = new int[3];
        }
    }
}
