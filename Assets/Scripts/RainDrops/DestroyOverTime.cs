using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField]
    float alphaReductionPerSecond = 0.4f;

    Renderer stainRenderer;

    Color staincolor;

    void Start()
    {
        stainRenderer = GetComponent<Renderer>();
        staincolor = stainRenderer.material.color;
    }

    void Update()
    {
        staincolor = stainRenderer.material.color;

        if (staincolor.a > 0.1f)
        {
            stainRenderer.material.color = new Color(staincolor.r, staincolor.g, staincolor.b, staincolor.a - alphaReductionPerSecond* Time.deltaTime);// -= 0.1f;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
