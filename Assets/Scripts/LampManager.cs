using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LampManager : MonoBehaviour
{
   [SerializeField] public int maxLamps;
   [SerializeField] private TextMeshProUGUI lampCountText;

   void Start()
   {
      lampCountText.text = maxLamps.ToString();
   }
}
