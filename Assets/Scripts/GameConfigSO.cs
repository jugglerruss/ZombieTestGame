using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigSO : ScriptableObject
{
    [Header ("Hero settings")]
    [SerializeField] private int _healthHero;
    [SerializeField] private int _velocityHero;
    [SerializeField] private int _turnVelocityHero;
    [SerializeField] private int _reloadTimeHero;
    [SerializeField] private int _damageHero;
    [SerializeField] private int _ammoHero;
    [SerializeField] private int _damageAreaRadiousHero;
    [SerializeField] private int _damageAreaAngleHero;
    [SerializeField] private int _viewAreaRadiusHero;
    [SerializeField] private int _viewAreaAngleHero;
    [Header("Zombie settings")]
    [SerializeField] private int _minHealthZombie;
    [SerializeField] private int _maxHealthZombie;
    [SerializeField] private int _minActiveVelocityZombie;
    [SerializeField] private int _maxActiveVelocityZombie;
    [SerializeField] private int _minPassiveVelocityZombie;
    [SerializeField] private int _maxPassiveVelocityZombie;
    [SerializeField] private int _minTurnVelocityZombie;
    [SerializeField] private int _maxTurnVelocityZombie;
    [SerializeField] private int _minReloadTimeZombie;
    [SerializeField] private int _maxReloadTimeZombie;
    [SerializeField] private int _stepsToChangeDirectionZombie;
    [SerializeField] private int _stepsToCoolDownZombie;
    [SerializeField] private int _minDamageZombie;
    [SerializeField] private int _maxDamageZombie;
    [SerializeField] private int _minDamageAreaRadiusZombie;
    [SerializeField] private int _maxDamageAreaRadiusZombie;
    [SerializeField] private int _damageAreaAngleZombie;
    [SerializeField] private int _minViewAreaRadiusZombie;
    [SerializeField] private int _maxViewAreaRadiusZombie;
    [SerializeField] private int _viewAreaAngleZombie;
    [Header("Ammo settings")]
    [SerializeField] private int _ammoBox;

    public int HealthHero { get => _healthHero;  }
    public int VelocityHero { get => _velocityHero;  }
    public int TurnVelocityHero { get => _turnVelocityHero;}
    public int ReloadTimeHero { get => _reloadTimeHero;  }
    public int DamageHero { get => _damageHero;  }
    public int AmmoHero { get => _ammoHero;  }
    public int DamageAreaRadiousHero { get => _damageAreaRadiousHero;  }
    public int DamageAreaAngleHero { get => _damageAreaAngleHero;  }
    public int ViewAreaRadiusHero { get => _viewAreaRadiusHero;  }
    public int ViewAreaAngleHero { get => _viewAreaAngleHero;}
    public int MinHealthZombie { get => _minHealthZombie; }
    public int MaxHealthZombie { get => _maxHealthZombie;  }
    public int MinActiveVelocityZombie { get => _minActiveVelocityZombie;}
    public int MaxActiveVelocityZombie { get => _maxActiveVelocityZombie;  }
    public int MinPassiveVelocityZombie { get => _minPassiveVelocityZombie;  }
    public int MaxPassiveVelocityZombie { get => _maxPassiveVelocityZombie;  }
    public int MinTurnVelocityZombie { get => _minTurnVelocityZombie; }
    public int MaxTurnVelocityZombie { get => _maxTurnVelocityZombie;  }
    public int MinReloadTimeZombie { get => _minReloadTimeZombie;}
    public int MaxReloadTimeZombie { get => _maxReloadTimeZombie;  }
    public int StepsToChangeDirectionZombie { get => _stepsToChangeDirectionZombie; }
    public int StepsToCoolDownZombie { get => _stepsToCoolDownZombie; }
    public int MinDamageZombie { get => _minDamageZombie;  }
    public int MaxDamageZombie { get => _maxDamageZombie; }
    public int MinDamageAreaRadiusZombie { get => _minDamageAreaRadiusZombie;  }
    public int MaxDamageAreaRadiusZombie { get => _maxDamageAreaRadiusZombie;  }
    public int DamageAreaAngleZombie { get => _damageAreaAngleZombie; }
    public int MinViewAreaRadiusZombie { get => _minViewAreaRadiusZombie; }
    public int MaxViewAreaRadiusZombie { get => _maxViewAreaRadiusZombie;  }
    public int ViewAreaAngleZombie { get => _viewAreaAngleZombie;  }
    public int AmmoBox { get => _ammoBox; }
}
