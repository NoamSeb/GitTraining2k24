using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Timer : MonoBehaviour
{
    float timer = 0;

    TextMeshProUGUI tmp;

    private void Start()
    {
        timer = 0;
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        var ts = TimeSpan.FromSeconds(timer);
        tmp.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }
}
