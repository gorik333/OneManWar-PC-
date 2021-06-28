using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameThemeController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MenuDelay());
    }


    private IEnumerator MenuDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
}
