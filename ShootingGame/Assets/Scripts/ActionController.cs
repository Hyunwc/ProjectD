using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 

    private RaycastHit hitInfo;  // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ȹ��.

    [SerializeField]
    private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ

    [SerializeField]
    private Inventory theInventory;  // Inventory ��ũ��Ʈ

    public GameObject handle;

    private bool state = false;

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
            if (hitInfo.transform.tag == "Item" | hitInfo.transform.tag == "FlashLight" ) // tag�� Item�� ��쿡�� ȹ�� �����ϴٴ� text ���
            {
                ItemInfoAppear();
            }
        }
        else
            ItemInfoDisappear(); // Item�� �ƴ� �� text ���x
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }


    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� �߽��ϴ�."); // �κ��丮 �ֱ�
                if (hitInfo.transform.tag == "FlashLight") // Tag�� FlashLight �� ��, �÷��̾��� FlashLight�� Ȱ��ȭ
                {
                    state = true;
                    handle.gameObject.SetActive(true);
                }
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            } 
        }
    }
}
