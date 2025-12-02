using UnityEngine;

public class ShowObOrCol : MonoBehaviour
{
    [Header("Show GameObjects")]
    public GameObject objectToShow;

    void Start()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objectToShow != null)
        {
            objectToShow.SetActive(true);

            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
            {
                myCollider.enabled = false;
            }

            enabled = false;
        }
    }
}