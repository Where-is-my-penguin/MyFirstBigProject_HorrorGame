using DG.Tweening;                  // Библиотека для плавной анимации
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform MainDoor;      // Ссылка на дверь
    public float openAngle = -175f; // Угол поворота
    public float duration = 2.0f;   // Время анимации (секунды)

    private bool opened = false;    // Открыта ли дверь

    public void Activate()          // Взаимодействие с дверью
    {
        if (opened) return;         // Если дверь открыта - ничего не делаем

        opened = true;
        MainDoor.DORotate(new Vector3(0, openAngle, 0), duration, RotateMode.LocalAxisAdd); // Настройки анимации 
    }
}
