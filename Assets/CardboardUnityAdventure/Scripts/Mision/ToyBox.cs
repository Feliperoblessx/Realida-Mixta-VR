using UnityEngine;

public class ToyBox : MonoBehaviour
{
    private GrabManager grabManager;
    [SerializeField] private Transform puntoDeGuardado;

    [Header("Sonido de Guardado")]
    [SerializeField] private AudioClip sonidoGuardar;
    private AudioSource audioSource;

    void Start()
    {
        grabManager = GameObject.Find("GrabManager").GetComponent<GrabManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClickXR()
    {
        if (grabManager != null && grabManager.heldItem != null)
        {
            GrabObject juguete = grabManager.heldItem.GetComponent<GrabObject>();

            if (juguete != null)
            {
                // 1. REPRODUCIR SONIDO INMEDIATAMENTE DESDE EL CAJÓN
                if (audioSource != null)
                {
                    if (sonidoGuardar != null)
                    {
                        audioSource.PlayOneShot(sonidoGuardar);
                    }
                    else if (juguete.soundPlace != null)
                    {
                        audioSource.PlayOneShot(juguete.soundPlace);
                    }
                }

                // 2. MOVER Y DESACTIVAR EL JUGUETE DIRECTAMENTE (SIN RETRASOS DE PLACE)
                juguete.transform.position = puntoDeGuardado.position;
                grabManager.heldItem = null;
                juguete.gameObject.SetActive(false);

                // 3. SUMAR AL PROGRESO
                GameProgress.Instance.AddToy();
            }
        }
    }
}