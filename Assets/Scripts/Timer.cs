using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Timer : MonoBehaviour
{
    public float timer = 0;

    TextMeshProUGUI tmp;

    public bool finJeu = false;

    public bool dansTriggerFinJeu = false;

    [SerializeField] LeaderBoard lb;

    bool IsActivate = false;

    private void Start()
    {
        timer = 0;
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
    }

    

    void Update()
    {
        if (!finJeu)
        {
            timer += Time.deltaTime;
            var ts = TimeSpan.FromSeconds(timer);
            tmp.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        } else if (lb != null)
        {
            lb.timerInt = (int)timer;
            lb.timerString = tmp.text;
            if (!IsActivate && dansTriggerFinJeu)
            {
                lb.AffichageLeaderBoard();
                IsActivate = true;
            }
        }
    }
}
