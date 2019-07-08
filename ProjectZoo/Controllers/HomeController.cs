using ProjectZoo.Helper;
using ProjectZoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectZoo.Controllers
{
    public class HomeController : Controller
    {
        private static DB db = new DB();

        private static List<ZooAnimal> CurrentAnimals = db.ZooAnimals.ToList();

        public bool IsHealthCheckRunning = false;

        /// <summary>
        /// Main index view
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            //Check if System is initialized if not initialized it based on configration data in Helper/config.cs
            if (!CurrentAnimals.Any()) {
                CurrentAnimals = await ZooHelper.ReInit();
            }
            //Check if Health update process is running or now if not run the process
            if (!IsHealthCheckRunning) {
                Task.Run(() => ProcessHealthCheck());
            }
            return View(CurrentAnimals);
        }

        /// <summary>
        /// Backend process to Monitor and Update Health
        /// </summary>
        public async void ProcessHealthCheck(){
            try
            {
                IsHealthCheckRunning = true;
                do
                {
                    await Task.Delay(config.HealthCheckInterval).ContinueWith(async (t) =>
                    {
                        CurrentAnimals = await ZooHelper.UpdateHealth();
                        if (CurrentAnimals.Count == 0)
                            IsHealthCheckRunning = false;
                    });
                } while (CurrentAnimals.Count > 0);
            }
            catch (Exception ee) {
                System.Diagnostics.Debug.WriteLine(string.Format("Error in Healthcheck {0}\n Please fix error and refresh page to start process"));
                IsHealthCheckRunning = false;
            }
        }

        /// <summary>
        /// process called every 20 sec from js to update health in UI
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetUpdatedData()
        {
             
            return Json(CurrentAnimals, JsonRequestBehavior.AllowGet);
            
        }

        
        /// <summary>
        /// Updating all animals health on feed
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> FeedAll()
        {
            CurrentAnimals = await ZooHelper.FeedUpdate();
            return View("Index", CurrentAnimals);
        }

        //Sending current server time as Zoo time 
        [HttpPost]
        public ActionResult CurrentZooTime() {
            return Json(DateTime.Now.ToString(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// At any point in time if you want to rest simulator and start from scratch 
        /// This will clear database and repopulate based in Configuraiton in config file
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ResetAll() {

            CurrentAnimals = await ZooHelper.ReInit();
            return View("Index", CurrentAnimals);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}