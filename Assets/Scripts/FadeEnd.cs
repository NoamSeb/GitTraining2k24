using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEnd : MonoBehaviour
{
    [SerializeField] ChangeScene changementScene;

    [SerializeField] bool PlayGame;
    [SerializeField] bool QuitGame;

    [SerializeField] Image debut;
    Color transparence;

    [SerializeField] float timer = 3f;
    float currentTimer;

    [SerializeField] AnimationCurve fade;

    bool startGame;

    private void Start()
    {
        startGame = false;
        transparence = debut.color;
        transparence.a = 0f;
        debut.color = transparence;

        currentTimer = timer;
    }


    public void Play()
    {
        startGame = true;
    }


    private void Update()
    {
        if (startGame)
        {
            currentTimer -= Time.deltaTime;

            float pourcentageTransparence = (timer - currentTimer) / timer;



            transparence.a = fade.Evaluate(pourcentageTransparence);
            debut.color = transparence;

            if (pourcentageTransparence >= 1)
            {
                if (!PlayGame || !QuitGame)
                {
                    if (changementScene != null && PlayGame)
                    {
                        changementScene.ChangeSceneGame();
                    }
                    else if (changementScene != null && QuitGame)
                    {
                        changementScene.Quit();
                    }
                }
            }
        }
    }
}
