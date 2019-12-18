using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GameAssets : MonoBehaviour
    {

        public static GameAssets I;
        public Transform damagePopupPrefab;
        public PlayerController player;
        public GameObject DebuffPrefab;
        private void Awake()
        {
            I = this;

        }
    }
