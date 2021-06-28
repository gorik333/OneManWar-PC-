using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private Transform _settingsPanel;


    void Start()
    {
        _settingsPanel.localScale = Vector2.zero;   
    }

    public void Open()
    {
        _settingsPanel.LeanScale(Vector2.one, 0.65f);
    }
}
