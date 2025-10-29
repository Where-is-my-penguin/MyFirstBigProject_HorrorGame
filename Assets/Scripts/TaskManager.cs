using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;             // ��� ��������� ������ ������
    public int currentTask = 0;                     // TaskManager.instance.currentTask

    private void Awake()
    {
        if (instance == null) instance = this;      // ���� instance �� ����������, �� �� ���������� ������� 
        else Destroy(gameObject);                   // �������� �� �������� "TaskManager" ����� ������� ������
    }

}
