using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIManager : MonoBehaviour
{
    /* [Header("Start Menu")]
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_InputField inputFieldSizeText;
    [SerializeField] private TMP_InputField inputFieldSeedText;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject sizeText;
    [SerializeField] private GameObject orText;
    [SerializeField] private GameObject seedInputText; */
    
    [Header("Generation Menu")]
    [SerializeField] private Button GenButton;
    [SerializeField] private GameObject SeedGenText;
    [SerializeField] private TMP_InputField SeedGenInputField;

    [SerializeField] private GenerationSettings generationSettings;

    void Start()
    {
        ShowSeedInText();
        SwitchToGen();
        //startButton.interactable = false;
        //inputFieldSeedText.interactable = false;
        
    }
    public void CheckIfInputHasText()
    {
        /* if(string.IsNullOrWhiteSpace(inputFieldSizeText.text))
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        } */
    }

    public void SwitchToGen()
    {
        ChangeMenuUIActivity(false);
        ChangeGameUIActivity(true);
    }

    private void ChangeMenuUIActivity(bool value)
    {
        /* startButton.gameObject.SetActive(value);
        inputFieldSizeText.gameObject.SetActive(value);
        sizeText.SetActive(value);
        background.SetActive(value);
        orText.SetActive(value);
        inputFieldSeedText.gameObject.SetActive(value);
        seedInputText.SetActive(value); */
    }

    private void ChangeGameUIActivity(bool value)
    {
        GenButton.gameObject.SetActive(value);
        SeedGenInputField.gameObject.SetActive(value);
        SeedGenText.SetActive(value);
    }

    public void TransferSizeSettings()
    {
        //generationSettings.townSize = int.Parse(inputFieldSizeText.text);
        Debug.Log($"Current population: { generationSettings.townSize}");
    }

    public void TransferSeedSettings()
    {
        generationSettings.seed = int.Parse(SeedGenInputField.text);
        Debug.Log($"Current seed: { generationSettings.seed}");
    }

    public void ShowSeedInText()
    {
        SeedGenInputField.text = generationSettings.seed.ToString();
    }
}
