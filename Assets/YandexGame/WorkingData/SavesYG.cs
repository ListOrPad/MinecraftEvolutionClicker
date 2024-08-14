
using System.Collections.Generic;
using System.Numerics;

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

        // Ваши сохранения

        //basic
        public int previousRecord;
        public BigInteger CounterValue = 0;
        public BigInteger IncomePerSecond = 0;
        public BigInteger ClickPower = 1;

        //evolution
        public int level = 0;
        public float expBarValue = 0;
        public float expBarMaxValue = 100;

        //hiding objects
        public bool[] upgradeBought = new bool[16];
        public List<Upgrade> upgrades = new List<Upgrade>( new Upgrade[16] );

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            for (int i = 0; i < 16; i++)
            {
                upgrades[i] = new Upgrade(i);
            }
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            //openLevels[1] = true;
        }
    }
}
