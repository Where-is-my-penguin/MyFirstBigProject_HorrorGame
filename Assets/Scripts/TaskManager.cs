using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;             // Для обращения откуда угодно
    public int currentTask = 0;                     // TaskManager.instance.currentTask

    private void Awake()
    {
        if (instance == null) instance = this;      // Если instance не существует, то он становится главным 
        else Destroy(gameObject);                   // Проверка на дубликат "TaskManager" иначе удаляет объект
    }

}
