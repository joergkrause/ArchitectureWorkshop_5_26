using Grpc.Net.Client;
using static Workshop.GrpcServer.CustomerService;

namespace Workshop.TestConsole
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello, World!");

      AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
      using var channel = GrpcChannel.ForAddress("http://localhost:5049"); 
      var client = new CustomerServiceClient(channel);

      var customers = client.GetAllCustomers(new GrpcServer.CustomersRequest());
      Console.WriteLine($"Received {customers.Customers.Count} customers from gRPC server.");

      foreach (var customer in customers.Customers)
      {
        Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.Name}, Number: {customer.Number}");
      }

      // channel.Dispose();

      Console.ReadLine();
    }
  }
}
