using UnityEngine;

public class TimyNPC : MonoBehaviour
{
    private enum State { OfrecerMision, EsperandoJuguetes, Agradecer, Terminado }
    [SerializeField] private State state = State.OfrecerMision;

    [Header("Referencias de escena")]
    [SerializeField] private MissionUI missionUI;
    [SerializeField] private int juguetesNecesarios = 3;
    [SerializeField] private int escenaSiguiente = 2;
    [SerializeField] private CambiodeEscena cambiadorEscena;

    private void Start()
    {
        GameProgress.Instance.StartLevel(juguetesNecesarios);
    }

    public void OnPointerClickXR()
    {
        switch (state)
        {
            case State.OfrecerMision:
                DialogueUI.Instance.ShowChoice(
                    "Hola... soy Timy. Mi cuarto está muy desordenado, ¿me ayudarías a ordenarlo?",
                    OnAceptarMision,
                    OnRechazarMision);
                break;
            case State.EsperandoJuguetes:
                DialogueUI.Instance.ShowLine("Todavía me faltan juguetes por guardar, ¡sigue buscando!");
                break;
            case State.Agradecer:
                DialogueUI.Instance.ShowLine("¡Muchas gracias por ayudarme a ordenar mi cuarto!");
                state = State.Terminado;
                Invoke(nameof(ContinuarDespuesDeAgradecer), 2.5f);
                break;
            case State.Terminado:
                break;
        }
    }

    private void OnAceptarMision()
    {
        DialogueUI.Instance.Hide();
        GameProgress.Instance.AcceptMission();
        missionUI.SetObjective("Ayuda a Timy a ordenar su habitación");
        missionUI.SetSubMission("Encuentra los juguetes desordenados en la habitación y guárdalos en la caja de juguetes");
        missionUI.ShowToyCounter();
        state = State.EsperandoJuguetes;
        GameProgress.Instance.OnToyCountChanged += RevisarSiTerminoDeRecoger;
    }

    private void OnRechazarMision()
    {
        DialogueUI.Instance.Hide();
        cambiadorEscena.CambiarEscena(0);
    }
    private void RevisarSiTerminoDeRecoger()
    {
        if (GameProgress.Instance.AllToysCollected())
        {
            missionUI.SetObjective("Habla con el pequeño Timy");
            missionUI.HideSubMission(); // <--- Ahora sí existirá

            state = State.Agradecer;
            GameProgress.Instance.OnToyCountChanged -= RevisarSiTerminoDeRecoger;
        }

    }

    private void ContinuarDespuesDeAgradecer()
    {
        DialogueUI.Instance.Hide();
        cambiadorEscena.CambiarEscena(escenaSiguiente);
    }
}