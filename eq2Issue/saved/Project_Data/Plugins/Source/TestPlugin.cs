using HorizonReports.ConnectionManagement;
using HorizonReports.DataDictionary;
using HorizonReports.Plugins;
using HorizonReports.ReportEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonReports
{
	[VirtualTablePlugin("{FC5CDC44-72E7-415F-87F3-CEFB088FF4FE}",
    "TestVirtualTable",
    PluginSource.Custom,
    "TestVirtualTable",
    Version = "1.0.0.0",
    ExecutionPriority = 5)]
    public class TestPlugin : IVirtualTablePlugin
    {
        public IHorizonReportsAppService Application { get; set; }

        public List<Parameter> GetParameters(Table table)
        {
            List<Parameter> parameters = new()
            {
                new()
                {
                    Name = "DTFrom",
                    Type = typeof(System.DateTime),
                    Caption = "Test Param",
                    Value = DateTime.Now
                }
            };
            return parameters;
        }

        public DataTable Select(IConnection connection, string datasource, string select, string tablename, IReport report, Table table)
        {
            DataTable dt = new();
            dt.Columns.Add("Col1", typeof(string));
            return dt;
        }
    }
}
