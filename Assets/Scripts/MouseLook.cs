using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 400;       //Сенса мыши

    public Transform playerBody;               //Объект с игроком

    float xRotation = 0f;

    //Start вызывается перед первым обновлением кадра
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;           //Чтобы при нажатии на экран скрыть курсор
    }

    //Обновление вызывается один раз за кадр
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;            //Time.deltaTime = подстраивается под FPS чтобы не ускорять сенсу
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;            //Эти две строчки отвечают за координаты мыши

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);                  //Ограничение камеры по вертикали в -90/90 градусов) 

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  //Для поворота по вертикали (такой способ для ограничения камеры в 90 градусов)
        playerBody.Rotate(Vector3.up * mouseX);                         //Поворачиваемся по горизонтали
    }
}
