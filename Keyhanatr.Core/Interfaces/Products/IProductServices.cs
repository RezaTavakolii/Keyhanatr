﻿using Keyhanatr.Core.DTOs.Products;
using Keyhanatr.Data.Domain.Products;
using Keyhanatr.Data.ViewModel.Search;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Keyhanatr.Core.DTOs.Products.ProductsViewModels;

namespace Keyhanatr.Core.Interfaces.Products
{
    public interface IProductServices
    {

        //for _AdminLayout.cshtml
        int GetProductCount();


        ////For ProductGroupsServices
        GroupsProductsViewModel GetProductsGroups();

         List<ProductGroup> GetAllProducts();
        List<Product> GetProducts();
        List<Product> GetLastProducts();
        void AddProductGroup(ProductGroup productGroup);

        void EditProductGroup(ProductGroup productGroup);

        ProductGroup GetProductGroupById(int productGroupId);

        void DeleteProductGroup(int productGroupId);


        List<SelectListItem> GetListOfNavGroups();


        ////For ProductSubGroupServices
        ///

        //for Index
        List<ProductSubGroup> GetAllProductSubGroups();

        //for Details
        ProductSubGroup GetProductSubGroupById(int productSubGroupId);

        //for Create
        List<SelectListItem> GetGroupsListItem();// also used in Product Controller inside Create Method
        void AddProductSubGroup(ProductSubGroup productSubGroup);
        void EditProductSubGroup(ProductSubGroup productSubGroup);

        //for Delete.cshtml
        void DeleteProductSubGroupById(int productSubGroupId);



        ////for ProductsController.cs

        //for Create
        void AddProduct(Product product, IFormFile imgUp);

        //there is already a function call method for GetGroups above!


        List<SelectListItem> GetSubGroups_ByGroupId_ListItem(int groupId);

        //for Index.cs
        Tuple<ShowProducts_VM, int> ShowProductsByFilter(int pageId = 1, int take = 8, string filter = "",
            string sortType = "all", string buyType = "all");


        //for Edit.cshtml
        Product GetProductById(int productId);
        void EditProduct(Product product,IFormFile imgUp);

        //for delete.cshtml and ConfirmedDelete
        bool DeleteProduct(int productId);


        /////for NavGroups
        //for Index.cshtml
        List<ProductNavGroup> GetAllNavGroups();


        //for Create.cshtml
        void AddNavGroup(ProductNavGroup navGroup);

        //for details.cshtml
        ProductNavGroup GetNavGroupById(int navGroupId);

        //for Edit.cshtml
        void EditNavGroup(ProductNavGroup navGroup);

        //for Delete.cshtml
        void DeleteNavGroupById(int navGroupId);

        bool NaveGroupExists(int id);



        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Outside Admin


        //for Products.cs
        //for comments
        Tuple<List<ProductComment>,int> GetAllCommentsByProductId(int productId);



    }
}
