using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveIndicator : MonoBehaviour
{
    public Transform currentTarget;     // Текущая цель с индикатором
    public Camera playerCamera;         // Камера игрока
    public RectTransform indicator;     // Оранжевый кружок (индикатор для задания)
    public float minDistance = 1f;      // Мин.Дистанция до объекта
    public float maxDistance = 10f;     // Мах.Дистанция до объекта
    public float minScale = 0.5f;       // Мин.Размер индикатора
    public float maxScale = 2f;         // Мах.Размер индикатора
    

    void Update()
    {
        if (indicator == null || playerCamera == null)      // Проверка есть ли камера и индикатор
        {
            return;
        }
        UpdateCurrentTarget();                              // Обновляем цель в соответствии с текущей задачей
        if (currentTarget == null)                          // Если нет цели выключаем индикатор
        {
            indicator.gameObject.SetActive(false);
            return;
        }
        else            // Если цель найдена - включаем индикатор
        {
            indicator.gameObject.SetActive(true);
        }

        Vector3 screenPos = playerCamera.WorldToScreenPoint(currentTarget.position);        // Получаем координаты объекта на экране
        if (screenPos.z < 0)
        {
            screenPos *= -1;
        }
        screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);                            // Ограничиваем позицию индикатора на границах экрана
        screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);                           // Ограничиваем позицию индикатора на границах экрана

        indicator.position = screenPos;         // Перемещаем индикатор в нужное место
        float distance = Vector3.Distance(playerCamera.transform.position, currentTarget.position);             // Считаем расстояние до цели
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, distance));    // Маштаб индикатора в зависимости от расстояния
        indicator.localScale = Vector3.one * scale;     // Применяем рассчитаный маштаб
    }
    void UpdateCurrentTarget()          // Ищем текущий интерактивный объект
    {
        currentTarget = null;           // Сбрасываем текущую цель

        Interactable[] allInteractables = FindObjectsOfType<Interactable>();        // Находим все интерактивные объекты

        foreach (var obj in allInteractables)       // Перебираем объекты
        {
            foreach (var index in obj.taskIndex)    
            {
                if (index == TaskManager.instance.currentTask)      // Если текущая задача совпадает
                {
                    currentTarget = obj.transform;                  // Запоминаем цель
                    return;
                }
            }
        }
    }
}
