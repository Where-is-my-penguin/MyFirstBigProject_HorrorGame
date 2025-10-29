using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 1.5f;
    public float gravity = -9.81f;

    Vector3 velocity;

    //Обновление вызывается один раз за кадр
    void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    //Передвижения игрока
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");          //Эти две строчки отвечают за нажатия клавиш WASD
        float z = Input.GetAxis("Vertical");            

        Vector3 move = transform.right * x + transform.forward * z;     //Изменяем по ЛОКАЛЬНЫМ координатам

        controller.Move(move * speed * Time.deltaTime);                 //Двигаемся по ЛОКАЛЬНЫМ координатам (Time.deltaTime зависит от частоты кадров)
    }

    //Настройки гравитации
    private void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0)    //Проверка игрока на земле
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;         //Гравитация (плавное падение под ускорением)

        controller.Move(velocity * Time.deltaTime);     //Действие гравитации на игрока (плавно падать)
    }

}
