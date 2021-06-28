using UnityEngine;

public class TotalGameStats : MonoBehaviour
{
    public static float DamageDealt;

    public static int EnemiesKilled;
    public static int SkillPointsFromCompletedLevel;
    public static int SkillPointsFromEnemies;


    public static void EarnSkillPointsFromEnemies()
    {
        if (Random.Range(0, 100) <= PlayerUpgrades.GetSkillPointChance.Value)
        {
            SkillPointsFromEnemies += 1;
        }
        EnemiesKilled++;
    }


    public static void EarnSkillPointsFromCompletedLevel(int skillPointsAmount)
    {
        SkillPointsFromCompletedLevel += skillPointsAmount;
    }


    public static void TransferToPlayerUpgrades()
    {
        PlayerUpgrades.SkillPoints.Value += SkillPointsFromCompletedLevel + SkillPointsFromEnemies;
        SaveDataController.SavePlayerSkillpoints();

        AudioController.Instance.PauseEffectsSound();
        ResetValues();
    }


    public static void ResetValues()
    {
        DamageDealt = 0;
        EnemiesKilled = 0;
        SkillPointsFromCompletedLevel = 0;
        SkillPointsFromEnemies = 0;
    }
}
