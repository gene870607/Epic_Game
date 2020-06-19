﻿using EpicGameLibrary.Models;
using EpicGameLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Epic_Game_Backstage.Repository.DataAccessLayer
{
    public class ActivityManageDAO
    {
        public EGContext context = new EGContext();

        public List<Activity> GetAllActivities()
        {

            using (var _context = new EGContext())
            {
                var repo = new EGRepository<Activity>(_context);
                return repo.GetAll().AsEnumerable().ToList();

            }

        }
        //Create
        public void CreateActivity(Activity a)
        {
            using(var _context = new EGContext())
            {
                var repo = new EGRepository<Activity>(_context);
                repo.Create(a);
                _context.SaveChanges();
            }
        }
        //Detail
        public Activity GetDetailActivity(string id)
        {
            return context.Activity.SingleOrDefault(x => x.ActivityID.ToString().Equals(id));
        }
        //Delete
        public void DeleteAct(string ActivityId)
        {
            var delete_item = context.Activity.FirstOrDefault(x => x.ActivityID.ToString().Equals(ActivityId));
            context.Activity.Remove(delete_item);
            context.SaveChanges();
        }
        //updateImg
    }
}