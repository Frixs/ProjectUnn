using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private GameObject[] TutorialPanels;
    private int currentTutorialPanel;


    void Awake()
    {
        currentTutorialPanel = 0;
        SwitchPanels(0);
        ApplyTutorialPanels();
    }

    public void SwitchPanels(int panel)
    {
        TitlePanel.SetActive((panel == 0));
        OptionPanel.SetActive((panel == 1));
        TutorialPanel.SetActive((panel == 2));
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void IncrementTutorialPanels()
    {
        currentTutorialPanel = Mathf.Min(currentTutorialPanel + 1, TutorialPanels.Length - 1);
        ApplyTutorialPanels();
    }

    public void DecrementTutorialPanels()
    {
        currentTutorialPanel = Mathf.Max(currentTutorialPanel - 1, 0);
        ApplyTutorialPanels();
    }

    public void ApplyTutorialPanels()
    {
        foreach(GameObject panel in TutorialPanels)
        {
            panel.SetActive(false);
        }

        TutorialPanels[currentTutorialPanel].SetActive(true);
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
