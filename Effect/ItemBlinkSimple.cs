using UnityEngine;

public class ItemBlinkSimple : MonoBehaviour
{
    public Renderer rend;
    public Color blinkColor = Color.yellow;
    public float blinkSpeed = 2f;

    void Start()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
            rend.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (rend != null)
        {
            float emission = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            rend.material.SetColor("_EmissionColor", blinkColor * emission);
        }
    }
}
