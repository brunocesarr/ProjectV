using UnityEngine;

public class Cam3Player : MonoBehaviour
{
    public GameObject head;
    public GameObject[] positions;
    private int indice = 0;
    private float SpeedMoviment = 2;
    //  Evitar que a camera atravessa a parece
    private RaycastHit hit;

    void FixedUpdate()
    {
        transform.LookAt(head.transform);
        //  Checar se tem colisor
        if (!Physics.Linecast(head.transform.position, positions[indice].transform.position))
        {
            transform.position = Vector3.Lerp(transform.position, positions[indice].transform.position, SpeedMoviment * Time.deltaTime);
            Debug.DrawLine(head.transform.position, positions[indice].transform.position);
        }
        else if (Physics.Linecast(head.transform.position, positions[indice].transform.position, out hit))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, SpeedMoviment * 2 * Time.deltaTime);
            Debug.DrawLine(head.transform.position, positions[indice].transform.position);
        }
    }

    void AlteraCameraPlayer()
    {
        if (indice < (positions.Length - 1))
            indice++;
        else if (indice >= (positions.Length - 1))
            indice = 0;
    }
}
