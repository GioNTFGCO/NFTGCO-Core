using System.Collections.Generic;
using NFTGCO.Helpers;

namespace NFTGCOSHOP
{
    [System.Serializable]
    public class ItemShop 
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public ItemRarityEnum ItemRarityEnum { get; set; }
        public ShopTypeEnum ShopTypeEnum { get; set; }
        public ItemTimeResetEnum ItemTimeResetEnum { get; set; }
        public List<string> ItemTags { get; set; }
        public Dictionary<string,string> ItemProperties { get; set; }
        
        public ItemShop()
        {
            ItemTags = new List<string>();
            ItemProperties = new Dictionary<string, string>();
        }
    }
}