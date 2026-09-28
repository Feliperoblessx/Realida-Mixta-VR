using UnityEngine;

using UnityEngine.UI;



public class MissionUI : MonoBehaviour

{

    [SerializeField] private Text objectiveText;

    [SerializeField] private Text subMissionText;

    [SerializeField] private Text toyCounterText;

    [SerializeField] private GameObject toyCounterPanel;



    private void OnEnable()

    {

        if (GameProgress.Instance != null)

        {

            GameProgress.Instance.OnToyCountChanged += RefreshToyCounter;

        }

    }



    private void OnDisable()

    {

        if (GameProgress.Instance != null)

        {

            GameProgress.Instance.OnToyCountChanged -= RefreshToyCounter;

        }

    }



    private void Start()

    {

        SetObjective("HABLA CON EL PEQUEÑO TIMY");



        // Ocultar submisión y contador al inicio (hasta que Timy te dé la misión)

        if (subMissionText != null)

            subMissionText.gameObject.SetActive(false);



        if (toyCounterPanel != null)

            toyCounterPanel.SetActive(false);



        // Asegurarnos de que el Game Object del pergamino esté visible

        gameObject.SetActive(true);

    }



    public void SetObjective(string text)

    {

        objectiveText.text = text;

    }



    public void SetSubMission(string text)

    {

        subMissionText.gameObject.SetActive(true);

        subMissionText.text = text;

    }



    public void ShowToyCounter()

    {

        toyCounterPanel.SetActive(true);

        RefreshToyCounter();

    }



    private void RefreshToyCounter()

    {

        toyCounterText.text = "Juguetes " + GameProgress.Instance.toysFound + "/" + GameProgress.Instance.toysRequired;

    }



    public void HideSubMission()

    {

        if (subMissionText != null)

        {

            subMissionText.gameObject.SetActive(false);

        }

    }
}

