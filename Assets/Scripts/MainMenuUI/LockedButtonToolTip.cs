using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LockedButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(2,5)]
    public string unlockInstructions = "Complete level 1 to unlock this feature!";

    // Assign the tooltip object (with a TextMeshProUGUI child) via Inspector
    public GameObject tooltipObject;  
    private TextMeshProUGUI tooltipText;

    // Optional offset so the tooltip doesn’t overlap the mouse pointer
    public Vector2 tooltipOffset = new Vector2(0f, 0f);

    // Track if the mouse is currently over this element
    private bool isHovering = false;

    void Awake()
    {
        if (tooltipObject != null)
        {
            tooltipText = tooltipObject.GetComponentInChildren<TextMeshProUGUI>();
            tooltipObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        if (tooltipObject != null && tooltipText != null)
        {
            tooltipObject.SetActive(true);
            tooltipText.text = unlockInstructions;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (tooltipObject != null)
        {
            tooltipObject.SetActive(false);
        }
    }

    void Update()
    {
        // If we are hovering, update the tooltip's position every frame.
        if (isHovering && tooltipObject != null)
        {
            // Get the mouse position in screen space
            Vector3 mousePos = Input.mousePosition;
            
            // Apply a small offset
            mousePos.x += tooltipOffset.x;
            mousePos.y += tooltipOffset.y;

            // Assign this to the tooltip's transform
            tooltipObject.transform.position = mousePos;
        }
    }
}