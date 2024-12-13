using System.Collections.Generic;
using System;
using HorizonReports.Filtering.Web;
using System.Linq;
using HorizonReports.DataDictionary;



namespace FilterBuilderExample
{

    static class SampleData
    {
        public static DatabaseModel DemoDatabase = new DatabaseModel() { Name = "SampleDatabase", ID = Guid.NewGuid(), BeginDelimiter = "[", EndDelimiter = "]" };
        public static List<TableModel> Tables { get; set; } = GetTables();
        public static List<FieldModel> Fields { get; set; } = GetFields();

        public static List<JoinModel> Joins { get; set; } = GetJoins();

        private static List<TableModel> GetTables()
        {
            var tables = new List<TableModel>()
            { new TableModel() { Name = "Customers", ID = Guid.NewGuid(), Caption = "Customers", Module = "Sales", Database = DemoDatabase },
                new TableModel() { Name = "Orders", ID = Guid.NewGuid(), Caption = "Orders", Module = "Sales", Database = DemoDatabase },
                new TableModel() { Name = "Products", ID = Guid.NewGuid(), Caption = "Products", Module = "Sales", Database = DemoDatabase },
                new TableModel() { Name = "Employees", ID = Guid.NewGuid(), Caption = "Employees", Module = "Payroll", Database = DemoDatabase },
                new TableModel() { Name = "Expenses", ID = Guid.NewGuid(), Caption = "Expenses", Module = "Payroll", Database = DemoDatabase }
            };
            return tables;
        }

        private static List<FieldModel> GetFields()
        {
            var customers = Tables.First(x => x.Name == "Customers");
            var orders = Tables.First(x => x.Name == "Orders");
            var products = Tables.First(x => x.Name == "Products");
            var employees = Tables.First(x => x.Name == "Employees");
            var expenses = Tables.First(x => x.Name == "Expenses");
            var fields = new List<FieldModel>()
            {
                new FieldModel(typeof(Int32)) { Name = "Customers.CustomerID", ID = Guid.NewGuid(), Table = customers, Caption = "Customer ID"},
                new FieldModel(typeof(String)) { Name = "Customers.CustomerName", ID = Guid.NewGuid(), Table = customers, Caption = "Customer Name" },
                new FieldModel(typeof(Int32)) {Name = "Orders.OrderID", ID = Guid.NewGuid(), Table = orders, Caption = "Order ID"},
                new FieldModel(typeof(DateTime)) {Name = "Orders.OrderDate", ID = Guid.NewGuid(), Table = orders, Caption = "Order Date"},
                new FieldModel(typeof(Decimal)) {Name = "Orders.OrderTotal", ID = Guid.NewGuid(), Table = orders, Caption = "Order Total" },
                new FieldModel(typeof(Int32)) {Name = "Orders.CustomerID", ID = Guid.NewGuid(), Table = orders, Caption = "Customer ID" },
                new FieldModel(typeof(String)) { Name = "Products.ProductName", ID = Guid.NewGuid(), Table = products, Caption = "Product Name" },
                new FieldModel(typeof(String)) { Name = "Products.Category", ID = Guid.NewGuid(), Table = products, Caption = "Category" },
                new FieldModel(typeof(String)) { Name = "Employees.EmployeeID", ID = Guid.NewGuid(), Table = employees, Caption = "Employee ID" },
                new FieldModel(typeof(String)) { Name = "Employees.Name", ID = Guid.NewGuid(), Table = employees, Caption = "Employee Name" },
                new FieldModel(typeof(Int32)) { Name = "Expenses.ExpenseID", ID = Guid.NewGuid(), Table = expenses, Caption = "Expense ID" },
                new FieldModel(typeof(DateTime)) { Name = "Expenses.TransactionDate", ID = Guid.NewGuid(), Table = expenses, Caption = "Transaction Date" },
                new FieldModel(typeof(Decimal)) { Name = "Expenses.Amount", ID = Guid.NewGuid(), Table = expenses, Caption = "Expense Amount" },
                new FieldModel(typeof(String)) { Name = "Expenses.Description", ID = Guid.NewGuid(), Table = expenses, Caption = "Description" },
                new FieldModel(typeof(Boolean)) { Name = "Expenses.Approved", ID = Guid.NewGuid(), Table = expenses, Caption = "Approved" },
            };

            return fields;
        }

        private static List<JoinModel> GetJoins()
        {
            var customers_key = Fields.First(x => x.Name == "Customers.CustomerID");
            var orders_key = Fields.First(x => x.Name == "Orders.CustomerID");
            
            var employees = Tables.First(x => x.Name == "Employees");
            var expenses = Tables.First(x => x.Name == "Expenses");

            // This shows how to create one regular join, and one join that uses a complex expression
            var joins = new List<JoinModel>()
            {
                new JoinModel() { JoinType = JoinTypes.InnerJoin, ParentTable = customers_key.Table, ChildTable = orders_key.Table, Expressions = new List<IJoinExpression>() { new JoinExpression(customers_key, orders_key) } },
                new JoinModel() { JoinType = JoinTypes.FavorParent, ParentTable = employees, ChildTable = expenses, ComplexExpression = "Employees.EmployeeID = Expenses.EmployeeID" }
            };
            return joins;
        }
    }

    public class FilterData
    {
        public string filtername { get; set; }
        public string filterdata { get; set; }
        public FilterData()
        {

        }
    }
}
