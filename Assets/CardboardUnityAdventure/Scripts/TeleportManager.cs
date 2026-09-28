using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;
    public GameObject Player;
    private GameObject lastTeleportPoint;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void DisableTeleportPoint(GameObject teleportPoint)
    {
        if (lastTeleportPoint != null)
        {
            lastTeleportPoint.SetActive(true);
        }

        teleportPoint.SetActive(false);
        lastTeleportPoint = teleportPoint;

        // 1. Guardamos la posición del punto de teletransporte
        Vector3 targetPosition = teleportPoint.transform.position;

        // 2. Mantenemos la altura Y actual del Player, adoptando X y Z del punto destino
        Player.transform.position = new Vector3(targetPosition.x, Player.transform.position.y, targetPosition.z);

        // 3. Fijamos la altura local de la cámara exactamente en Y = 0.5f y X, Z en 0
        if (Camera.main != null)
        {
            Camera.main.transform.localPosition = new Vector3(0f, 0.9f, 0f);
        }

#if UNITY_EDITOR
        CardboardSimulator simulator = Player.GetComponent<CardboardSimulator>();
        if (simulator != null)
        {
            simulator.UpdatePlayerPositonSimulator();
        }
#endif
    }


}


