using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static PanelController Instance;

    public List<PanelHolder> Panels;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CloseAllPanels();
    }
    
    public void CloseAllPanels()
    {
        foreach (PanelHolder p in Panels)
        {
            p.SetCurrent(false);
        }
    }
    public GameObject  ShowPanel (string name)
    {
        CloseAllPanels();
        PanelHolder pa = Panels.Find(p => p.name == name);
        pa.SetCurrent(true);
        return pa.GO;
    }


    [System.Serializable]
    public class PanelHolder
    {
        public string name;
        public GameObject GO;
        public bool isCurrent;
        public void SetCurrent(bool curr)
        {
            isCurrent = curr;
            GO.SetActive(curr);
        }
    }
    
}
