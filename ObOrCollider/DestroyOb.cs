using UnityEngine;

public class DestroyOb : MonoBehaviour
{
    [Header("Object to Destroy")]
    public GameObject objectToDestroy;
    public string triggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag) && objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}
