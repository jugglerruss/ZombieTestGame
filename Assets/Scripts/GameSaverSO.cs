using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class GameSaverSO : ScriptableObject
{
    [SerializeField] private GameConfigSO _configSO;
    private bool IsDirectoryExist(string path)
    {
        return Directory.Exists(Application.dataPath + path);
    }
    private bool IsFileExist(string path)
    {
        return File.Exists(Application.dataPath + path);
    }
    private void CreateDirectory(string path)
    {
        Directory.CreateDirectory(Application.dataPath + path);
    }
    public void Save()
    {
        SaveData(_configSO, "config", "config.json");
    }
    public void Load()
    {
        LoadData(new ConfigJson(), "config", "config.json");
    }
    private void SaveData(object obj,string folder,string fileName)
    {
        folder = "/" + folder;
        fileName = "/" + fileName;
        if (!IsDirectoryExist(folder))
        {
            CreateDirectory( folder);
        }
        if (!IsFileExist(folder + fileName))
        {
            SaveByJSON(obj, folder, fileName);
        }
    }
    private void LoadData(object obj, string folder, string fileName)
    {
        folder = "/" + folder;
        fileName = "/" + fileName;
        if (!IsDirectoryExist(folder))
            return;
        if (IsFileExist(folder + fileName))
        {
            LoadByJSON(obj, folder, fileName);
        }
    }

    private void SaveBySerialize(object obj, string folder, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + folder + fileName);
        var json = JsonUtility.ToJson(obj);
        bf.Serialize(file, json);
        file.Close();
    }
    private void LoadBySerialize(object obj, string folder, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + folder + fileName, FileMode.Open);
        JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), obj);
        file.Close();
    }
    private void SaveByJSON(object obj, string folder, string fileName)
    {
        ConfigJson.Config save = CreateSave();
        string jsonString = JsonUtility.ToJson(save);
        Debug.Log(save.hero);
        Debug.Log(jsonString);
        StreamWriter sw = new StreamWriter(Application.dataPath + folder + fileName);
        sw.Write(jsonString);
        sw.Close();
    }
    private void LoadByJSON(object obj, string folder, string fileName)
    {
        StreamReader sr = new StreamReader(Application.dataPath + folder + fileName);
        string jsonString = sr.ReadToEnd();
        sr.Close();
        ConfigJson.Config save = JsonUtility.FromJson<ConfigJson.Config>(jsonString);
        _configSO.ImportSave(save);
    }
    private ConfigJson.Config CreateSave()
    {
        ConfigJson configJsonGO = new ConfigJson();
        configJsonGO.configGO.hero = new ConfigJson.Hero
        {
            health = _configSO.HealthHero,
            velocity = _configSO.VelocityHero,
            turnVelocity = _configSO.TurnVelocityHero,
            reloadTime = _configSO.ReloadTimeHero,
            damage = _configSO.DamageHero,
            ammo = _configSO.AmmoHero,
            damageArea = new ConfigJson.Hero.DamageArea { angle = _configSO.DamageAreaAngleHero, radius = _configSO.DamageAreaRadiousHero },
            viewArea = new ConfigJson.Hero.ViewArea { angle = _configSO.ViewAreaAngleHero, radius = _configSO.ViewAreaAngleHero }
        };
        configJsonGO.configGO.zombie = new ConfigJson.Zombie
        {
            health = new int[2] { _configSO.MinHealthZombie, _configSO.MaxHealthZombie },
            activeVelocity = new int[2] { _configSO.MinActiveVelocityZombie, _configSO.MaxActiveVelocityZombie },
            passiveVelocity = new int[2] { _configSO.MinPassiveVelocityZombie, _configSO.MaxPassiveVelocityZombie },
            turnVelocity = new int[2] { _configSO.MinTurnVelocityZombie, _configSO.MaxTurnVelocityZombie },
            reloadTime = new int[2] { _configSO.MinReloadTimeZombie, _configSO.MaxReloadTimeZombie },
            stepsToChangeDirection = _configSO.StepsToChangeDirectionZombie,
            stepsToCoolDown = _configSO.StepsToCoolDownZombie,
            damage = new int[2] { _configSO.MinDamageZombie, _configSO.MaxDamageZombie },
            damageArea = new ConfigJson.Zombie.DamageAreaZombie { 
                angle = _configSO.DamageAreaAngleZombie, radius = new int[2] { _configSO.MinDamageAreaRadiusZombie, _configSO.MaxDamageAreaRadiusZombie} 
            },
            viewArea = new ConfigJson.Zombie.ViewAreaZombie
            { 
                angle = _configSO.ViewAreaAngleZombie, radius = new int[2] { _configSO.MinViewAreaRadiusZombie, _configSO.MaxViewAreaRadiusZombie } 
            }            
        };
        configJsonGO.configGO.misc = new ConfigJson.Misc { ammoBox = _configSO.AmmoBox };
        return configJsonGO.configGO;
    }

}
