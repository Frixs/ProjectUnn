using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject OptionPanel;


    void Awake()
    {
        OptionPanel.SetActive(false);
    }

    public void SwitchPanels(int panel)
    {
        TitlePanel.SetActive((panel == 0));
        OptionPanel.SetActive((panel == 1));
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnVolumeChange()
    {
        Debug.Log("Volume Changed");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
