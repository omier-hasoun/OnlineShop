namespace Domain.Common.ValidationRules;

public static class CartRules
{
        // this should be consistant with the Order entity's CartItems count limits, because the Order is created based on the Cart
        public const byte MinCartItemsCount = 1;
        public const byte MaxCartItemsCount = 250;

}

