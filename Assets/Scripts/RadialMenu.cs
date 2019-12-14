using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public List<MenuButton> Buttons = new List<MenuButton>();
    private Vector2 MousePosition;
    private Vector2 FromVector2M = new Vector2(0.5f, 1f);
    private Vector2 CircleCenter = new Vector2(0.5f, 0.5f);
    private Vector2 ToVector2M;

    public int NumOfMenuItems;
    public int CurrentMenuItem;
    private int OldMenuItem;

    private void Start()
    {
        NumOfMenuItems = Buttons.Count;
        foreach(MenuButton button in Buttons)
        {
            button.Image.color = button.NormalColor;
        }
        CurrentMenuItem = 0;
        OldMenuItem = 0;
    }
    private void Update()
    {
        GetCurrentMenuItem();
        if ( PlayerInput.Instance.MenuRelease)
        {
            MenuItemPressed();
        }
    }

    private void GetCurrentMenuItem()
    {
        MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        ToVector2M = new Vector2(MousePosition.x / Screen.width, MousePosition.y / Screen.height);
        float angle = (Mathf.Atan2(FromVector2M.y - CircleCenter.y, FromVector2M.x - CircleCenter.x) - Mathf.Atan2(ToVector2M.y - CircleCenter.y, ToVector2M.x - CircleCenter.x)) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        
        CurrentMenuItem = (int)(angle / (360 / NumOfMenuItems));
       // Debug.Log(angle + "; " + CurrentMenuItem);
        if (CurrentMenuItem != OldMenuItem)
        {
            //Buttons[OldMenuItem].Image.color = Buttons[OldMenuItem].NormalColor;
            Buttons[OldMenuItem].Image.transform.localScale = new Vector3(2, 2);
            OldMenuItem = CurrentMenuItem;
            Buttons[CurrentMenuItem].Image.transform.localScale = new Vector3(2.25f, 2.25f);
            //Buttons[CurrentMenuItem].Image.color = Buttons[CurrentMenuItem].HighlightedColor;
        }
    
    }
    public void MenuItemPressed()
    {
        GameObject.Find("Player").GetComponent<AE_AnimatorEvents>().CurrentArrowType = Buttons[CurrentMenuItem].Arrow;
    }
    [System.Serializable]
    public class MenuButton
    {
        public ArrowTypes Arrow;
        public Image Image;
        public TextMeshProUGUI Text;
        public Color NormalColor = Color.white;
        public Color HighlightedColor = Color.grey;
    }

}
