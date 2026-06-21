using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //아이템 데이터
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems;

    //아이템 슬롯
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    //아이템 설명
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;


    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public int Additem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if(isFull)
        {
            return quantity;
        }

        this.itemName = itemName;

        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        this.itemDescription = itemDescription;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }


    public void OnLeftClick()
    {
        if(thisItemSelected)
        {
            bool useble = inventoryManager.UseItem(itemName);
            if(useble)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }

        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }


        
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingLayerName = "Obstacles";
        sr.sortingOrder = 0;

        itemToDrop.AddComponent<BoxCollider2D>();

        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0.5f,0.5f,0);
        itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Destroy(itemToDrop );

        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }

}
