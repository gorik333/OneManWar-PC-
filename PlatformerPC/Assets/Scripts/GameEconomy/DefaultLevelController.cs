using UnityEngine;
using TMPro;

public class DefaultLevelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _showCurrentBalance;

    [SerializeField] private int _startGold;
    [SerializeField] private int _enemyReward;
    [SerializeField] private int _chestGoldReward;
    [SerializeField] private int _currentLevel;

    [SerializeField] private int _skillPointsForWin;

    public static int SkillPointsForWin;
    public static int CurrentLevel;

    private static int s_levelBalance;
    private static int s_enemyGoldReward;
    private static int s_chestGoldReward;

    public static bool IsGameEnd;
        

    void Start()
    {
        AudioController.Instance.PlayEffectsSound();
        DefaultValues();
    }


    private void DefaultValues()
    {
        IsGameEnd = false;

        s_levelBalance = _startGold;
        s_enemyGoldReward = _enemyReward;
        s_chestGoldReward = _chestGoldReward;

        CurrentLevel = _currentLevel;
        SkillPointsForWin = _skillPointsForWin;
    }


    void FixedUpdate()
    {
        _showCurrentBalance.text = s_levelBalance.ToString();
    }


    public static void EarnMoneyFromChest()
    {
        if (s_levelBalance + s_chestGoldReward <= 99999)
        {
            s_levelBalance += s_chestGoldReward;
        }
        else
        {
            s_levelBalance = 99999;
        }
    }


    public static void EarnMoneyFromEnemy()
    {
        if (s_levelBalance + s_enemyGoldReward <= 99999)
        {
            s_levelBalance += s_enemyGoldReward;
        }
        else
        {
            s_levelBalance = 99999;
        }
    }


    public static bool BuySomething(int value)
    {
        if (s_levelBalance >= value)
        {
            s_levelBalance -= value;
            return true;
        }
        else
        {
            return false;
        }
    }


    public static int EnemyGoldReward { get => s_enemyGoldReward; set => s_enemyGoldReward = value; }

    public static int Balance { get => s_levelBalance; set => s_levelBalance = value; }
}
