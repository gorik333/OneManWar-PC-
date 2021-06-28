using UnityEngine;
using System.Collections;

public class FirstLvlCheckPointsMaster : MonoBehaviour
{
    [SerializeField] private Transform player;

    private FirstLvlPlayerShootMechanic _playerShootMechanic;

    private Vector2 lastCheckPointPos;
    private Vector2 respawn;

    #region playerData

    private float _oldPlayerHealth;
    private float _oldPlayerShieldDurability;

    private int _oldPlayerCurrentCommonAmmo;
    private int _oldPlayerTotalCommonAmmo;

    private int _oldPlayerCurrentStrengthenedAmmo;
    private int _oldPlayerTotalStrengthenedAmmo;

    private int _oldPlayerBalance;

    #endregion

    private static bool s_isRespawning;


    void Awake()
    {
        respawn = player.position;
        lastCheckPointPos = respawn;
        _playerShootMechanic = FindObjectOfType(typeof(FirstLvlPlayerShootMechanic)) as FirstLvlPlayerShootMechanic;
    }


    void Start()
    {
        if (FirstLvlTipsController.AllAllow)
        {
            Destroy(transform.parent.gameObject);
        }
        StartCoroutine(SaveDataDelay());
    }


    public void RespawnOnLastCheckPoint()
    {
        StopProcessesWhenRespawning();
        RestoreThePastState();
    }
    

    private void StopProcessesWhenRespawning()
    {
        _playerShootMechanic.TurnOffReloadingPanel();
        s_isRespawning = true;
    }


    public void SaveCurrentData()
    {
        SaveAmmoInfo();
        SaveHpAndShieldInfo();
        SaveBalanceInfo();
    }


    private void RestoreThePastState()
    {
        player.position = lastCheckPointPos;
        s_isRespawning = false;

        RestoreHpAndShieldInfo();
        RestoreAmmoInfo();
        RestoreBalanceInfo();
    }


    private void SaveBalanceInfo()
    {
        _oldPlayerBalance = DefaultLevelController.Balance;
    }


    private void SaveHpAndShieldInfo()
    {
        _oldPlayerHealth = player.GetComponentInParent<FirstLvlPlayerHealthController>().CurrentHealth;
        _oldPlayerShieldDurability = player.GetComponentInParent<FirstLvlPlayerShieldController>().CurrentDurability;
    }


    private void SaveAmmoInfo()
    {
        var playerShootMechanic = player.GetComponentInParent<FirstLvlPlayerShootMechanic>();

        _oldPlayerCurrentCommonAmmo = playerShootMechanic.CurrentCommonMagazineCapacity;
        _oldPlayerTotalCommonAmmo = playerShootMechanic.TotalCommonAmmoAmount;

        _oldPlayerCurrentStrengthenedAmmo = playerShootMechanic.CurrentStrengthenedMagazineCapacity;
        _oldPlayerTotalStrengthenedAmmo = playerShootMechanic.TotalStrengthenedAmmoAmount;
    }


    private void RestoreHpAndShieldInfo()
    {
        var playerHealthController = player.GetComponentInParent<FirstLvlPlayerHealthController>();
        playerHealthController.CurrentHealth = _oldPlayerHealth;
        playerHealthController.UpdateHealthBarInfo();

        var playerShieldController = player.GetComponentInParent<FirstLvlPlayerShieldController>();
        playerShieldController.CurrentDurability = _oldPlayerShieldDurability;
        playerShieldController.UpdateValuesOnScreen();
    }


    private void RestoreAmmoInfo()
    {
        var playerShootMechanic = player.GetComponentInParent<FirstLvlPlayerShootMechanic>();

        playerShootMechanic.CurrentCommonMagazineCapacity = _oldPlayerCurrentCommonAmmo;
        playerShootMechanic.TotalCommonAmmoAmount = _oldPlayerTotalCommonAmmo;

        playerShootMechanic.CurrentStrengthenedMagazineCapacity = _oldPlayerCurrentStrengthenedAmmo;
        playerShootMechanic.TotalStrengthenedAmmoAmount = _oldPlayerTotalStrengthenedAmmo;

        playerShootMechanic.UpdateValuesOnScreen();
    }


    private void RestoreBalanceInfo()
    {
        DefaultLevelController.Balance = _oldPlayerBalance;
    }


    private IEnumerator SaveDataDelay()
    {
        yield return new WaitForEndOfFrame();
        SaveCurrentData();
    }


    public Vector3 LastCheckPointPos { get => lastCheckPointPos; set => lastCheckPointPos = value; }

    public static bool IsRespawning { get => s_isRespawning; set => s_isRespawning = value; }
}
