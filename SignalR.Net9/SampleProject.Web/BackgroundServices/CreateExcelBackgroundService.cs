
using ClosedXML.Excel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;
using SampleProject.Web.Hubs;
using SampleProject.Web.Models;
using System;
using System.Data;
using System.Threading.Channels;

namespace SampleProject.Web.BackgroundServices
{
    public class CreateExcelBackgroundService(Channel<(string userId, List<Product> products)> channel, IFileProvider fileProvider,IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await channel.Reader.WaitToReadAsync(stoppingToken))
            {
                var (userId, products) = await channel.Reader.ReadAsync(stoppingToken);

                var wwwrootFolder = fileProvider.GetDirectoryContents("wwwroot");

                var files = wwwrootFolder.Single(x=>x.Name == "files");

                var newExcelFileName = $"{userId}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

                var newExcelFilePath = Path.Combine(files.PhysicalPath, newExcelFileName);

                var wb = new XLWorkbook();

                var ds = new DataSet();

                ds.Tables.Add(GetTable("Products", products));

                wb.Worksheets.Add(ds);

                await using var excelFileStream = new FileStream(newExcelFilePath, FileMode.Create, FileAccess.Write);

                wb.SaveAs(excelFileStream);

                //hub

                using (var scope = serviceProvider.CreateScope()) {
                    var appHub = scope.ServiceProvider.GetRequiredService<IHubContext<AppHub>>();

                    await appHub.Clients.User(userId).SendAsync("AlertCompleteFile", $"/files/{newExcelFileName}", stoppingToken);
                } 
            }
        }

        private DataTable GetTable(string tableName, List<Product> products)
        {
            var table = new DataTable { TableName = tableName };

            foreach (var item in typeof(Product).GetProperties()) table.Columns.Add(item.Name, item.PropertyType);


            products.ForEach(x => { table.Rows.Add(x.Id, x.Name, x.Price, x.Description, x.UserId); });

            return table;
        }
    }
}
