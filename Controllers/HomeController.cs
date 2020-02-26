
using System.Web.Mvc;

namespace MVCAngular.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBConnection _DBConnection = null;
        private static readonly object datalock = new object();
        public HomeController() 
        {
            lock (datalock)
            {
                if (_DBConnection == null)
                {
                    _DBConnection = new DBConnection();
                }
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Add_Signatory(Signatory signatory) 
        {
            return Json(_DBConnection.AddSignatory(signatory),JsonRequestBehavior.AllowGet);
        
        }
        public JsonResult GetSignatories()
        {            
            return Json(_DBConnection.GetSignatories(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddSignatory()
        {
            ViewBag.Message = "Add Signatory here!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}