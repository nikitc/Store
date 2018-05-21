namespace Store.Models.Order
{
    public enum OrderStates
    {
        /// <summary>
        /// Заказ в корзине
        /// </summary>
        BasketState = 1,

        /// <summary>
        /// Заказ в процессе сборки
        /// </summary>
        ProcessState = 2,

        /// <summary>
        /// Заказ исполнен
        /// </summary>
        CompleteState = 3         
    }
}
