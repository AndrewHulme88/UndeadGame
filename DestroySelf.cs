using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
