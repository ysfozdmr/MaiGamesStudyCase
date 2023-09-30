using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.EventBehaviour;
using Fenrir.Managers;
using Fenrir.EventBehaviour.Attributes;


public class UIManager : GameActor<GameManager>
{
    public GameObject StartPanel;
    public GameObject GamePanel;
    public GameObject EndPanel;

    public override void ActorAwake()
    {
        StartPanel.SetActive(true);
        GamePanel.SetActive(false);
        EndPanel.SetActive(false);
    }

    public override void ActorStart()
    {
        GamePanelLevelTextUpdate();
        AddListeners();
    }

    public void TapToStartAction()
    {
        StartPanel.SetActive(false);
        GamePanel.SetActive(true);
        GameManager.Instance.StartLevel();
    }

    void GamePanelLevelTextUpdate()
    {
        int level = PlayerPrefs.GetInt("level");
        level++;
        GamePanel.GetComponent<GamePanel>().LevelText.text = "Level "+level;
    }

    void AddListeners()
    {
        EndPanel.GetComponent<EndPanel>().NextLevelButton.onClick.AddListener(NextLevelButtonListener);
        EndPanel.GetComponent<EndPanel>().RestartLevelButton.onClick.AddListener(RestartLevelButtonListener);
    }

    void RestartLevelButtonListener()
    {
        GameManager.Instance.RestartLevel();
    }

    void NextLevelButtonListener()
    {
        GameManager.Instance.NextLevel();
    }
    [GE(BaseGameEvents.LoseGame)]
    void LevelFailPanelOpen()
    {
        GamePanel.SetActive(false);
        EndPanel.SetActive(true);
        EndPanel.GetComponent<EndPanel>().LevelFailPanel.SetActive(true);
        EndPanel.GetComponent<EndPanel>().LevelCompletePanel.SetActive(false);
    }  
    [GE(BaseGameEvents.WinGame)]
    void LevelCompletePanelOpen()
    {
        GamePanel.SetActive(false);
        EndPanel.SetActive(true);
        EndPanel.GetComponent<EndPanel>().LevelCompletePanel.SetActive(true);
        EndPanel.GetComponent<EndPanel>().LevelFailPanel.SetActive(false);
    }
}
