using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace cosmos_table
{
    class Program
    {
        static string connection_string = "BlobEndpoint=https://pruebamier.blob.core.windows.net/;QueueEndpoint=https://pruebamier.queue.core.windows.net/;FileEndpoint=https://pruebamier.file.core.windows.net/;TableEndpoint=https://pruebamier.table.core.windows.net/;SharedAccessSignature=sv=2020-02-10&ss=t&srt=sco&sp=rwdlacu&se=2021-04-08T03:57:19Z&st=2021-04-07T19:57:19Z&spr=https&sig=8Y8RU2evcd20DUnC3qeh53%2Fr4mfR17AIetcWvHuDiFU%3D";
        static void Main(string[] args)
        {
            //NewItem().Wait();
           
           //UpdateItem().Wait();
           /// DeleteItem().Wait();
            Console.ReadLine();
        }

        static async Task NewItem()
        {
            //pasamos la conezion por el cloud storage
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);
            
            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();
            
            CloudTable p_table = p_tableclient.GetTableReference("Demo"); //aqui obteneoms la refrencia de la tabla

            Customer obj = new Customer("3", "James", "New York");
            TableOperation p_operation = TableOperation.Insert(obj);
            TableResult response = await p_table.ExecuteAsync(p_operation);

            Console.WriteLine("Entity added");
        }

       
        static async Task UpdateItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Demo");

            string partition_key = "2";
            string rowkey = "James";

            Customer updated_obj = new Customer(partition_key, rowkey, "Chicago");

            TableOperation p_operation = TableOperation.InsertOrReplace(updated_obj);
            TableResult response = await p_table.ExecuteAsync(p_operation);
            Console.WriteLine("Entity updated");

        }

        static async Task DeleteItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Demo");

            string partition_key = "3";
            string rowkey = "James";

            TableOperation p_operation = TableOperation.Retrieve<Customer>(partition_key, rowkey);
            TableResult response = await p_table.ExecuteAsync(p_operation);

            Customer return_obj = (Customer)response.Result;


            TableOperation p_delete = TableOperation.Delete(return_obj);

            response = await p_table.ExecuteAsync(p_delete);
            Console.WriteLine("Entity deleted");

        }
    }
    }
