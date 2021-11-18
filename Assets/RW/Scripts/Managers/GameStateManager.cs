using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    [HideInInspector]
    public int sheepSaved;
    [HideInInspector]
    public int sheepDropped;
    public int sheepDroppedBeforeGameOver;
    public SheepSpawner sheepSpawner;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //Debug.Log("Sheeps dropped: " + sheepDropped);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped == sheepDroppedBeforeGameOver)
        {
            GameOver();
        }
    }

    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();

    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();

    }

}
