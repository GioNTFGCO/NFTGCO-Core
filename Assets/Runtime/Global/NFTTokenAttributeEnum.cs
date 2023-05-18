using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTCreator
{
    public enum NFTTokenAttributeEnum
    {
        //Armor
        [NFTTokenAttributeEnumAttribute("Armor")]
        Head_Armor = 0,
        [NFTTokenAttributeEnumAttribute("Armor")]
        Left_Arm_Armor = 1,
        [NFTTokenAttributeEnumAttribute("Armor")]
        Right_Arm_Armor = 2,
        [NFTTokenAttributeEnumAttribute("Armor")]
        Torso_Armor = 3,
        [NFTTokenAttributeEnumAttribute("Armor")]
        Hip_Armor = 4,
        [NFTTokenAttributeEnumAttribute("Armor")]
        Left_Leg_Armor = 5,
        [NFTTokenAttributeEnumAttribute("Armor")]
        Right_Leg_Armor = 6,

        [NFTTokenAttributeEnumAttribute("Armor")]
        Background_Armor = 7,

        //Accesories
        [NFTTokenAttributeEnumAttribute("Accessory")]
        Head_Accessory = 8,
        [NFTTokenAttributeEnumAttribute("Accessory")]
        Back_Accessory = 9,
        [NFTTokenAttributeEnumAttribute("Accessory")]
        Chest_Accessory = 10,
        [NFTTokenAttributeEnumAttribute("Accessory")]
        Arms_Accessory = 11,
        [NFTTokenAttributeEnumAttribute("Accessory")]
        Hips_Accessory = 12,
        [NFTTokenAttributeEnumAttribute("Accessory")]
        Legs_Accessory = 13,

        //Auras
        [NFTTokenAttributeEnumAttribute("Aura")]
        Aura_Accessory = 14
    }

    public class NFTTokenAttributeEnumAttribute : Attribute
    {
        public string Name { get; private set; }

        public NFTTokenAttributeEnumAttribute(string name)
        {
            this.Name = name;
        }
    }
}