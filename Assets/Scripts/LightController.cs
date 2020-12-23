using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light light;
    // Start is called before the first frame update
    void Start()
    {
        light.gameObject.SetActive(false);
    }

    public void ActiveLight()
    {
        light.gameObject.SetActive(true);
    }
    public void DesactiveLight()
    {
        light.gameObject.SetActive(false);
    }
}
