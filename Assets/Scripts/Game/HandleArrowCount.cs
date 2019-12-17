using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleArrowCount : MonoBehaviour
{
    public bool isGoal;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject arrow = collision.transform.parent.gameObject;
        if (arrow == null) return;
        if (arrow.CompareTag("Arrow"))
        {
            GameAssets.I.player.DecreaseArrow();
            if (isGoal)
            {
                GameAssets.I.player.ArrowsHit++;
            }
            GameAssets.I.player.ArrowsShot++;
            GameAssets.I.player.CalculateAccuracy();
        }
    }
}
