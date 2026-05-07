using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Workshop.GrpcServer;
using Workshop.ServiceClients;

namespace Workshop.DesktopClient
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly RestClient _restClient;
    private readonly CustomerService.CustomerServiceClient _grpcClient;

    public MainWindow(RestClient restClient, CustomerService.CustomerServiceClient grpcClient)
    {
      _restClient = restClient;
      _grpcClient = grpcClient;
      InitializeComponent();
      Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
      try
      {
        var restCustomers = await _restClient.GetAllAsync();
        //var grpcCustomers = await _grpcClient.GetAllCustomersAsync(new Workshop.GrpcServer.CustomersRequest());
        RestTextBlock.Text = "REST Customers:\n" + string.Join("\n", restCustomers.Select(c => $"{c.Id}: {c.CustomerName}"));
        //GrpcTextBlock.Text = "gRPC Customers:\n" + string.Join("\n", grpcCustomers.Customers.Select(c => $"{c.Id}: {c.Name}"));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error loading customers: {ex.Message}");
      }

    }
  }
}