using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Чтобы сделать объект интерактивным нужно:
//    1)Добавить дочерний объект с включенным тригером
//    2)Переключить слой объекта на интерактивный

public class Interactable : MonoBehaviour
{
    public int taskIndex;

    // Обновление вызывается один раз за кадр
    void Update()
    {
        if (TaskManager.instance.currentTask == taskIndex)              //В соответствии с задачей включаем/отключаем коллайдер
        {
            GetComponent<Collider>().enabled = true;                    //Включаем коллайдер
        }
        else
        {
            GetComponent<Collider>().enabled = false;                   //Выключаем коллайдер
        }
    }

    public void Activate()
    {
        Debug.Log("Игрок взаимодействует с: " + gameObject.name);

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
            TaskManager.instance.currentTask++;                 // Переходим к следующей задаче 
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
