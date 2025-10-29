using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class Inspectable : MonoBehaviour
{
    [SerializeField] public GameObject originalVisual;      // ����������� ������
    [SerializeField] private GameObject pickupHint;         // ������� "������� [E] ����� ����� �������"
    [SerializeField] private GameObject inspectCopy;        // ������ �� ����� ��������
    [SerializeField] private Transform inspectPosition;     // ������� ����� ������� ��� ������ ����� �������������
    [SerializeField] private float rotationSpeed = 800f;    // �������� �������� �����

    public PostProcessVolume postProcessVolume;             // �������� ���� ��� ������ �������
    public PlayerMovement playerMovement;                   // ������ �� ���������� �������
    public MouseLook mouseLook;                             // ������ �� ���������� �������
    private bool isInspecting = false;                      // ����������� �� ����� ������
    private Vector3 previousMousePosition;                  // ��������� ���� ��� ������� ��������
    private GameObject activeCopy;                          // �������� ����� �������
    private DepthOfField dof;                               // Depth Of Field

    private void Start()            
    {
        if (postProcessVolume != null)
            postProcessVolume.profile.TryGetSettings(out dof);
    }


    void Update()
    {
        if (!isInspecting) return;          // ���� ����� �� ������������� ������ ���������� ���� Update()

        float deltaX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float deltaY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        activeCopy.transform.Rotate(Vector3.up, -deltaX, Space.World);      //������� ������ �� ����
        activeCopy.transform.Rotate(Vector3.right, deltaY, Space.World);
        previousMousePosition = Input.mousePosition;                        // ��������� ������� ����

        if (Input.GetKeyDown(KeyCode.E))    //����� �� ������� �������� "�"
        {
            StopInspecting();
        }
    }
    public void StartInspecting()       // ������ �������
    {
        if (inspectCopy == null)
        {
            Debug.Log($"{name}: ��� ������ �� ����� ��� �������!");
            return;         // ���� ����� ������� ��� - �������
        }

        originalVisual.SetActive(false);
        if (pickupHint != null)
            pickupHint.SetActive(true);

        if (dof != null)             // �������� �������� ����
            dof.active = true;

        // ������� ����� ����� �������
        activeCopy = Instantiate(inspectCopy, inspectPosition.position, Quaternion.identity);
        isInspecting = true;
        if (playerMovement != null)         // ��������� ���������� ������� � �������
        {
            playerMovement.enabled = false;
            mouseLook.enabled = false;
        }
    }
    public void StopInspecting()    // ����� �������
    {
        if (activeCopy != null)
            Destroy(activeCopy);    // ������� ����� �������

        isInspecting = false;
        if (playerMovement != null) //������������ ���������� ������� � �������
        {
            playerMovement.enabled = true;
            mouseLook.enabled = true;
        }
        if (pickupHint != null)
            pickupHint.SetActive(false);

        if (dof != null)             // ��������� �������� ����
            dof.active = false;
    }

}
