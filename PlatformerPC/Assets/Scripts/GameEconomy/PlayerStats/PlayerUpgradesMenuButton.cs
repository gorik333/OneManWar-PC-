using UnityEngine;

public class PlayerUpgradesMenuButton : MonoBehaviour
{
    public void ExitToMenuOnClick()
    {
        AudioController.Instance.PlayButtonSound();
        StartCoroutine(LevelLoader.Instance.AnimationDelayToMenu());
    }
}
