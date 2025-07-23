using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class CigaretteController : MonoBehaviour
{
    [SerializeField]
    Transform toMove;

    Renderer rendererOfToMove;

    [SerializeField]
    float ashHeatMin = 0.6f;

    [SerializeField]
    float ashHeatMax = 0.8f;

    [SerializeField]
    Transform endPosition;

    [SerializeField]
    Renderer targetRenderer;

    public Renderer TargetRenderer { get => targetRenderer; }

    [SerializeField]
    VisualEffect exhaleEffect;

    float currentConsumation = 0f;
    float targetConsumation = 0f;

    bool isInCoroutine;

    [SerializeField]
    float consumationAmount = 0.2f;

    [SerializeField]
    float consumationSpeed = 0.01f;

    float amountRelativeToDistance;


    private void Awake()
    {

        float distanceToEnd = Vector3.Distance(toMove.localPosition, endPosition.localPosition);

        amountRelativeToDistance = distanceToEnd * (consumationAmount / 1);

        rendererOfToMove = toMove.gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isInCoroutine && targetConsumation+consumationAmount<1)
        {
            Inhale();
        }

        if (!isInCoroutine && rendererOfToMove.material.GetFloat("_BurnState")> ashHeatMin)
        {
            toMove.gameObject.GetComponent<Renderer>().material.SetFloat("_BurnState", Mathf.Lerp(rendererOfToMove.material.GetFloat("_BurnState"), ashHeatMin, consumationSpeed));
        }
    }

    public void Inhale()
    {
        if (Time.timeScale == 0) return;
            
        targetConsumation += consumationAmount;
        StartCoroutine(Consumation());
    }

    IEnumerator Consumation()
    {
        isInCoroutine = true;

        exhaleEffect.Play();

        Vector3 directionToMove = (endPosition.localPosition - toMove.localPosition).normalized;
        Vector3 newPosition = toMove.localPosition + directionToMove*amountRelativeToDistance;


        while (Vector3.Distance(toMove.localPosition, newPosition)>0.001f && currentConsumation + 0.001f < targetConsumation)
        {

            directionToMove = (endPosition.localPosition - toMove.localPosition).normalized;
            toMove.localPosition = Vector3.Lerp(toMove.localPosition, newPosition, consumationSpeed);

            float process = Mathf.Lerp(currentConsumation, targetConsumation, consumationSpeed);
            currentConsumation = process;
            targetRenderer.material.SetFloat("_BurnState", currentConsumation);

            rendererOfToMove.material.SetFloat("_BurnState", Mathf.Lerp(rendererOfToMove.material.GetFloat("_BurnState"), ashHeatMax, consumationSpeed));

            while (Time.timeScale == 0) // for pausing coroutine while menu
            {
                yield return null;
            }

            yield return null;
        }

        exhaleEffect.Stop();

        isInCoroutine = false;
    }
}
