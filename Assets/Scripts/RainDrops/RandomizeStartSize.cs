using UnityEngine;

public class RandomizeStartSize : MonoBehaviour
{
    [SerializeField]
    Vector2 xScale;

    [SerializeField]
    Vector2 yScale;

    [SerializeField]
    Vector2 zScale;

    void Start()
    {
        float randomX = Random.Range(xScale.x, xScale.y);

        float randomY = Random.Range(yScale.x, yScale.y);

        float randomZ = Random.Range(zScale.x, zScale.y);


        transform.localScale = new Vector3(randomX, randomY, randomZ);
    }
}
