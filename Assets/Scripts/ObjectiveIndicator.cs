using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveIndicator : MonoBehaviour
{
    public Camera playerCamera;
    public RectTransform indicator;
    public float minDistance = 1f;
    public float maxDistance = 10f;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public Transform currentTarget;


    // Update is called once per frame
    void Update()
    {
        if (indicator == null || playerCamera == null)
        {
            return;
        }
        UpdateCurrentTarget();
        if (currentTarget == null)
        {
            indicator.gameObject.SetActive(false);
            return;
        }
        else
        {
            indicator.gameObject.SetActive(true);
        }

        Vector3 screenPos = playerCamera.WorldToScreenPoint(currentTarget.position);
        if (screenPos.z < 0)
        {
            screenPos *= -1;
        }
        screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
        screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

        indicator.position = screenPos;
        float distance = Vector3.Distance(playerCamera.transform.position, currentTarget.position);
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, distance));
        indicator.localScale = Vector3.one * scale;
    }
    void UpdateCurrentTarget()
    {
        currentTarget = null;

        Interactable[] allInteractables = FindObjectsOfType<Interactable>();

        foreach (var obj in allInteractables)
        {
            if (obj.taskIndex == TaskManager.instance.currentTask)
            {
                currentTarget = obj.transform;
                break;
            }
        }
    }
}
