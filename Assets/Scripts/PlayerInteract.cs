using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 1f;                 //��������� "����"
    public LayerMask interactLayer;                     //���� ������������� ��������
    public Camera playerCamera;                         //������ ������
    public TextMeshProUGUI hintText;                    //������ �� UI-���������


    private Interactable currentInteractable;           //�� ��� ������ ������ ������
    public float hintSpeed = 5f;

    private void Start()
    {
        if (hintText != null)
        {
            hintText.alpha = 0f;    //�������� ��������� ��� ������
            hintText.text = "<b>������� [E] ��� ��������������</b>";    //������ ����� ��-�� <b></b>
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);     //������ "���" ��� �������������� � �������������� ���������
        RaycastHit hit;       //�� ��� ����� "���"

        bool hitInteractive = Physics.Raycast(ray, out hit, interactDistance, interactLayer);   //����� �� "���" �� ������������� ������


        if (hitInteractive)
        {
            var interactable = hit.collider.GetComponent<Interactable>();       //�������� ��������� <Interactable> � ������� ����� ������ ������������� �� ��
            if (interactable != null)
            {
                ShowHint(true);         //���������� ���������
                if (Input.GetKeyDown(KeyCode.E))                //�������������� � ��������
                {
                    interactable.Activate();                    //������������� ����, ��������� � ����.������ � �.�.
                }
                return;
            }
        }
        ShowHint(false);                //�������� ���������
    }
    void ShowHint(bool show)            //����� ��� ������/������� ���������
    {
        if (hintText != null)
        {
            hintText.alpha = show ? 1f : 0f;
        }
    }
}
