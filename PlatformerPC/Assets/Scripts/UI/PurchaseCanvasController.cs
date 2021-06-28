using UnityEngine;
using UnityEngine.UI;

public class PurchaseCanvasController : MonoBehaviour
{
    [SerializeField] private Button _ammoPanelButton;
    [SerializeField] private Button _healPanelButton;
    [SerializeField] private Button _shieldPanelButton;

    [SerializeField] private GameObject _ammoPanel;
    [SerializeField] private GameObject _healPanel;
    [SerializeField] private GameObject _shieldPanel;


    public void OpenAmmoPanelOnClick()
    {
        _ammoPanel.SetActive(true);
        _healPanel.SetActive(false);
        _shieldPanel.SetActive(false);
        _ammoPanelButton.interactable = false;
        _healPanelButton.interactable = true;
        _shieldPanelButton.interactable = true;

        SoundOnButtonClick();
    }


    public void OpenHealPanelOnClick()
    {
        _ammoPanel.SetActive(false);
        _healPanel.SetActive(true);
        _shieldPanel.SetActive(false);
        _ammoPanelButton.interactable = true;
        _healPanelButton.interactable = false;
        _shieldPanelButton.interactable = true;

        AmmunitionPanelController.Instance.HideAllInfo();
        SoundOnButtonClick();
    }


    public void OpenShieldPanelOnClick()
    {
        _ammoPanel.SetActive(false);
        _healPanel.SetActive(false);
        _shieldPanel.SetActive(true);
        _ammoPanelButton.interactable = true;
        _healPanelButton.interactable = true;
        _shieldPanelButton.interactable = false;

        AmmunitionPanelController.Instance.HideAllInfo();
        SoundOnButtonClick();
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}
