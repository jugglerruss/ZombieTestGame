using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameConfigSO _config;
    [SerializeField] private Hero _heroPrefab;
    [SerializeField] private Zombie _zombiePrefab;
    [SerializeField] private AmmoBox _ammoBoxPrefab;
    [SerializeField] private UI _UI;

    private Hero _hero;
    private Zombie _zombie;
    private void Start()
    {
        SpawnHero();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnZombie();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SpawnAmmoBox();
        }
    }
    private void SpawnAmmoBox()
    {
        var spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0;
        var ammoBox = Instantiate(_ammoBoxPrefab, spawnPosition, Quaternion.identity);
        ammoBox.Init(_config.AmmoBox);
    }
    private void SpawnHero()
    {
        _hero = Instantiate(_heroPrefab);
        var configHero = new CharacterData
        {
            Health = _config.HealthHero,
            Velocity = _config.VelocityHero,
            TurnVelocity = _config.TurnVelocityHero,
            ReloadTime = _config.ReloadTimeHero,
            Damage = _config.DamageHero,
            DamageAreaRadious = _config.DamageAreaRadiousHero,
            DamageAreaAngle = _config.DamageAreaAngleHero,
            ViewAreaRadius = _config.ViewAreaRadiusHero,
            ViewAreaAngle = _config.ViewAreaAngleHero
        };
        _hero.OnHPChange += _UI.SetHP;
        _hero.OnAmmoChange += _UI.SetAmmo;
        _hero.Initialize(configHero, _config.AmmoHero);
    }
    private void SpawnZombie()
    {
        var spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0;
        _zombie = Instantiate(_zombiePrefab, spawnPosition, Quaternion.identity);
        var configZombie = new CharacterData
        {
            Health = Random.Range(_config.MinHealthZombie, _config.MaxHealthZombie),
            Velocity = Random.Range(_config.MinActiveVelocityZombie, _config.MaxActiveVelocityZombie),
            TurnVelocity = Random.Range(_config.MinTurnVelocityZombie, _config.MaxTurnVelocityZombie),
            ReloadTime = Random.Range(_config.MinReloadTimeZombie, _config.MaxReloadTimeZombie),
            Damage = Random.Range(_config.MinDamageZombie, _config.MaxDamageZombie),
            DamageAreaRadious = Random.Range(_config.MinDamageAreaRadiusZombie, _config.MaxDamageAreaRadiusZombie),
            DamageAreaAngle = _config.DamageAreaAngleZombie,
            ViewAreaRadius = Random.Range(_config.MinViewAreaRadiusZombie, _config.MaxViewAreaRadiusZombie),
            ViewAreaAngle = _config.ViewAreaAngleZombie
        };
        _zombie.Initialize(configZombie, _config.StepsToChangeDirectionZombie, _config.StepsToCoolDownZombie, Random.Range(_config.MinPassiveVelocityZombie, _config.MaxPassiveVelocityZombie));
    }
}
