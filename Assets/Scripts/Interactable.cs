using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ������� ������ ������������� �����:
//    1)�������� �������� ������ � ���������� ��������
//    2)����������� ���� ������� �� �������������

public class Interactable : MonoBehaviour
{
    public int taskIndex;

    // ���������� ���������� ���� ��� �� ����
    void Update()
    {
        if (TaskManager.instance.currentTask == taskIndex)              //� ������������ � ������� ��������/��������� ���������
        {
            GetComponent<Collider>().enabled = true;                    //�������� ���������
        }
        else
        {
            GetComponent<Collider>().enabled = false;                   //��������� ���������
        }
    }

    public void Activate()
    {
        Debug.Log("����� ��������������� �: " + gameObject.name);

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
            TaskManager.instance.currentTask++;                 // ��������� � ��������� ������ 
        }

        Door door = GetComponent<Door>();                       // ���� ���� ������ "Door"
        if (door != null)
        {
            door.Activate();
        }

        Inspectable inspectable = GetComponent<Inspectable>();  // ���� ���� ������ "Inspectable"
        if (inspectable != null)
        {
            inspectable.StartInspecting();
            return;
        }

    }
}
