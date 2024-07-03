using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncProgramming
{
    public class HomeController : Controller
    {
        public async Task<ViewResult> CarsAsync()
        {
            SampleAPIClient client = new SampleAPIClient();
            var cars = await client.GetCarsAsync();

            return View("Index", model: cars);
        }

        public ViewResult Cars(int id)
        {
            SampleAPIClient client = new SampleAPIClient();
            var cars = client.GetCarsAsync().Result;

            return View("Index", model: cars);
        }
    }


}
