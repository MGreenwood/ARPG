﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class InventoryEquipmentSlot : MonoBehaviour, IEquippableArea
{
    [SerializeField]
    Item.ItemType _itemType;
    [SerializeField]
    Weapon.SlotType _weaponSlot;
    [SerializeField]
    Armor.Slot _armorSlot;

    [SerializeField]
    Item _item;

    public InventoryEquipmentSlot GetEquipmentSlot()
    {
        return this;
    }

    public void SetItem(InventoryItemObject item)
    {
        _item = item.GetItem();
        item.transform.SetParent(transform.parent);

        Image image = GetComponent<Image>();
        Image itemImage = item.GetImage();

        itemImage.rectTransform.localScale = image.rectTransform.localScale;
        itemImage.rectTransform.sizeDelta = image.rectTransform.sizeDelta;
        itemImage.transform.position = image.transform.position;
    }

    public Attributes GetItemAttribute(Attributes.StatTypes statType, Item.ItemType itemType)
    {
        if(_item != null)
        {
            
        }

        return new Attributes();
    }
}