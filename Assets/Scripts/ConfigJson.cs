
[System.Serializable]
public class ConfigJson
{
    public Config configGO;
    [System.Serializable]
    public struct Config
    {
        public Hero hero;
        public Zombie zombie;
        public Misc misc;
    }
    [System.Serializable]
    public struct Hero
    {
        [System.Serializable]
        public struct DamageArea
        {
            public int radius, angle;
        }
        [System.Serializable]
        public struct ViewArea
        {
            public int radius, angle;
        }

        public int health,
         velocity, turnVelocity,
         reloadTime, damage, ammo;
        public DamageArea damageArea;
        public ViewArea viewArea;
    }
    [System.Serializable]
    public struct Zombie
    {
        [System.Serializable]
        public struct DamageAreaZombie
        {
            public int[] radius;
            public int angle;
        }
        [System.Serializable]
        public struct ViewAreaZombie
        {
            public int[] radius;
            public int angle;
        }
        public int[] health,
         activeVelocity, passiveVelocity, turnVelocity,
         reloadTime, damage;
        public int stepsToChangeDirection;
        public int stepsToCoolDown;
        public DamageAreaZombie damageArea;
        public ViewAreaZombie viewArea;
    }
    [System.Serializable]
    public struct Misc
    {
        public int ammoBox;
    }


}