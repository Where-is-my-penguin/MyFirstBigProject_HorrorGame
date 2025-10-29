using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 1f;                 //Дальность "луча"
    public LayerMask interactLayer;                     //Слой интерактивных объектов
    public Camera playerCamera;                         //Камера игрока
    public TextMeshProUGUI hintText;                    //Ссылка на UI-подсказку


    private Interactable currentInteractable;           //На что сейчас наведёт прицел
    public float hintSpeed = 5f;

    private void Start()
    {
        if (hintText != null)
        {
            hintText.alpha = 0f;    //Скрываем подсказку при старте
            hintText.text = "<b>Нажмите [E] для взаимодействия</b>";    //Жирный текст из-за <b></b>
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);     //Создаём "луч" для взаимодействия с интерактивными объектами
        RaycastHit hit;       //Во что попал "луч"

        bool hitInteractive = Physics.Raycast(ray, out hit, interactDistance, interactLayer);   //Попал ли "луч" на интерактивный объект


        if (hitInteractive)
        {
            var interactable = hit.collider.GetComponent<Interactable>();       //Получаем компонент <Interactable> у объекта чтобы понять интерактивный ли он
            if (interactable != null)
            {
                ShowHint(true);         //Показываем подсказку
                if (Input.GetKeyDown(KeyCode.E))                //Взаимодействие с объектом
                {
                    interactable.Activate();                    //Воспроизводим звук, переходим к след.задаче и т.д.
                }
                return;
            }
        }
        ShowHint(false);                //Скрываем подсказку
    }
    void ShowHint(bool show)            //Метод для показа/скрытия подсказки
    {
        if (hintText != null)
        {
            hintText.alpha = show ? 1f : 0f;
        }
    }
}
