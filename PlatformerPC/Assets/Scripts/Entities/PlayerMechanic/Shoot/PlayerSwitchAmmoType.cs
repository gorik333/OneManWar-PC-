using System.Collections;
using UnityEngine;

public class PlayerSwitchAmmoType : MonoBehaviour
{
    [SerializeField] private GameObject _strengthenedAmmoPanel;

    private GameObject _reloadPanel;

    private PlayerShootMechanic _shootMechanic;

    private bool _isSwitching;
    private bool _isSwitchedTypeOfBullet;


    void Start()
    {
        _shootMechanic = GetComponent<PlayerShootMechanic>();
        DefaultValues();
    }


    void DefaultValues()
    {
        _strengthenedAmmoPanel.SetActive(false);
    }


    public void StartChangeTypeOfBullet(GameObject reloadPanel)
    {
        _isSwitching = true;

        _reloadPanel = reloadPanel;
        _reloadPanel.SetActive(true);

        StartCoroutine(ChangeTypeOfBulletsDelay());
    }


    private IEnumerator ChangeTypeOfBulletsDelay()
    {
        yield return new WaitForSeconds(GlobalDefVals.CHANGE_AMMO_TYPE_DELAY); 

        if (_strengthenedAmmoPanel.activeInHierarchy)
        {
            _strengthenedAmmoPanel.SetActive(false);
        }
        else
        {
            _strengthenedAmmoPanel.SetActive(true);
        }

        _isSwitchedTypeOfBullet = !_isSwitchedTypeOfBullet;
        _isSwitching = false;
        _reloadPanel.SetActive(false);
        _shootMechanic.UpdateValuesOnScreen();
    }


    public bool IsSwitching { get => _isSwitching; set => _isSwitching = value; }

    public bool IsSwitchedTypeOfBullet { get => _isSwitchedTypeOfBullet; set => _isSwitchedTypeOfBullet = value; }
}
