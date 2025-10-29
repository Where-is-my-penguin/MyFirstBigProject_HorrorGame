using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 1.5f;
    public float gravity = -9.81f;

    Vector3 velocity;

    //���������� ���������� ���� ��� �� ����
    void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    //������������ ������
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");          //��� ��� ������� �������� �� ������� ������ WASD
        float z = Input.GetAxis("Vertical");            

        Vector3 move = transform.right * x + transform.forward * z;     //�������� �� ��������� �����������

        controller.Move(move * speed * Time.deltaTime);                 //��������� �� ��������� ����������� (Time.deltaTime ������� �� ������� ������)
    }

    //��������� ����������
    private void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0)    //�������� ������ �� �����
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;         //���������� (������� ������� ��� ����������)

        controller.Move(velocity * Time.deltaTime);     //�������� ���������� �� ������ (������ ������)
    }

}
