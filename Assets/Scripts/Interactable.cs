using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ������� ������ ������������� �����:
//    1)�������� �������� ������ � ���������� ��������
//    2)����������� ���� ������� �� �������������

public class Interactable : MonoBehaviour
{
    public int[] taskIndex;                     // ������ �����  (����� ��������� �����)
    public AudioClip[] audioClips;              // ������ ������ (��� ������ ������)
    private Collider col;                       // ��������� �������
    public GameObject objectToEnable;           // ������ ������� ������ ����������


    private void Awake()                        
    {
        col = GetComponent<Collider>();         // �������� ��������� �������                                                                                         
    }

    // ���������� ���������� ���� ��� �� ����
    void Update()
    {
        bool isActiveTast = false;                                                                                                               
        foreach (var index in taskIndex)                    // ���������� ������ �����
        {
            if (TaskManager.instance.currentTask == index)  // ���� ��������� � ������� �������
            {
                isActiveTast = true;    // ���� ������ �������
                break;                  // ������� �� �����
            }
        }
        if (col != null)                // ���� ��������� ����
            col.enabled = isActiveTast; // ���� ��������� �������, �� ����� �����������������
    }

    public void Activate()              // ����� ����� ��������������� � ��������
    {
        Debug.Log("����� ��������������� �: " + gameObject.name);       // � ����� �������� ��������������� 

        AudioSource audio = GetComponent<AudioSource>();                // �������� �����
        if (audio != null)
        {
            for (int i = 0; i < taskIndex.Length; i++)                  // ���������� ��� ������
            {
                if (TaskManager.instance.currentTask == taskIndex[i] && i < audioClips.Length && audioClips[i] != null)     // ��� ������ ������ ��������� ������ ����
                {
                    audio.clip = audioClips[i];     // ������������� ������ ����
                    audio.Play();                   // ������ ����
                    break;
                }
                if (objectToEnable != null && !objectToEnable.activeSelf)       // ���� ����� �������� ������ - ��������
                {
                    objectToEnable.SetActive(true);
                    TaskManager.instance.currentTask++;
                    return;
                }
            }
            TaskManager.instance.currentTask++;                 // ������� � ��������� ������
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
