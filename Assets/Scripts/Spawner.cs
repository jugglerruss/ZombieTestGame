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
    private List<Zombie> _zombies;
    private List<AmmoBox> _ammoBoxes;
    private bool _isGameStop;
    private void Start()
    {
        SpawnHero();
        _zombies = new List<Zombie>();
        _ammoBoxes = new List<AmmoBox>();
    }
    private void Update()
    {
        if (_isGameStop)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            SpawnZombie();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SpawnAmmoBox();
        }
    }
    public void Restart()
    {
        _isGameStop = false;
        SpawnHero();
        foreach (var zombie in _zombies)
        {
            if(zombie != null)
                Destroy(zombie.gameObject);
        }
        foreach (var box in _ammoBoxes)
        {
            if (box != null)
                Destroy(box.gameObject);
        }
        _UI.HideRestart();
    }
    private void StopGame()
    {
        _isGameStop = true;
        _UI.ShowRestart();
    }
    private void SpawnAmmoBox()
    {
        var spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0;
        var ammoBox = Instantiate(_ammoBoxPrefab, spawnPosition, Quaternion.identity);
        ammoBox.transform.SetParent(transform);
        ammoBox.Init(_config.AmmoBox);
        _ammoBoxes.Add(ammoBox);
    }
    private void SpawnHero()
    {
        _hero = Instantiate(_heroPrefab, transform);
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
        _hero.OnDead += StopGame;
        _hero.Initialize(configHero, _config.AmmoHero);
    }
    private void SpawnZombie()
    {
        var spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0;
        _zombie = Instantiate(_zombiePrefab, spawnPosition, Quaternion.identity);
        _zombie.transform.SetParent(transform);
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
        _zombies.Add(_zombie);
    }
}
