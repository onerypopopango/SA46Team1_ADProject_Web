﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SA46Team1_Web_ADProj.DAL;
using SA46Team1_Web_ADProj.Models;

namespace SA46Team1_Web_ADProj.Controllers
{
    [RoutePrefix("Store/StoreInventory")]
    public class StoreInventoryController : Controller
    {
        [Authorize(Roles = "Store Clerk, Store Manager")]
        [Route("Overview")]
        public ActionResult Overview()
        {
            return View();
        }

        [Authorize(Roles = "Store Clerk, Store Manager")]
        [Route("Reorder")]
        public ActionResult Reorder()
        {
            List<ReorderList> reorderLists;
            using (SSISdbEntities m = new SSISdbEntities())
            {
                reorderLists = m.ReorderLists.ToList();
                Session["ReorderList"] = reorderLists;
            }
            return View();
        }

        [Authorize(Roles = "Store Clerk, Store Manager")]
        [HttpPost]
        public ActionResult AddToPO(string[] arr1, string[] arrSupplier)
        {
            List<ReorderList> reorderList = (List<ReorderList>)Session["ReorderList"];
            List<Supplier> supplierList = new List<Supplier>();
            List<Item> itemAdded = new List<Item>();
            int arrayCount = 0;

            using (SSISdbEntities m = new SSISdbEntities())
            {
                // Grouping Suppliers based on array
                for(int i = 0; i < arrSupplier.Length; i++)
                {
                    string supName = arrSupplier[i];
                    Supplier sup = m.Suppliers.Where(x => x.CompanyName == supName).FirstOrDefault();
                    supplierList.Add(sup);
                }
                int qtyCount = 0;

                foreach(ReorderList r in reorderList)
                {
                    r.ReOrderQuantity = Convert.ToInt32(arr1[qtyCount]);
                    qtyCount++;
                }

                // Each Supplier iterates once such that only 1 PO is created for each of them
                foreach (Supplier s in supplierList)
                {
                    // Create new PO based on supplier
                    int count = m.POHeaders.Count() + 1;
                    string poId = "PO-" + count.ToString();

                    POHeader newPOHeader = new POHeader();
                    newPOHeader.PONumber = poId;
                    newPOHeader.Date = DateTime.Now;
                    newPOHeader.SupplierCode = s.SupplierCode;
                    newPOHeader.ContactName = s.ContactName;
                    newPOHeader.DeliverTo = "Logic University";
                    // I need session of role here
                    newPOHeader.EmployeeID = "E1";
                    newPOHeader.Remarks = "";
                    newPOHeader.Status = "Open";
                    newPOHeader.TransactionType = "PO";
                    m.POHeaders.Add(newPOHeader);
                    m.SaveChanges();

                    // Loop through PODetails to check suppliers
                    foreach (ReorderList r in reorderList)
                    {
                        // Create PO Details, Line by line based on items
                        string supName = arrSupplier[arrayCount];
                        Supplier supplier = m.Suppliers.Where(x => x.CompanyName == supName).FirstOrDefault();
                        if (supplier.Equals(s))
                        {
                            Item item = m.Items.Where(x => x.ItemCode == r.ItemCode).FirstOrDefault();
                            if (!itemAdded.Contains(item))
                            {
                                PODetail poDetailToAdd = new PODetail();
                                float itemUnitPrice = m.SupplierPriceLists.Where(x => x.SupplierCode == supplier.SupplierCode
                                    && x.ItemCode == r.ItemCode).Select(y => y.UnitCost).FirstOrDefault();
                                poDetailToAdd.PONumber = poId;
                                poDetailToAdd.ItemCode = r.ItemCode;
                                poDetailToAdd.QuantityOrdered = r.ReOrderQuantity;
                                poDetailToAdd.QuantityBackOrdered = r.ReOrderQuantity;
                                poDetailToAdd.QuantityDelivered = 0;
                                poDetailToAdd.UnitCost = itemUnitPrice;
                                poDetailToAdd.CancelledBackOrdered = 0;
                                m.PODetails.Add(poDetailToAdd);
                                m.SaveChanges();
                                itemAdded.Add(item);
                            }
                        }
                        arrayCount++;
                    }
                    // Reset Array for new PO
                    arrayCount = 0;
                }
            }
            Session["newPOList"] = new List<PODetail>();
            return View();
        }

        [Authorize(Roles = "Store Clerk")]
        [Route("StockAdj")]
        public ActionResult StockAdj()
        {
            if (Session["StockAdjPage"].ToString() == "1")
            {
                return View("StockAdj");
            }
            else
            {
                if(TempData["ItemList"] == null)
                {
                    List<StockAdjustmentDetail> sadList = new List<StockAdjustmentDetail>();
                    StockAdjustmentDetail sad = new StockAdjustmentDetail();
                    sad.ItemCode = "C001";
                    sad.RequestId = "SA/1000";
                    sad.ItemQuantity = 100;
                    sad.Amount = 100;
                    sad.Remarks = "Damaged";
                    sadList.Add(sad);

                    sad.StockAdjustmentDetails = sadList;

                    Session["StockAdjPage"] = "1";
                    return View("StockAdj2", sad);
                }
                else
                {

                    StockAdjustmentDetail sad = TempData["ItemList"] as StockAdjustmentDetail;

                    Session["StockAdjPage"] = "1";
                    return View("StockAdj2", sad);
                }                
            }
        }
        [Authorize(Roles = "Store Clerk")]
        [HttpPost]
        public RedirectToRouteResult CreateNewStockAdj()
        {                        
            Session["StockAdjPage"] = "2";            
            return RedirectToAction("Inventory", "Store");
        }

        [Authorize(Roles = "Store Clerk")]
        [HttpPost]
        public RedirectToRouteResult AddNewItem(StockAdjustmentDetail stockAdjustmentDetail)
        {
            StockAdjustmentDetail sad = new StockAdjustmentDetail();
            sad.ItemCode = "C002";
            sad.RequestId = "SA/2000";
            sad.ItemQuantity = 1100;
            sad.Amount = 1010;
            sad.Remarks = "Damaged";
            //sad.Reason = "nil";

            stockAdjustmentDetail.StockAdjustmentDetails.Add(sad);

            TempData["ItemList"] = stockAdjustmentDetail;

            Session["StockAdjPage"] = "2";
            return RedirectToAction("Inventory", "Store");
        }

        [Authorize(Roles = "Store Clerk")]
        [HttpPost]
        public RedirectToRouteResult SubmitNewStockAdj(StockAdjustmentDetail stockAdjustmentDetail)
        {
            StockAdjustmentHeader sah = new StockAdjustmentHeader();
            sah.DateRequested = DateTime.Now;
            sah.Requestor = "E1";
            //sah.Status = "Pending";
            sah.TransactionType = "Stock Adjustment";

            using(SSISdbEntities m = new SSISdbEntities())
            {
                m.StockAdjustmentHeaders.Add(sah);
                m.SaveChanges();
            }

            using (SSISdbEntities m = new SSISdbEntities())
            {
                foreach(StockAdjustmentDetail sad in stockAdjustmentDetail.StockAdjustmentDetails)
                {
                    StockAdjustmentDetail sadDb = new StockAdjustmentDetail();
                    sadDb.RequestId = sah.RequestId;
                    sadDb.ItemCode = sad.ItemCode;
                    sadDb.ItemQuantity = sad.ItemQuantity;
                    sadDb.Remarks = sad.Remarks;
                    //sadDb.Reason = sad.Reason;
                    //Temporary
                    sadDb.Amount = 1000;

                    m.StockAdjustmentDetails.Add(sadDb);
                    m.SaveChanges();

                }

            }

            return RedirectToAction("Inventory", "Store");
        }

        [Authorize(Roles = "Store Manager")]
        [Route("StockTake")]
        public ActionResult StockTake()
        {
            return View();
        }

        [Authorize(Roles = "Store Manager")]
        [HttpPost]
        public ActionResult StockTakeUpdate(StockTakeList[] arr, string[] arr1)
        {
            int count = 0;
            
            string transType = "Stock Take";
            List<StockTakeList> list = new List<StockTakeList>();
            for (int i = 0; i < arr.Length; i++)
            {
                list.Add(arr[i]);
            }

            using (SSISdbEntities m = new SSISdbEntities())
            {
                int itemTransCount = m.StockTakeHeaders.Count() + 1;
                string itemTransactionId = "ST-" + itemTransCount.ToString();
                
                // Update StockTakeHeader Table
                StockTakeHeader stockTakeHeader = new StockTakeHeader();
                stockTakeHeader.StockTakeID = itemTransactionId;
                stockTakeHeader.Date = DateTime.Now;
                stockTakeHeader.TransactionType = transType;
                m.StockTakeHeaders.Add(stockTakeHeader);
                m.SaveChanges();

                foreach (StockTakeList l in list)
                {
                    Item item = m.Items.Where(x => x.ItemCode == l.ItemCode).FirstOrDefault();
                    int itemQty = Convert.ToInt32(arr1[count]);
                    float avgCost = item.AvgUnitCost;
                    int qtyOnHand = item.Quantity;
                    int qtyAdjusted = itemQty - qtyOnHand;
                    float totalAmt = avgCost * (float) qtyAdjusted;
                    string itemcode = l.ItemCode;

                    if(qtyAdjusted != 0)
                    {
                        // Update Item Table
                        item.Quantity = itemQty;

                        // Update ItemTransaction Table
                        ItemTransaction itemTransaction = new ItemTransaction();
                        itemTransaction.TransDateTime = DateTime.Now;
                        itemTransaction.DocumentRefNo = itemTransactionId;
                        itemTransaction.ItemCode = itemcode;
                        itemTransaction.TransactionType = transType;
                        itemTransaction.Quantity = qtyAdjusted;
                        itemTransaction.UnitCost = avgCost;
                        itemTransaction.Amount = totalAmt;
                        m.ItemTransactions.Add(itemTransaction);
                        m.SaveChanges();
                    }

                    // Update StockTakeDetails Table
                    StockTakeDetail stockTakeDetail = new StockTakeDetail();
                    stockTakeDetail.StockTakeID = itemTransactionId;
                    stockTakeDetail.ItemCode = itemcode;
                    stockTakeDetail.QuantityOnHand = qtyOnHand;
                    stockTakeDetail.QuantityCounted = itemQty;
                    stockTakeDetail.QuantityAdjusted = qtyAdjusted;
                    m.StockTakeDetails.Add(stockTakeDetail);
                    m.SaveChanges();
                    count++;
                }
            }
            return View();
        }
        
    }
}
