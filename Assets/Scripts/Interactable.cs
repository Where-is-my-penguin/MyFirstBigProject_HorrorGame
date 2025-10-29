using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Чтобы сделать объект интерактивным нужно:
//    1)Добавить дочерний объект с включенным тригером
//    2)Переключить слой объекта на интерактивный

public class Interactable : MonoBehaviour
{
    public int[] taskIndex;                     // Массив задач  (можно несколько задач)
    public AudioClip[] audioClips;              // Массив звуков (для каждой задачи)
    private Collider col;                       // Коллайдер объекта
    public GameObject objectToEnable;           // Объект который должен включаться


    private void Awake()                        
    {
        col = GetComponent<Collider>();         // Получаем коллайдер объекта                                                                                         
    }

    // Обновление вызывается один раз за кадр
    void Update()
    {
        bool isActiveTast = false;                                                                                                               
        foreach (var index in taskIndex)                    // Перебираем номера задач
        {
            if (TaskManager.instance.currentTask == index)  // Если совпадает с текущей задачей
            {
                isActiveTast = true;    // Если объект активен
                break;                  // Выходим из цикла
            }
        }
        if (col != null)                // Если коллайдер есть
            col.enabled = isActiveTast; // Если коллайдер включен, то можно взаимодействовать
    }

    public void Activate()              // Когда игрок взаимодействует с объектом
    {
        Debug.Log("Игрок взаимодействует с: " + gameObject.name);       // С каким объектом взаимодействуем 

        AudioSource audio = GetComponent<AudioSource>();                // Получаем аудио
        if (audio != null)
        {
            for (int i = 0; i < taskIndex.Length; i++)                  // Перебираем все задачи
            {
                if (TaskManager.instance.currentTask == taskIndex[i] && i < audioClips.Length && audioClips[i] != null)     // Для первой задачи запускаем первый звук
                {
                    audio.clip = audioClips[i];     // Устанавливаем нужный звук
                    audio.Play();                   // Играем звук
                    break;
                }
                if (objectToEnable != null && !objectToEnable.activeSelf)       // Если ножно включить объект - включаем
                {
                    objectToEnable.SetActive(true);
                    TaskManager.instance.currentTask++;
                    return;
                }
            }
            TaskManager.instance.currentTask++;                 // Переход к следующей задаче
        }

        Door door = GetComponent<Door>();                       // Если есть скрипт "Door"
        if (door != null)
        {
            door.Activate();
        }

        Inspectable inspectable = GetComponent<Inspectable>();  // Если есть скрипт "Inspectable"
        if (inspectable != null)
        {
            inspectable.StartInspecting();
            return;
        }
    }
}
