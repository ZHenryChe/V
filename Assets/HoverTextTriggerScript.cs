using UnityEngine;

public class HoverTextTriggerScript : MonoBehaviour
{

    public GameObject hoverText;

    void OnMouseEnter()
    {
        if (hoverText != null)
        {
            //Debug.Log("Hover text activated");
            hoverText.SetActive(true);
        }
    }
    void OnMouseExit()
    {
        if (hoverText != null)
        {
            hoverText.SetActive(false);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
