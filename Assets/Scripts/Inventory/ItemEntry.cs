    public class ItemEntry
    {
        public ItemWord ItemWord { get; set; }
        public bool IsUsed { get; set; }
        public ItemEntry(ItemWord itemWord)
        {
            ItemWord = itemWord;
            IsUsed = false;
        }
    }