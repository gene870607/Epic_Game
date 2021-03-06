﻿using Epic_Game.Repository.DataOperationLayer;
using Epic_Game.ViewModels;
using EpicGameLibrary.Models;
using EpicGameLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using WebGrease.Css.Extensions;

namespace Epic_Game.Repository.BusinessLogicLayer
{
    public class LibraryBLO
    {
        public string Key;
        private LibraryDAO libraryDAO { get; set; }

        public LibraryBLO(string UserId)
        {
            libraryDAO = new LibraryDAO(UserId);
        }
        public LibraryBLO(string UserId,string key)
        {
            libraryDAO = new LibraryDAO(UserId);
            Key = key;
        }
        public List<LibraryViewModel> LibraryToView(List<Library> p)
        {
            var viewModel = new List<LibraryViewModel>();
            
            foreach (var item in p)
            {
                viewModel.Add (new LibraryViewModel { ProductId = item.ProductID.ToString(),ProductName = item.Product.ProductName, Img_Url = libraryDAO.GetImg(item.ProductID.ToString()),Img_Url_list=libraryDAO.GetImg_list(item.ProductID.ToString()), Date = libraryDAO.GetDate(item.ProductID.ToString()) });
            }

            return viewModel;
        }
        public List<LibraryViewModel> OrderLibraryToView(List<Library> p)
        {
            var viewModel = new List<LibraryViewModel>();

            foreach (var item in p)
            {
                viewModel.Add(new LibraryViewModel { ProductId = item.ProductID.ToString(), ProductName = item.Product.ProductName, Img_Url = libraryDAO.GetImg(item.ProductID.ToString()), Img_Url_list = libraryDAO.GetImg_list(item.ProductID.ToString()), Date = libraryDAO.GetDate(item.ProductID.ToString())});
            }
            var result = viewModel.OrderByPropertyName(Key).ToList();
            return result;
        }
        public List<LibraryViewModel> GetLibraryProduct()
        {
            var l = libraryDAO.GetLibraryProduct();
            return LibraryToView(l);
        }
        public List<LibraryViewModel> OrderLibraryProduct()
        {
            var l = libraryDAO.GetLibraryProduct();
            return OrderLibraryToView(l);
        }

    }
}