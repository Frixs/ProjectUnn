using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuController : MonoBehaviour
{
    [SerializeField] private GameObject RadialMenu;
     
    // Update is called once per frame
    void Update()
    {
        RadialMenu.SetActive(PlayerInput.Instance.Menu);
        
    }
}
