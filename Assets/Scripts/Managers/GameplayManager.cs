using System;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static Action OnPlayerLose;

    private void OnEnable()
    {
        OnPlayerLose += PlayerLose;
    }
    private void OnDisable()
    {
        OnPlayerLose -= PlayerLose;
    }

    private void PlayerLose()
    {
        SceneManager.LoadScene("Ending");
    }
}
