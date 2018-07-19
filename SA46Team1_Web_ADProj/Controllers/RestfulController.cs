﻿using SA46Team1_Web_ADProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;


namespace SA46Team1_Web_ADProj.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/Restful")]
    public class RestfulController : ApiController
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetEmployeeList")]
        public List<Employee> GetEmployeeList()
        {
            using (SSISdbEntities m = new SSISdbEntities())
            {
                //to further filter by user's deptCode
                m.Configuration.ProxyCreationEnabled = false;
                return m.Employees.OrderBy(x=>x.EmployeeName).ToList<Employee>();
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetItemCodeList")]
        public List<String> GetItemCodeList()
        {
            using (SSISdbEntities m = new SSISdbEntities())
            {
                return m.InventoryOverviews.Select(x => x.ItemCode).ToList<String>();
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetInventoryOverviewList/{id}")]
        public List<InventoryOverview> GetInventoryOverviewList(string id)
        {
            using (SSISdbEntities m = new SSISdbEntities())
            {
                m.Configuration.ProxyCreationEnabled = false;
                return m.InventoryOverviews.Where(x => x.ItemCode == id).ToList<InventoryOverview>();
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetGoodsReceivedLists/{id}")]
        public List<GoodsReceivedList> GetGoodsReceivedLists(string id)
        {
            using (SSISdbEntities m = new SSISdbEntities())
            {
                m.Configuration.ProxyCreationEnabled = false;
                return m.GoodsReceivedLists.Where(x => x.ItemCode == id).ToList<GoodsReceivedList>();
            }
        }
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("/AdjustmentOverView")]
        public List<StockAdjustmentHeader> GetStockAdjustmentOverview()
        {
            using (SSISdbEntities m = new SSISdbEntities())
            {
                m.Configuration.ProxyCreationEnabled = false;
                return m.StockAdjustmentHeaders.ToList();
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("/GetStockAdjustmentList")]
        public List<StockAdjustmentOverview> GetStockAdjustmentList()
        {
            //Temporary placeholder to make the requestID = 1
            string requestorId = "E1";            

            using (SSISdbEntities m = new SSISdbEntities())
            {
                m.Configuration.ProxyCreationEnabled = false;
                return m.StockAdjustmentOverviews.Where(x => x.Requestor == requestorId).ToList<StockAdjustmentOverview>();
            }

        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetShortItemList")]
        public List<Item> GetShortItemList()
        {
            using (SSISdbEntities m = new SSISdbEntities())
            {
                m.Configuration.ProxyCreationEnabled = false;
                return m.Items.ToList();
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("GetStockAdjustmentSupervisorApproval")]
        public List<StockAdjustmentApprovalForSupervisor> GetStockAdjustmentSupervisorApproval()
        {
            using (SSISdbEntities m = new SSISdbEntities())
            {
                m.Configuration.ProxyCreationEnabled = false;
                return m.StockAdjustmentApprovalForSupervisors.ToList();
            }
        }

    }
}