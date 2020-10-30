using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Windows;

namespace UserSecrets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IConfiguration Configuration;

        public MainWindow()
        {
            InitializeComponent();
            doConfig();
        }

        private void doConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", 
                optional: true, 
                reloadOnChange: true);

            var devEnvVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvVariable) || 
                                    devEnvVariable.ToLower() == "development";

            if (isDevelopment)
            {
                /// TODO : Do not forget to add the user secret file
                /// 
                builder.AddUserSecrets<MainWindow>();
            }
            
            Configuration = builder.Build();

            Debug.WriteLine($"A value in the appsettings.json : " +  $"{Configuration["defaultPath"]}");

            var secondValue =
                Configuration.GetSection("someConfigSection")["second"];

            Debug.WriteLine($"Second value : {secondValue}");

            var secretCode = Configuration["NuclearCode"];

            Debug.WriteLine("The nuclear code is : " + 
                secretCode);
        }
    }
}
