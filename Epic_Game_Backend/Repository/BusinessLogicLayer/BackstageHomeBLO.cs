using Epic_Game_Backend.Repository.DataAccessLayer;
using Epic_Game_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using EpicGameLibrary.Service;
using System.Web;

namespace Epic_Game_Backend.Repository.BusinessLogicLayer
{
    public class BackstageHomeBLO
    {
        private BackstageHomeDAO backstageHomeDAO;
        public BackstageHomeBLO()
        {
            backstageHomeDAO = new BackstageHomeDAO();
        }
        public BackstageHomeViewModel GetAllData()
        {
            BackstageHomeViewModel backstageHomeVM = new BackstageHomeViewModel();
            backstageHomeVM.backstageSingleDataVM = backstageHomeDAO.getSingledata();
            backstageHomeVM.backstageChartLineVM = backstageHomeDAO.getMonthData();
            backstageHomeVM.monthDataTotalPrice = new int[12];
            backstageHomeVM.backstageChartLineVM002 = backstageHomeDAO.getMonthCount();
            backstageHomeVM.monthDataTotalCount = new int[12];

            var type = backstageHomeVM.backstageChartLineVM[0].GetType();
            var r = type.GetProperties();

            var type2 = backstageHomeVM.backstageChartLineVM002[0].GetType();
            var r2 = type2.GetProperties();

            for (int i = 0; i < 12; i++)
            {
                var name = r[i].Name;
                var val = (int)backstageHomeVM.backstageChartLineVM[0].GetType().GetProperty(name).GetValue(backstageHomeVM.backstageChartLineVM[0]);
                backstageHomeVM.monthDataTotalPrice[i] = val;

                var name2 = r2[i].Name;
                var val2 = (int)backstageHomeVM.backstageChartLineVM002[0].GetType().GetProperty(name2).GetValue(backstageHomeVM.backstageChartLineVM002[0]);
                backstageHomeVM.monthDataTotalCount[i] = val2;
            }

            var resultPie = backstageHomeDAO.GetProductTop5();
            backstageHomeVM.backstageChartLineVMPie = resultPie.Select((x) => new BackstageChartLineVMPie
            {
                ProductName = x.ProductName,
                count = x.count
            }).ToList();

            int size = backstageHomeVM.backstageChartLineVMPie.Count();

            //j一段到底三小?
            //backstageHomeVM.PieData = new int[size];
            backstageHomeVM.PieProductName = new string[size];
            for (int i = 0; i < backstageHomeVM.PieData.Length; i++)
            {
                backstageHomeVM.PieData[i] = backstageHomeVM.backstageChartLineVMPie[i].count;
                backstageHomeVM.PieProductName[i] = backstageHomeVM.backstageChartLineVMPie[i].ProductName;
            }

            return backstageHomeVM;
        }

        //阿三無聊重構
        public BackstageHomeViewModel GetAllData2()
        {
            var vm = new BackstageHomeViewModel
            {
                backstageSingleDataVM = backstageHomeDAO.getSingledata(),
                backstageChartLineVM = backstageHomeDAO.getMonthData(),
                backstageChartLineVM002 = backstageHomeDAO.getMonthCount(),
                backstageChartLineVMPie = getPieValues()
            };
            vm.monthDataTotalPrice = GetChartValues(vm.backstageChartLineVM);
            vm.monthDataTotalCount = GetChartValues(vm.backstageChartLineVM002);
            vm.PieProductName = GetPieValues("ProductName", vm.backstageChartLineVMPie);
            vm.PieData = GetPieValues("count", vm.backstageChartLineVMPie);

            return vm;
        }
        public int[] GetChartValues<T>(IEnumerable<T> list)
        {
            
            var el = list.First();
            Type t = el.GetType();
            var names = t.GetProperties();
            int size = names.Count();
            var result = new int[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = (int)t.GetProperty(names[i].Name).GetValue(el);
            }
            return result;
        }
        public List<BackstageChartLineVMPie> getPieValues()
        {
            var dao = new BackstageHomeDAO();
            return dao.GetProductTop5().Select((x) => new BackstageChartLineVMPie{ ProductName = x.ProductName, count = x.count }).ToList();
        }
        public dynamic[] GetPieValues<T>(string PropertyName, IList<T> list)
        {
            var result = new List<dynamic>();
            foreach(var item in list)
            {
                result.Add(item.GetValueByName(PropertyName));
            }
            var t = result.ToArray();
            return t;
        }
    }
}