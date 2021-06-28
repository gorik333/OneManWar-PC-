using UnityEngine;
using BayatGames.SaveGameFree;

public class SaveDataController : MonoBehaviour
{
    public static int[] ActiveBars;
    public static int CurrentLevelSaved;

    private static bool s_isFirstStart;


    void Awake()
    {
        if (!s_isFirstStart)
        {
            LoadUpgradesValues();
            LoadVolumeValue();

            if (ActiveBars == null)
            {
                ActiveBars = new int[8]; // Number of Progress Bars
            }

            s_isFirstStart = true;
        }
        LoadData();
    }


    public static void SavePlayerUpgradesData()
    {
        SaveGame.Save("Bars", ActiveBars);

        SavePlayerSkillpoints();

        SaveGame.Save("ExtraHitPoints", PlayerUpgrades.ExtraHitPoints.Value);
        SaveGame.Save("AdditionalMagazineCapacity", PlayerUpgrades.AdditionalMagazineCapacity.Value);
        SaveGame.Save("BonusGoldFromEnemies", PlayerUpgrades.BonusGoldFromEnemies.Value);
        SaveGame.Save("GetSkillPointChance", PlayerUpgrades.GetSkillPointChance.Value);

        SaveGame.Save("ExtraShieldDurability", PlayerUpgrades.ExtraShieldDurability.Value);
        SaveGame.Save("AdditionalDamage", PlayerUpgrades.AdditionalDamage.Value);

        SaveGame.Save("AdditionalFireRate", PlayerUpgrades.AdditionalFireRate.Value);
        SaveGame.Save("ReloadTimeDecrease", PlayerUpgrades.ReloadTimeDecrease.Value);
    }


    public static void SavePlayerSkillpoints()
    {
        SaveGame.Save("SkillPoints", PlayerUpgrades.SkillPoints.Value);
    }


    public static void SaveCompletedLevelData()
    {
        if (CurrentLevelSaved <= DefaultLevelController.CurrentLevel)
        {
            SaveGame.Save("CurrentDefaultLevelSaved", DefaultLevelController.CurrentLevel);
        }
    }


    public static void SaveMuteState(bool isMuted)
    {
        SaveGame.Save("IsMuted", isMuted);
    }


    public static void SaveVolumeValue(string name, float volume)
    {
        if (name.Equals("Master"))
        {
            SaveGame.Save("Master", volume);
        }
        else if (name.Equals("Music"))
        {
            SaveGame.Save("Music", volume);
        }
        else if (name.Equals("Effects"))
        {
            SaveGame.Save("Effects", volume);
        }
        else if (name.Equals("UI"))
        {
            SaveGame.Save("UI", volume);
        }
    }


    public static void ResetAttackModeData()
    {
        SaveGame.Save("SkillPoints", 0); // 0

        SaveGame.Save("Bars", new int[8]);

        SaveGame.Save("ExtraHitPoints", 0);
        SaveGame.Save("AdditionalMagazineCapacity", 0);
        SaveGame.Save("BonusGoldFromEnemies", 0);
        SaveGame.Save("GetSkillPointChance", 0);

        SaveGame.Save("ExtraShieldDurability", 0);
        SaveGame.Save("AdditionalDamage", 0);

        SaveGame.Save("AdditionalFireRate", 0);
        SaveGame.Save("ReloadTimeDecrease", 0);

        SaveGame.Save("CurrentDefaultLevelSaved", 0);

        LoadUpgradesValues();
        LoadData();
    }


    public static void LoadVolumeValue()
    {
        if (SaveGame.Exists("IsMuted"))
            SetVolume.IsMuted = SaveGame.Load<bool>("IsMuted");
        else
            SetVolume.IsMuted = false;

        if (SaveGame.Exists("Master"))
            SetVolume.MasterVolumeLevel = SaveGame.Load<float>("Master");
        else
            SetVolume.MasterVolumeLevel = 1;

        if (SaveGame.Exists("Music"))
            SetVolume.MusicVolumeLevel = SaveGame.Load<float>("Music");
        else
            SetVolume.MusicVolumeLevel = 1;

        if (SaveGame.Exists("Effects"))
            SetVolume.EffectsVolumeLevel = SaveGame.Load<float>("Effects");
        else
            SetVolume.EffectsVolumeLevel = 1;

        if (SaveGame.Exists("UI"))
            SetVolume.UIVolumeLevel = SaveGame.Load<float>("UI");
        else
            SetVolume.UIVolumeLevel = 1;

    }


    public static void LoadUpgradesValues()
    {
        if (SaveGame.Exists("Bars"))
            ActiveBars = SaveGame.Load<int[]>("Bars");

        if (SaveGame.Exists("SkillPoints"))
            PlayerUpgrades.SkillPoints.Value = SaveGame.Load<int>("SkillPoints");

        if (SaveGame.Exists("AdditionalMagazineCapacity"))
            PlayerUpgrades.AdditionalMagazineCapacity.Value = SaveGame.Load<int>("AdditionalMagazineCapacity");

        if (SaveGame.Exists("BonusGoldFromEnemies"))
            PlayerUpgrades.BonusGoldFromEnemies.Value = SaveGame.Load<int>("BonusGoldFromEnemies");

        if (SaveGame.Exists("GetSkillPointChance"))
            PlayerUpgrades.GetSkillPointChance.Value = SaveGame.Load<int>("GetSkillPointChance");

        if (SaveGame.Exists("ExtraHitPoints"))
            PlayerUpgrades.ExtraHitPoints.Value = SaveGame.Load<int>("ExtraHitPoints");

        if (SaveGame.Exists("ExtraShieldDurability"))
            PlayerUpgrades.ExtraShieldDurability.Value = SaveGame.Load<int>("ExtraShieldDurability");

        if (SaveGame.Exists("AdditionalDamage"))
            PlayerUpgrades.AdditionalDamage.Value = SaveGame.Load<int>("AdditionalDamage");

        if (SaveGame.Exists("AdditionalFireRate"))
            PlayerUpgrades.AdditionalFireRate.Value = SaveGame.Load<float>("AdditionalFireRate");

        if (SaveGame.Exists("ReloadTimeDecrease"))
            PlayerUpgrades.ReloadTimeDecrease.Value = SaveGame.Load<float>("ReloadTimeDecrease");
    }


    public static void LoadData()
    {
        if (SaveGame.Exists("CurrentDefaultLevelSaved"))
        {
            CurrentLevelSaved = SaveGame.Load<int>("CurrentDefaultLevelSaved");
        }
        else
        {
            CurrentLevelSaved = 0;
        }

        SelectDefaultLevelController.Instance.UpdateUnlockedLevels();
    }
}
