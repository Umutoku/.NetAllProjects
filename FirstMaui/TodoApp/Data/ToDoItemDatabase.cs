using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class ToDoItemDatabase
    {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<ToDoItemDatabase> Instance = new AsyncLazy<ToDoItemDatabase>(async () =>
        {
            var instance = new ToDoItemDatabase();
            try
            {
                CreateTableResult result = await Database.CreateTableAsync<TodoItem>();
            }
            catch (Exception)
            {

                throw;
            }
            return instance;
        });

        public ToDoItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return Database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemNotDoneAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return Database.Table<TodoItem>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if(item.Id !=0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem todoItem)
        {
            return Database.DeleteAsync(todoItem);
        }

    }
}
