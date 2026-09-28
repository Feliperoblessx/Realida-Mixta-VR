using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    GrabManager grabManager;
    BoxCollider boxCollider;
    Vector3 spawnerPosition;
    Quaternion spawnerRotation;

    public AudioClip soundGrab;
    public AudioClip soundPlace;

    [SerializeField] public string type = "Objeto";
    [SerializeField] public GameObject spawner;

    private AudioSource player;

    void Start()
    {
        player = GetComponent<AudioSource>();
        spawnerPosition = spawner.transform.position;
        spawnerRotation = spawner.transform.rotation;
        boxCollider = GetComponent<BoxCollider>();
        grabManager = GameObject.Find("GrabManager").GetComponent<GrabManager>();
    }

    public void Grab()
    {
        player.PlayOneShot(soundGrab);
        if (grabManager.heldItem != null)
        {
            grabManager.heldItem.GetComponent<GrabObject>().Drop();
        }
        grabManager.heldItem = transform.gameObject;
        boxCollider.enabled = false;
    }

    public void Drop()
    {
        transform.position = spawnerPosition;
        transform.rotation = spawnerRotation;
        grabManager.heldItem = null;
        boxCollider.enabled = true;
    }

    public void Delete()
    {
        transform.position = spawnerPosition;
        transform.rotation = spawnerRotation;
        grabManager.heldItem = null;
        boxCollider.enabled = true;
        transform.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        transform.position = spawnerPosition;
        transform.rotation = spawnerRotation;
        boxCollider.enabled = true;
        transform.gameObject.SetActive(true);
    }

    public void Place(Vector3 position)
    {
        player.PlayOneShot(soundPlace);
        transform.position = position;
        grabManager.heldItem = null;
        boxCollider.enabled = true;
    }

    public void OnPointerClickXR()
    {
        // Verifica si la misión ya fue aceptada antes de permitir agarrar
        if (GameProgress.Instance != null && !GameProgress.Instance.missionAccepted)
        {
            // Si no se ha aceptado la misión, muestra un diálogo indicándolo (si tienes DialogueUI)
            if (DialogueUI.Instance != null)
            {
                DialogueUI.Instance.ShowLine("Primero debes hablar con Timy y aceptar la misión.");
            }
            return; // Cancela la acción de agarrar
        }

        Grab();
    }
}
