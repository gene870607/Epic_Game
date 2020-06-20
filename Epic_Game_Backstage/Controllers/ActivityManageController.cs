﻿using Epic_Game_Backstage.Repository.BusinessLogicLayer;
using Epic_Game_Backstage.ViewModels;
using EpicGameLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Epic_Game_Backstage.Controllers
{
    public class ActivityManageController : Controller
    {
        private ActivityManageBLO blo;
        // GET: Activity
        public ActivityManageController()
        {
            blo = new ActivityManageBLO();
        }
        public ActionResult Index(string search, string item)
        {
            var vm = blo.GetActivityManageView();
            if (!String.IsNullOrEmpty(search))
            {
                switch (item)
                {
                    case "Activity Id":
                        vm = vm.Where(x => x.ActivityID.Contains(search)).ToList();
                        break;
                    case "Product Name":
                        vm = vm.Where((x) => x.ProductName.Contains(search)).ToList();
                        break;
                    case "Title":
                        vm = vm.Where((x) => x.Title.Contains(search)).ToList();
                        break;
                    case "Content":
                        vm = vm.Where((x) => x.Content.Contains(search)).ToList();
                        break;
                    case "Time":
                        vm = vm.Where((x) => x.Time.ToString().Contains(search)).ToList();
                        break;
                }
            }

            return View(vm);
        }
        // GET: ActivityManage/Create
        public ActionResult Create()
        {
            ActivityViewModel data = new ActivityViewModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult CreateAct(string jdata)
        {
            if (jdata == null) return HttpNotFound("Error");
            ActivityViewModel AVM = JsonConvert.DeserializeObject<ActivityViewModel>(jdata);
            blo = new ActivityManageBLO();
            blo.ViewToModel(AVM);
            return RedirectToAction("Index");
        }

        // GET: ActivityManage/Delete
        public ActionResult Delete()
        {
            return View();
        }
        //接資料
        [HttpPost]
        public ActionResult Delete(string jdata)
        {
            ActivityViewModel AVM = JsonConvert.DeserializeObject<ActivityViewModel>(jdata);
            blo = new ActivityManageBLO();
            blo.DeleteActivity(AVM.ActivityID);
            return View();
        }

        // GET: ActivityManage/Edit
        public ActionResult Edit()
        {
            return View();
        }
        // GET: ActivityManage/Edit
        public ActionResult Details(string id)
        {
            var vm = blo.GetActivityDetailsView(id);
            return View(vm);
        }
        //uploadImg 寫在ProductManage
    }
}