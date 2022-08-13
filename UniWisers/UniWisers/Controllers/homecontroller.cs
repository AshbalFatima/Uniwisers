using Microsoft.AspNetCore.Mvc;

namespace UniWisers.Controllers
{
    public class homecontroller : Controller
    {
        public string Index() {
            return "AoA Pakistan";
        }
        public string Student () {
            return "AOA Students";
        }
    }
}
