using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10;

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
