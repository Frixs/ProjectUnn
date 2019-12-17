using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMenu : MonoBehaviour
{
    public MenuItem[] Elements;
    private int CurrentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Apply();
    }
     public void IncrementMenuItem()
    {
        CurrentIndex++;
        if (CurrentIndex > Elements.Length - 1) CurrentIndex = 0;
        Apply();

    }
    public void DecrementMenuItem()
    {
        CurrentIndex--;
        if (CurrentIndex < 0) CurrentIndex = Elements.Length - 1;
        Apply();
    }

    public void Apply()
    {
        foreach (MenuItem item in Elements)
        {
            item.Item.transform.localScale = new Vector3(1, 1);
            item.Item.transform.GetChild(0).gameObject.SetActive(false);
        }
        Elements[CurrentIndex].Item.transform.localScale = new Vector3(1.3f, 1.3f);
        Elements[CurrentIndex].Item.transform.GetChild(0).gameObject.SetActive(true);
        GameAssets.I.player.GetComponent<AE_AnimatorEvents>().CurrentArrowType = Elements[CurrentIndex].Arrow;
    }
    void Update()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            DecrementMenuItem();

        }
        else if (d < 0f)
        {
            IncrementMenuItem();
        }
    }

    [System.Serializable]
    public class MenuItem
    {
        public ArrowTypes Arrow;
        public GameObject Item;
    }
    
}
