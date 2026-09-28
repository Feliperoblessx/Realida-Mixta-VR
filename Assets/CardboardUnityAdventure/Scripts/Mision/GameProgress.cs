using UnityEngine;
using System;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    [HideInInspector] public int toysFound = 0;
    [HideInInspector] public int toysRequired = 3;
    [HideInInspector] public bool missionAccepted = false;

    public event Action OnToyCountChanged;
    public event Action OnMissionAccepted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Llamar al empezar cada nivel (Tutorial = 3, Nivel1 = 6)
    public void StartLevel(int requiredToys)
    {
        toysFound = 0;
        toysRequired = requiredToys;
        missionAccepted = false;
    }

    public void AcceptMission()
    {
        missionAccepted = true;
        OnMissionAccepted?.Invoke();
    }

    public void AddToy()
    {
        toysFound++;
        OnToyCountChanged?.Invoke();
    }

    public bool AllToysCollected()
    {
        return toysFound >= toysRequired;
    }
}


