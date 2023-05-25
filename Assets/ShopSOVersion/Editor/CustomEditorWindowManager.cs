using UnityEditor;
using UnityEngine;


namespace NFTGCOSHOP
{
    [System.Serializable]
    public class ItemShopClass : ItemBaseClass
    {
        public int Cost{ get; set; }
        public int Quantity{ get; set; }
        public ItemRarityEnum ItemRarityEnum{ get; set; }
        public ShopTypeEnum ShopTypeEnum{ get; set; }
        public ItemTimeResetEnum ItemTimeResetEnum{ get; set; }
    }
    [System.Serializable]
    public class ItemBaseClass
    {
        //[Header("Base data")] 
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription{ get; set; }
    }
    // public static class CustomEditorWindowManager
// {
//     [MenuItem("Window/Custom Editor Window/Player Stats")]
//     public static void ShowPlayerStatsWindow()
//     {
//         CustomEditorWindow<PlayerStats>.ShowWindow();
//     }
//
//     [MenuItem("Window/Custom Editor Window/ItemData")]
//     public static void ShowItemsDataWindow()
//     {
//         CustomEditorWindow<ItemData>.ShowWindow();
//     }
//
//     [MenuItem("Window/Custom Editor Window/QuestInfo")]
//     public static void ShowQuestInfoWindows()
//     {
//         CustomEditorWindow<QuestInfo>.ShowWindow();
//     }
// }
}