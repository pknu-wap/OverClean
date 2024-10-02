using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenEvent : MonoBehaviour
{
    public void SceneMove()
    {
        SceneManager.LoadScene("GameLobby");
    }
}
