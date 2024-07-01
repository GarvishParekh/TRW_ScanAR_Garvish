using UnityEngine;

public class SpawnLog : MonoBehaviour
{
    private void Start()
    {
        Debug.Log ("Item spawned: " + this.gameObject.name);
    }
}
