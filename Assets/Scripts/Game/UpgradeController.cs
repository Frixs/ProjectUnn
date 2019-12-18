using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public GameObject[] Buttons;
    public static UpgradeController Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        HideButtons();
    }
    public void ShowButtons()
    {
        foreach (GameObject g in Buttons)
        {
            g.SetActive(true);
        }
    }
    public void HideButtons()
    {
        foreach (GameObject g in Buttons)
        {
            g.SetActive(false);
        }
    }


}
