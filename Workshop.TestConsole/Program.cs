using Grpc.Net.Client;
using Workshop.ServiceClients;
using static Workshop.GrpcServer.CustomerService;

namespace Workshop.TestConsole
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello, World!");

      // grpc
      //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
      //using var channel = GrpcChannel.ForAddress("http://localhost:5049"); 
      //var client = new CustomerServiceClient(channel);

      //var customers = client.GetAllCustomers(new GrpcServer.CustomersRequest());
      //Console.WriteLine($"Received {customers.Customers.Count} customers from gRPC server.");

      //foreach (var customer in customers.Customers)
      //{
      //  Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.Name}, Number: {customer.Number}");
      //}

      ////channel.Dispose();
      ///

      var httpClient = new HttpClient();
      httpClient.BaseAddress = new Uri("http://localhost:5049");
      var client = new RestClient(httpClient);

      var customers = await client.GetAllAsync();
      foreach (var customer in customers)
      {
        Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.CustomerName}, Number: {customer.CustomerNumber}");
      }

      Console.ReadLine();
    }
  }
}
