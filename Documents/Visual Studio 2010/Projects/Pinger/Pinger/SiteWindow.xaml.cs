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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Windows.Threading;
namespace Pinger
{
    /// <summary>
    /// Interaction logic for SiteWindow.xaml
    /// </summary>
    public partial class SiteWindow : Window
    {
        
        private string siteId;
        private MySqlConnection con;

        public SiteWindow(string SiteId, MySqlConnection con)
        {
            InitializeComponent();
            this.siteId = SiteId;
            this.con = con;
            Check_Components(this.con);
           


            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((sender, e) => dispatcherTimer_Tick(sender, e, this.con));
            // dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e, MySqlConnection con)
        {
            UpdateDataGrid(con);
        }
        public void UpdateDataGrid(MySqlConnection connection)
        {
            Check_Components(connection);
        }
        public void Check_Components(MySqlConnection con)
        {
            //####CHECK QUERY#######
            MySqlDataReader dr;
            string status;
            string select_query = "Select sites.SiteName,c.CompId, c.CompName, c.CompIp, c.Status, c.LastTime from components as c left join sites on c.SiteId = sites.Id where c.SiteId ='" + this.siteId + "' order by c.Status DESC, LastTime DESC";
            dr = DbClass.SelectQuery(con, select_query);
            if (dr.HasRows) MyDataGrid.Items.Clear();
            while (dr.Read())
            {
                if (dr["Status"].ToString() == "0") status = "Up";
                else status = "Down";

                MyDataGrid.Items.Add(
                new Comp()
                {
                    CompId = dr["CompId"].ToString(),
                    CompName = dr["CompName"].ToString(),
                    CompIp = dr["CompIp"].ToString(),
                    Status = status,
                    LastTime = dr["LastTime"].ToString()
                });


            }
            this.Title = dr["SiteName"].ToString();
            dr.Dispose();
            //####END OF CHECK#######
        }
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
    }
}
