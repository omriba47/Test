using System;
using System.Collections.Generic;
using System.Linq;
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
using MySql.Data.MySqlClient;
using System.Windows.Threading;


namespace Pinger
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public static class Selected
    {

        public static string selectedTeam;
        
    }
    public partial class MainWindow : Window
    {


        public string subquery="";
        public string team = "";


        
        public MainWindow()
        {
           
            //Login f = new Login();
            //f.ShowDialog();
            InitializeComponent();
            Selected.selectedTeam = "Hamal";
            switch (Selected.selectedTeam)
            {
                case "Tzyr":
                    subquery = " where Team ='Tzyr'";
                    team = "Tzyr";
                    break;
                case "Darom":
                    subquery = " where Team ='Darom'";
                    team = "Darom";
                    break;
                case "Hamal":
                    subquery = "";
                    team = "Hamal";
                    break;
                default:
                    subquery = "";
                    team = "Hamal";
                    break;

            }

            MySqlConnection connection;
            DbClass.ConnectDB(out connection);
            CheckSites(connection);    
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((sender, e) => dispatcherTimer_Tick(sender, e, connection));
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e,MySqlConnection con)
        {
            UpdateGrid(con);
        }
        public static void OpenSiteWindow(object sender, EventArgs e, string SiteId, ref MySqlConnection connection)
        {
            SiteWindow Sw = new SiteWindow(SiteId, connection);
            Sw.ShowDialog();
        }
        public void UpdateGrid(MySqlConnection connection)
        {
            CheckSites(connection);
        }
        public void CheckSites(MySqlConnection connection)
        {
            string select_query = "Select c.SiteId, sites.SiteName,sites.LastStatus, sites.Status from sites right join components as c on c.SiteId = sites.Id" + subquery + " group by c.SiteId order by sites.Status DESC, sites.LastStatus DESC ";

            MySqlDataReader dr = DbClass.SelectQuery(connection, select_query);
            mygrid.Children.Clear();
            mygrid.RowDefinitions.Clear();
            int i = 0; int j = 0;
            mygrid.RowDefinitions.Add(new RowDefinition());
            
            DateTime LastStatus;
            SolidColorBrush GreenBrush = new SolidColorBrush(Color.FromRgb(43, 179, 0));
            SolidColorBrush YellowBrush = new SolidColorBrush(Color.FromRgb(255, 235, 0));

            SolidColorBrush GoldBrush = new SolidColorBrush(Color.FromRgb(255, 150, 0));
            SolidColorBrush RedBrush = new SolidColorBrush(Color.FromRgb(255, 55, 0));
            while (dr.Read())
            {
                if (j > 3)
                {
                    j = 0;
                    i++;
                    mygrid.RowDefinitions.Add(new RowDefinition());
                }
                LastStatus = Convert.ToDateTime(dr["LastStatus"]);
                string SiteId = dr["SiteId"].ToString();
                Button b = new Button();
                b.Name = dr["SiteName"].ToString();
                b.Content = dr["SiteName"].ToString();
                b.Focusable = false;
                b.SetValue(Grid.RowProperty, i);
                b.SetValue(Grid.ColumnProperty, j);

                b.Click += new RoutedEventHandler((sender, e) => OpenSiteWindow(sender, e, SiteId, ref connection));
                switch (dr["Status"].ToString())
                {
                    case "0":
                        
                        b.SetValue(BackgroundProperty, GreenBrush);
                        break;
                    case "1":
                        b.SetValue(BackgroundProperty, YellowBrush);
                        break;
                    case "2":
                         b.SetValue(BackgroundProperty,GoldBrush);
                         break;
                    case "3":
                         b.SetValue(BackgroundProperty, RedBrush);
                         break;   
                    default:
                        b.SetValue(BackgroundProperty, GreenBrush);
                        break;


                }



                mygrid.Children.Add(b);

                j++;
                
            }



            dr.Dispose();
        }


    }
}
