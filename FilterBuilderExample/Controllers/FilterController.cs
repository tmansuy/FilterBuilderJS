using FilterBuilderExample;
using HorizonReports.DataDictionary;
using HorizonReports.Filtering;
using HorizonReports.Filtering.Web;
using HorizonReports.SQLServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FilterBuilder
{
    public class FilterController : Controller
    {

        public static Dictionary<string, string> ToyFilterStore { get; set; } = new Dictionary<string, string>();

        [HttpGet]
        public object FilterFields()
        {
            try
            {
                return Json(SampleData.Fields, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpGet]
        public object FilterTables()
        {
            return Json(SampleData.Tables, JsonRequestBehavior.AllowGet);
        }

        // This shows an example of how to return a requested filter to the browser. 
        [HttpGet]
        public object LoadFilter(string filtername)
        {
            if (ToyFilterStore.ContainsKey(filtername))
            {
                return Json(ToyFilterStore[filtername], JsonRequestBehavior.AllowGet);
            }

            return Json("");
        }

        // When the close button is clicked, the serialized JSON sent to this method is converted to filtercondition objects. 
        // A helper method from HorizonReports.Core then converts the filters to a sql where clause, which is returned back to the browser for display.
        [HttpPost]
        public object ProcessFilter(FilterData model)
        {
            string filterJSON = model.filterdata;
            var fields = SampleData.Fields.Select(x => x as IDbField).ToList();
            var tables = SampleData.Tables.Select(x => x as IDbTable).ToList();
            List<IFilterCondition> hrfilters = FilterModel.GetFilterConditionsFromJSON(filterJSON, fields, tables);
            string sqlwhere = FilterModel.ConvertFiltersToSQLWhere(hrfilters);
            return Json(sqlwhere);
        }

        [HttpGet]
        public object GetFilterNames()
        {
            return Json(ToyFilterStore.Keys.ToList(), JsonRequestBehavior.AllowGet);
        }


        // This shows an example of how to implement the server side of the save button.
        [HttpPost]
        public object SaveFilter(FilterData model)
        {
            if (!String.IsNullOrEmpty(model.filterdata))
            {
                // Save the filter model here.
                if (ToyFilterStore.ContainsKey(model.filtername))
                {
                    ToyFilterStore[model.filtername] = model.filterdata;
                }
                else
                {
                    ToyFilterStore.Add(model.filtername, model.filterdata);
                }

                return Json(model.filterdata);
            }
            return null;
        }

        // This shows an example of how to implement the values button lookup on the server.
        [HttpGet]
        public object FieldValues(string id)
        {
            return Json(new List<object>() { id }, JsonRequestBehavior.AllowGet);
        }


        // This shows an example of how to implement the controller side of the typeahead feature.
        [HttpGet]
        public object FieldTypeahead(string id, string q)
        {
            // We return this list so some sample lookup values are visible in the browser. Normally you would use the passed in field ID to look up the field values.
            // Make sure to filter the values returned so that only those values that contain the query string q are returned.
            List<string> values = new List<string>() { "Automotive and Transport",
                                                       "Business and Finance",
                                                       "Chemicals and Materials",
                                                       "Company Reports",
                                                       "Consumer Goods and Services",
                                                       "Country Reports",
                                                       "Energy and Natural Resources",
                                                       "Food and Beverage",
                                                       "Government and Public Sector",
                                                       "Healthcare",
                                                       "Humanities Books",
                                                       "Industry Standards",
                                                       "Manufacturing and Construction",
                                                       "Military Aerospace and Defense",
                                                       "Pharmaceuticals",
                                                       "Science Books",
                                                       "Telecommunications and Computing"};
            if (!String.IsNullOrEmpty(q))
            {
                return Json(values.Where(x => x.ToUpper().Contains(q.ToUpper())), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(values);
            }
        }
    }
}