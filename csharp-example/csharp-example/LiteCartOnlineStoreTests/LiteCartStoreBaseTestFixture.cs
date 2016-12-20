using csharp_example.PagesObjects;

namespace csharp_example.LiteCartOnlineStoreTests
{
    public class LiteCartStoreBaseTestFixture : BaseTestfixture
    {

        public void RunLiteCartOnlineStore()
        {
            Driver.Url = "http://localhost/litecart/en/";
            new StoreHomePage(Driver).WaitStorePageLoaded();
        }
    }
}