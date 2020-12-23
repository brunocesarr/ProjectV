using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SceneController : MonoBehaviour
{
    //  Cameras
    private GameObject ARCamera;
    private GameObject PathCamera;

    /// Dropdown
    Dropdown dropdownTypeDestination;
    Dropdown dropdownDestination;

    /// Button
    Button buttonBackToARCamera;
    Button buttonTypeDestination;
    Button buttonSelectNewDestination;
    Button buttonStartChat;
    Button buttonStartSearch;
    Button buttonStartPathButton;

    /// Canvas
    GameObject canvasNewDestination;
    GameObject panelNewDestination;
    GameObject panelControl;

    /// Serviços
	private PathManager servicePathManager;
    private PlayerControllerAnimation servicePlayerAnimation;

    private void Awake()
    {
        ARCamera = GameObject.Find("ARCamera");
        PathCamera = GameObject.Find("PathCamera");

        panelNewDestination = GameObject.Find("Panel-NewDestination");
        buttonTypeDestination = GameObject.Find("Button-TypeDestination").GetComponent<Button>();
        buttonSelectNewDestination = GameObject.Find("Button-SelectNewDestination").GetComponent<Button>();
        dropdownTypeDestination = GameObject.Find("Dropdown-TypeDestination").GetComponent<Dropdown>();
        dropdownDestination = GameObject.Find("Dropdown-Destination").GetComponent<Dropdown>();

        panelControl = GameObject.Find("Panel-Control");
        buttonBackToARCamera = GameObject.Find("Button-BackToARCamera").GetComponent<Button>();
        buttonStartSearch = GameObject.Find("Button-StartSearch").GetComponent<Button>();

        buttonStartChat = GameObject.Find("StartButton").GetComponent<Button>();
        buttonStartPathButton = GameObject.Find("StartPath").GetComponent<Button>();

        canvasNewDestination = GameObject.Find("Canvas-NewDestination");

        servicePathManager = GameObject.Find("AIPath").GetComponent<PathManager>();
        servicePlayerAnimation = gameObject.GetComponent<PlayerControllerAnimation>();

        SetupDropdown();
        SetupButtons();
    }
    private void Start()
    {
        panelNewDestination.SetActive(false);
        canvasNewDestination.SetActive(false);

        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (ARCamera.activeSelf)
            buttonBackToARCamera.gameObject.SetActive(false);
        else
            buttonBackToARCamera.gameObject.SetActive(true);


        if (servicePathManager.NavAgent.GetComponent<NavMeshAgent>().hasPath)
        {
            panelControl.SetActive(false);
            servicePlayerAnimation.StartWalking();
        }
        else
        {
            panelControl.SetActive(true);
            servicePlayerAnimation.StopWalking();
        }
    }

    private void SetupDropdown()
    {
        dropdownTypeDestination.ClearOptions();
        dropdownDestination.ClearOptions();
        PreencheTiposDestinos();
        dropdownDestination.gameObject.SetActive(false);
    }
    private void SetupButtons()
    {
        panelNewDestination.SetActive(false);
        panelControl.SetActive(false);
        buttonStartSearch.gameObject.SetActive(false);
        buttonSelectNewDestination.interactable = false;
    }

    public void ActiveButtonStartSearch()
    {
        if (buttonStartSearch)
        {
            panelControl.SetActive(true);
            buttonStartSearch.gameObject.SetActive(true);
            buttonBackToARCamera.gameObject.SetActive(false);
        }
    }
    public void DesactiveButtonStartSearch()
    {
        if (buttonStartSearch)
        {
            buttonStartSearch.gameObject.SetActive(false);
            panelControl.SetActive(false);
            servicePathManager.RemovePontoDeInicio();
        }
    }

    public void SearchDestiny()
    {
        buttonStartSearch.gameObject.SetActive(false);
        panelControl.SetActive(false);
        canvasNewDestination.SetActive(true);
        panelNewDestination.SetActive(true);

        dropdownTypeDestination.gameObject.SetActive(true);
        dropdownDestination.gameObject.SetActive(false);
        buttonTypeDestination.gameObject.SetActive(false);
        buttonSelectNewDestination.interactable = false;
    }

    public void PreencheTiposDestinos()
    {
        dropdownTypeDestination.ClearOptions();
        dropdownTypeDestination.options.Add(new Dropdown.OptionData
        {
            text = "Selecione...",
        });
        foreach (var opcao in Constantes.TiposDestinos)
        {
            //Add each entry to the Dropdown
            dropdownTypeDestination.options.Add(new Dropdown.OptionData
            {
                text = opcao,
            });
        }
    }

    /// Inicializa dropdown destinos
    public void SelecionaDestino()
    {
        if (!ValidaValorSelecionadoDropdown(dropdownTypeDestination))
            return;

        SelecionaDestinoDropdown(dropdownTypeDestination.options[dropdownTypeDestination.value].text);
        ToggleActiveDropdown();
        ToggleButtons();
    }
    private void SelecionaDestinoDropdown(string tipoDestinoSelecionado)
    {
        switch (tipoDestinoSelecionado)
        {
            case "Laboratório":
                PreencheDropdownDestinos(Constantes.Laboratorios);
                break;
            case "Sala do Professor":
                PreencheDropdownDestinos(Constantes.Professores);
                break;
            case "Grupo de Pesquisa":
                PreencheDropdownDestinos(Constantes.GruposDePesquisa);
                break;
            case "Outro Lugar":
                PreencheDropdownDestinos(Constantes.OutrosDestinos);
                break;
            default:
                break;
        }
    }
    public void PreencheDropdownDestinos(List<string> destinos)
    {
        dropdownDestination.ClearOptions();
        dropdownDestination.options.Add(new Dropdown.OptionData
        {
            text = "Selecione...",
        });
        foreach (var opcao in destinos)
        {
            //Add each entry to the Dropdown
            dropdownDestination.options.Add(new Dropdown.OptionData
            {
                text = opcao
            });
        }
    }

    /// Selecionar novo destino
    public void SelecionarNovoDestino()
    {
        if (!ValidaValorSelecionadoDropdown(dropdownDestination))
        {
            return;
        }

        string novoDestino = dropdownDestination.options[dropdownDestination.value].text;

        servicePathManager.ToggleCamera();
        servicePathManager.SetDestinyPoint(novoDestino);
        servicePathManager.SearchPath();

        panelNewDestination.SetActive(false);
    }

    /// Voltar para seleção tipos
    public void VoltarParaDropdownTipoDestino()
    {
        ToggleActiveDropdown();
        ToggleButtons();
    }

    /// Voltar para AR Câmera
    public void VoltarParaARCamera()
    {
        panelNewDestination.SetActive(false);
        buttonBackToARCamera.gameObject.SetActive(false);

        buttonStartChat.gameObject.SetActive(true);
        buttonStartSearch.gameObject.SetActive(false);

        PathCamera.SetActive(false);
        ARCamera.SetActive(true);

        gameObject.SetActive(false);
    }
    private void ToggleActiveDropdown()
    {
        dropdownTypeDestination.gameObject.SetActive(!dropdownTypeDestination.gameObject.activeSelf);
        dropdownDestination.gameObject.SetActive(!dropdownDestination.gameObject.activeSelf);
    }
    private void ToggleButtons()
    {
        buttonTypeDestination.gameObject.SetActive(!buttonTypeDestination.gameObject.activeSelf);
        buttonSelectNewDestination.interactable = !buttonSelectNewDestination.interactable;
    }

    /// Validação
    private bool ValidaValorSelecionadoDropdown(Dropdown dropdown)
    {
        if (dropdown == null)
            return false;
        else if (dropdown.value == 0)
            return false;

        return true;
    }
}
