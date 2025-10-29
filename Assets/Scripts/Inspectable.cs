using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class Inspectable : MonoBehaviour
{
    [SerializeField] public GameObject originalVisual;      // Подбираемый объект
    [SerializeField] private GameObject pickupHint;         // Надпись "Нажмите [E] чтобы взять предмет"
    [SerializeField] private GameObject inspectCopy;        // Ссылка на копию объекста
    [SerializeField] private Transform inspectPosition;     // Позиция перед камерой где объект будет осматриваться
    [SerializeField] private float rotationSpeed = 800f;    // Скорость вращения мышью

    public PostProcessVolume postProcessVolume;             // Размытие фона при взятии объекта
    public PlayerMovement playerMovement;                   // Скрипт на управление игроком
    public MouseLook mouseLook;                             // Скрипт на управление камерой
    private bool isInspecting = false;                      // Осматривает ли игрок объект
    private Vector3 previousMousePosition;                  // Положение мыши для расчета движения
    private GameObject activeCopy;                          // Активная копия объекта
    private DepthOfField dof;                               // Depth Of Field

    private void Start()            
    {
        if (postProcessVolume != null)
            postProcessVolume.profile.TryGetSettings(out dof);
    }


    void Update()
    {
        if (!isInspecting) return;          // Если игрок не рассматривает объект пропускаем весь Update()

        float deltaX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float deltaY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        activeCopy.transform.Rotate(Vector3.up, -deltaX, Space.World);      //Вращаем объект по осям
        activeCopy.transform.Rotate(Vector3.right, deltaY, Space.World);
        previousMousePosition = Input.mousePosition;                        // Обновляем позицию мыши

        if (Input.GetKeyDown(KeyCode.E))    //Выход из осмотра клавишой "Е"
        {
            StopInspecting();
        }
    }
    public void StartInspecting()       // Начало осмотра
    {
        if (inspectCopy == null)
        {
            Debug.Log($"{name}: нет ссылки на копию для осмотра!");
            return;         // Если копии объекта нет - выходим
        }

        originalVisual.SetActive(false);
        if (pickupHint != null)
            pickupHint.SetActive(true);

        if (dof != null)             // Включаем размытие фона
            dof.active = true;

        // Создаем копию перед камерой
        activeCopy = Instantiate(inspectCopy, inspectPosition.position, Quaternion.identity);
        isInspecting = true;
        if (playerMovement != null)         // Блокируем управление игроком и камерой
        {
            playerMovement.enabled = false;
            mouseLook.enabled = false;
        }
    }
    public void StopInspecting()    // Конец осмотра
    {
        if (activeCopy != null)
            Destroy(activeCopy);    // Удаляем копию объекта

        isInspecting = false;
        if (playerMovement != null) //разблокируем управление игроком и камерой
        {
            playerMovement.enabled = true;
            mouseLook.enabled = true;
        }
        if (pickupHint != null)
            pickupHint.SetActive(false);

        if (dof != null)             // Выключаем размытие фона
            dof.active = false;
    }

}
