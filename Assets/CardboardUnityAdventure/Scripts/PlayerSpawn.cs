using System.Collections;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Transform player;  // el objeto PADRE de la cámara
    [SerializeField] private float espera = 0.2f;

    private IEnumerator Start()
    {
        // Esperamos un momento para que el visor lea hacia dónde mira la cabeza
        yield return new WaitForSeconds(espera);

        player.position = transform.position;

        Camera cam = player.GetComponentInChildren<Camera>();
        float rotY = transform.eulerAngles.y - cam.transform.localEulerAngles.y;
        player.rotation = Quaternion.Euler(0, rotY, 0);

#if UNITY_EDITOR
        CardboardSimulator sim = player.GetComponent<CardboardSimulator>();
        if (sim != null) sim.UpdatePlayerPositonSimulator();
#endif
    }
}