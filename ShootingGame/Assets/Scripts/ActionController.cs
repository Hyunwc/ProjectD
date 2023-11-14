using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range;  // ������ ������ ������ �ִ� �Ÿ�
    [SerializeField] private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ȹ��.
    [SerializeField] private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ
    [SerializeField] private Inventory theInventory;  // Inventory ��ũ��Ʈ

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 
    private RaycastHit hitInfo;  // �浹ü ���� ����
    public GameObject handle;
    private bool state = false;
    public Slider hpBar;

    public GameObject FireExtPanel;
    public GameObject FireExt2;

    [SerializeField] private PlayerMove playerMove;  // PlayerMove
    [SerializeField] private PlayerFire playerfire;  // PlayerFire
    [SerializeField] private CameraRotate cameraRotate;  // CameraRotate


    void Update()
    {
        CheckItem(); // ������ Ȯ��
        TryAction(); // ������ ����
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // EŰ �Է� ��
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag != null) // �ش��ϴ� tag�� ��쿡�� ȹ�� �����ϴٴ� text ���, CompareTag("")
            {
                ItemInfoAppear();
            } 
        }
        else
            ItemInfoDisappear(); // Item�� �ƴ� �� text ���x
    }

    private void ItemInfoAppear() // ������ ���� text ��� 
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);

        
        if (actionText != null)
        {
            actionText.gameObject.SetActive(false);
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� �߽��ϴ�."); // �κ��丮 �ֱ�
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
                if (hitInfo.transform.CompareTag("FlashLight")) // Tag�� FlashLight �� ��, �÷��̾��� FlashLight�� Ȱ��ȭ
                {
                    state = true;
                    handle.gameObject.SetActive(true);
                }
                else if (hitInfo.transform.CompareTag("Medicine")) // tag�� Medicine �� ��, ȹ��� hp 15ȸ��
                {
                    hpBar.value += 15;
                }
                else if (hitInfo.transform.CompareTag("FireExt")) // FireExt�� �޾��� �� ��ȭ�� ��� �г� �˾�
                {
                    actionText.enabled = false; // �ؽ�Ʈ �����
                    FireExtPanel.SetActive(true);
                    FireExt2.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;  // Ŀ�� ��� ���� 
                    Cursor.visible = true; // Ŀ�� ���̰�
                    playerMove.enabled = false; // �÷��̾� ���� ��Ȱ��ȭ
                    playerfire.enabled = false;
                    cameraRotate.enabled = false;

                }
            } 
        }
    }
}
