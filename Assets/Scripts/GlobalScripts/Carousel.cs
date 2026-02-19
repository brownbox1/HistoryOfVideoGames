using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Carousel : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public HorizontalLayoutGroup layoutGroup;
    public float snapSpeed = 10f;

    private bool isTargeting = false;
    private Vector2 targetPos;

    // Update is called once per frame
    void Update()
    {
        bool isPressing = Pointer.current != null && Pointer.current.press.isPressed;
        if (!isPressing && scrollRect.velocity.magnitude < 200f)
        {
            SnaptoNearest();
        }
    }

    void SnaptoNearest()
    {
        float closestDistance = float.MaxValue;
        float targetX = contentPanel.anchoredPosition.x;

        foreach (RectTransform child in contentPanel)
        {
            float childLocalX = child.localPosition.x;
            float containerCenterInContentSpace = -contentPanel.anchoredPosition.x;

            float distance = childLocalX - containerCenterInContentSpace;

            if (Mathf.Abs(distance) < Mathf.Abs(closestDistance))
            {
                closestDistance = distance;

                targetX = contentPanel.anchoredPosition.x - distance;
            }
        }
        
        Vector2 targetVec = new Vector2(targetX, contentPanel.anchoredPosition.y);
        contentPanel.anchoredPosition = Vector2.Lerp(contentPanel.anchoredPosition, targetVec, snapSpeed*Time.deltaTime);
    }
}
