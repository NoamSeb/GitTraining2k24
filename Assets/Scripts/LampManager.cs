using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
   private static LampManager m_Instance;

   public static LampManager Instance
   {
      get
      {
         if (m_Instance == null)
         {
            m_Instance = new LampManager();
         }

         return m_Instance;
      }
   }

   public event Action UseLampEvent;

   [SerializeField] public int maxLamps { get; set; } = 10;
   public void UseLamp()
   {
      maxLamps--;
      if (UseLampEvent != null)
      {
         UseLampEvent();
      }
   }
}
