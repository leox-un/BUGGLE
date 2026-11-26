using System;
using DesignPatterns.CreationalPatterns;
using UnityEngine;

public class GameManagement : Singleton<GameManagement>
{
    public GameObject looseScreen;

    void Awake()
    {
        looseScreen.SetActive(false);
    }

    public void Loose()
    {
        looseScreen.SetActive(true);
    }
}
