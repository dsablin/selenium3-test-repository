using csharp_example.PagesObjects;
using NUnit.Framework;

namespace csharp_example.LiteCartOnlineStoreTests
{
    class LiteCartGoogsOrderTests : LiteCartStoreBaseTestFixture
    {
        [Test]
        public void LiteCartStoreManageCartTest()
        {
            var storeHome = new StoreHomePage(Driver);
            var product = new ProductPage(Driver);
            var cart = new CartPage(Driver);
            const int goodsToBuy = 7;

            RunLiteCartOnlineStore();
            product.MoveProductsToCartByNumberGiven(
                storeHome, goodsToBuy, storeHome.GetCurrentCartItemsCount());
            storeHome.OpenCart();
            cart.ClearCart();
            storeHome.WaitStorePageLoaded();
        }
    }
}
