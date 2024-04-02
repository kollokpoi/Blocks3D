using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController init;

    [Header("Анимации")]
    /// <summary>
    /// Компонент анимции
    /// </summary>
    public Animation UIAnimation;
    public AnimationClip startGame;
    public AnimationClip endGame;
    /// <summary>
    /// Анимцая настроек
    /// </summary>
    public AnimationClip animSettings;
    public AnimationClip animBackSettings;
    /// <summary>
    /// Анимация инструкции
    /// </summary>
    public AnimationClip animHelp;
    public AnimationClip animBackHelp;

    [Header("Переменные")]
    public UIState uiState = UIState.Menu;

    [Header("Таймер")]
    public Text tTimer;
    public Image iTimer;
    public float MaxTimer = 60;
    public float Timer; // 58
    private float OldTimer; // 59

    [Header("Текстовые элементы")]
    public Text tMoney;
    public Text tScore;
    public Text tName;

    /// <summary>
    /// Состояния игры
    /// </summary>
    public enum UIState {
        Menu,
        StartGame,
        Settings,
        Help
    }
    void Start()
    {
        tScore.text = "Лучший " + PlayerPrefs.GetInt("Score");
        Timer = MaxTimer;
        OldTimer = MaxTimer;
        init = this;
    }
    void Update()
    {
        if (GameController.init.Game && Timer > 0) {
            Timer -= Time.deltaTime;
            tTimer.text = ((int)Timer).ToString();
            iTimer.fillAmount = Timer / MaxTimer;

            if (OldTimer != (int)Timer) {
                if (Level.init.CheckEndLevel())
                    GameController.init.CreateLevel();

                OldTimer = (int)Timer;
            }
        }
    }

    /// <summary>
    /// Старт игры
    /// </summary>
    public void OnStartGame() {
        if (uiState == UIState.Menu) {
            UIAnimation.Play(startGame.name);
            uiState = UIState.StartGame;
            GameController.init.CreateLevel();

            new Thread(() => { Thread.Sleep(100); GameController.init.Game = true; }).Start();
        }
    }

    public void AddTimer(int seconds)
    {
        Timer += seconds;
        if (Timer > MaxTimer)
        {
            Timer = MaxTimer;
        }
    }

    /// <summary>
    /// Открытие настроек
    /// </summary>
    public void OnSettings() {
        if (uiState != UIState.Settings)
        {
            UIAnimation.Play(animSettings.name);
            uiState = UIState.Settings;
        }
        else if (uiState == UIState.Settings) {
            UIAnimation.Play(animBackSettings.name);
            uiState = UIState.Menu;
        }
    }
    /// <summary>
    /// Открытие инструкции
    /// </summary>
    public void OnHelp() {
        if (uiState != UIState.Help)
        {
            UIAnimation.Play(animHelp.name);
            uiState = UIState.Help;
        } else if (uiState == UIState.Help) {
            UIAnimation.Play(animBackHelp.name);
            uiState = UIState.Settings;
        }
    }
    public void EndGame()
    {
        int maxScore = PlayerPrefs.GetInt("Score");
        if (maxScore< GameController.init.GetILevel)
        {
            PlayerPrefs.SetInt("Score", GameController.init.GetILevel);

        }
        tScore.text = "Очки: " + GameController.init.GetILevel;
        tName.text = "Конец!";
        UIAnimation.Play(endGame.name);

        Invoke("EndGameTimer", 5);
    }
    public void EndGameTimer()=>
         SceneManager.LoadScene(0);
}
