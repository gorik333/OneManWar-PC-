using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _selectModePanel;
   

    public void PlayOnClick()
    {
        _menuPanel.SetActive(false);
        _selectModePanel.SetActive(true);
        SoundOnButtonClick();
    }


    public void OpenPurchasePanelOnClick()
    {

        SoundOnButtonClick();
    }

    public void CloseGameOnClick()
    {
        Application.Quit();
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}
