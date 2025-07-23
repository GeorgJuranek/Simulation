using UnityEngine;

public class CigaretteEnflamer : MonoBehaviour, IFlameable
{
    [SerializeField]
    CigaretteController cigaretteController;

    [SerializeField]
    GameObject amber;

    [SerializeField]
    float burnedPaperStartThreshold = 2.14f;

    public void Burn()
    {
        if (cigaretteController != null && !cigaretteController.enabled)
        {
            cigaretteController.enabled = true;

            cigaretteController.TargetRenderer.material.SetFloat("_Threshold", burnedPaperStartThreshold);
        }

        if (amber.activeSelf == false)
        {
            amber.SetActive(true);
        }

        cigaretteController.Inhale();
    }
}
