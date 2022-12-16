using ImbizoFoundation.Classes;
using ImbizoFoundation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImbizoFoundation.Controllers
{
    public class MarketPlaceController : Controller
    {
        FireBaseWorker fireBaseWorker = new FireBaseWorker();
        FireBaseStorageWorker storageWorker = new FireBaseStorageWorker();
        // GET: MarketPlaceController
        public ActionResult Index()
        {
            List<MarketPlace> mList = new List<MarketPlace>();
            mList = fireBaseWorker.getAllCompany();
            return View(mList);
        }

        // GET: MarketPlaceController/Details/5
        public ActionResult Details(string companyName)
        {
            List<MarketPlace> marketPlaceList = fireBaseWorker.getAllCompany();

            MarketPlace m = new MarketPlace();

            m = marketPlaceList.Where(x => x.companyName.Equals(companyName)).FirstOrDefault();

            return View(m);
        }

        // GET: MarketPlaceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarketPlaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string companyName, string companyEmailAddress, string companyDescription, string companyLogoURL)
        {

            MarketPlace m = new MarketPlace(companyName,companyEmailAddress,companyDescription,companyLogoURL);

            fireBaseWorker.writeCompany(m);


            return RedirectToAction(nameof(Index));
        }

        // GET: MarketPlaceController/Edit/5
        public ActionResult Edit(string id)
        {
            List<MarketPlace> marketPlaceList = fireBaseWorker.getAllCompany();

            MarketPlace m = new MarketPlace();

            m = marketPlaceList.Where(x => x.companyName.Equals(id)).FirstOrDefault();


            return View(m);
        }

        // POST: MarketPlaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,string companyName, string companyEmailAddress, string companyDescription, string companyLogoURL)
        {
            MarketPlace m = new MarketPlace(companyName, companyEmailAddress, companyDescription, companyLogoURL);

            Worker.companyIDs = companyName;

            fireBaseWorker.updateCompany(m);

            return RedirectToAction(nameof(Index));
        }

        // POST: MarketPlaceController/Delete/5
        
        public IActionResult Delete(string companyName ,string id)
        {
            fireBaseWorker.deleteCompany(id);

            return RedirectToAction("Index");
        }
    }
}
