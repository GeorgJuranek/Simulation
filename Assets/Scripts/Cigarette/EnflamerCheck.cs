using UnityEngine;

public class EnflamerCheck : MonoBehaviour
{
    [SerializeField]
    float distanceToCheck = 5f;

    [SerializeField]
    LayerMask fire;


    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, distanceToCheck, fire))
        {
            GetComponent<IFlameable>().Burn();
            this.enabled = false;
        }
    }

}
