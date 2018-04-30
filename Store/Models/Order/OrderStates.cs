namespace Store.Models.Order
{
    public enum OrderStates
    {
        //Когда заказ это корзина
        BasketState,

        //Когда заказ в процессе сборки
        ProcessState,

        //Когда заказ выполнен
        CompleteState            
    }
}
