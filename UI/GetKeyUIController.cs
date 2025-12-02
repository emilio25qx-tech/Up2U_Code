using System.Collections;
using UnityEngine;

public class GetKeyUIController : MonoBehaviour
{
    [Header("KeyUI Setting")]
    public GameObject getKeyUI;
    public float timeShowText = 2f;

    public void ShowGetKeyUIMessage()
    {
        StartCoroutine(ShowUI());
    }

    IEnumerator ShowUI()
    {
        getKeyUI.SetActive(true);
        yield return new WaitForSeconds(timeShowText);
        getKeyUI.SetActive(false);
    }
}