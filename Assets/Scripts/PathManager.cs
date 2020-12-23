using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class PathManager : MonoBehaviour
{
    public Vector3 startPoint { get; set; }
    public Vector3 destinyPoint { get; set; }

    public GameObject NavAgent { get; set; }

    private GameObject Predio;
    private GameObject sceneController;

    private GameObject ARCamera;
    private GameObject PathCamera;

    private GameObject StartPathButton;

    private Vector3 StartPointDefault;

    private void Awake()
    {
        sceneController = GameObject.Find("Controller");
        ARCamera = GameObject.Find("ARCamera");
        PathCamera = GameObject.Find("PathCamera");
        StartPathButton = GameObject.Find("StartPath");
        Predio = GameObject.Find("Predio");
        NavAgent = GameObject.Find("Player");
        StartPointDefault = NavAgent.transform.position;
    }
    private void Start()
    {
        Setup();
    }
    private void Setup()
    {
        ARCamera.SetActive(true);
        PathCamera.SetActive(false);
        StartPathButton.SetActive(false);
    }

    public void SetStartPoint(string ponto)
    {
        var inicio = buscarPonto(ponto);

        if (!string.IsNullOrEmpty(inicio))
        {
            startPoint = GameObject.Find(inicio).transform.position;
        }
        else
        {
            startPoint = StartPointDefault;
        }

        DefinePontoDeInicio();
    }

    public void DefinePontoDeInicio()
    {
        NavAgent.GetComponent<NavMeshAgent>().transform.position = startPoint;
        Predio.SetActive(false);
    }
    public void RemovePontoDeInicio()
    {
        // NavAgent.GetComponent<NavMeshAgent>().transform.position = StartPointDefault;
        Predio.SetActive(true);
    }

    public void SetDestinyPoint(string ponto)
    {
        var destino = buscarPonto(ponto);

        if (!string.IsNullOrEmpty(destino))
        {
            destinyPoint = GameObject.Find(destino).transform.position;
            Debug.Log("Destino: " + destinyPoint);
        }
    }
    private string buscarPonto(string ponto)
    {
        var destino = Constantes.DefaultPosicao
                        .FirstOrDefault(defaultPosicao => defaultPosicao.Key.Equals(ponto));

        if (destino.Key != null)
        {
            return destino.Value;
        }

        destino = Constantes.ProfessoresPosicao
                        .FirstOrDefault(professor => professor.Key.Equals(ponto));

        if (destino.Key != null)
        {
            return destino.Value;
        }

        destino = Constantes.LaboratoriosPosicao
                    .FirstOrDefault(laboratorio => laboratorio.Key.Equals(ponto));

        if (destino.Key != null)
        {
            return destino.Value;
        }

        destino = Constantes.GruposPesquisaPosicao
                    .FirstOrDefault(grupoPesquisa => grupoPesquisa.Key.Equals(ponto));

        if (destino.Key != null)
        {
            return destino.Value;
        }

        destino = Constantes.OutrosDestinosPosicao
                    .FirstOrDefault(outroDestino => outroDestino.Key.Equals(ponto));

        if (destino.Key != null)
        {
            return destino.Value;
        }

        return "";
    }

    public void SearchPath()
    {
        if (destinyPoint == null)
            return;

        StartPathButton.SetActive(true);
    }
    public void StartPath()
    {
        StartPathButton.SetActive(false);

        NavAgent.GetComponent<NavMeshAgent>().nextPosition = startPoint;
        NavAgent.GetComponent<NavMeshAgent>().SetDestination(destinyPoint);

        sceneController.SetActive(true);
    }
    public void ToggleCamera()
    {
        ARCamera.SetActive(false);
        PathCamera.SetActive(true);
        StartPathButton.SetActive(true);
        Predio.SetActive(true);
    }
}
