using TMPro;
using UnityEngine;

public class ChoiceImageSquareScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI textVer;
    public string imageName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(imageName))
        {
            spriteRenderer.sprite = Resources.Load<Sprite>(imageName);
            imageName = null; // Clear the imageName after loading to prevent reloading every frame
        }
    }
}
