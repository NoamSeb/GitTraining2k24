using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public static bool GameIsPaused = false;
   public GameObject pauseMenuUI;

    //[SerializeField] Timer timer;
    [SerializeField] PlayerController player;

    [SerializeField] Animator buttonResume;
    [SerializeField] Animator buttonMenu;
    [SerializeField] Animator buttonQuit;

    //void FixedUpdate()
    //{
    //     if (player.isPause)
    //     {
    //         print("ah ke koukou");
    //         Pause();
    //     }
    //     else if (!player.isPause)
    //     {
    //         print("kakou kakou");
    //         Resume();
    //     }
    //}

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        //buttonResume.updateMode = AnimatorUpdateMode.UnscaledTime;
        //buttonMenu.updateMode = AnimatorUpdateMode.UnscaledTime;
        //buttonQuit.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void Resume()
   {
      pauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      //GameIsPaused = false;
   }

   public void Pause()
   {
      pauseMenuUI.SetActive(true);
      Time.timeScale = 0f;
      //GameIsPaused = true;
   }

   public void LoadMenu()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene("Menu");
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
