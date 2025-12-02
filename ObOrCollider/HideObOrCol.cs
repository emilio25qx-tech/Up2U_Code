using UnityEngine;

public class HideObOrCol : MonoBehaviour
{
    [Header("Hide GameObjects")]
    public GameObject objectToHide;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objectToHide != null)
        {
            objectToHide.SetActive(false);

            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
            {
                myCollider.enabled = false;
            }

            enabled = false;
        }
    }
}