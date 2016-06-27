using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor.Gutters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Sitecore.SharedSource.SitecoreJourney
{
    public class SubItemCount : GutterRenderer
    {
        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            string childCount = item.Children.Count.ToString();

            /* Commented below code for better performance
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();            
            Log.Info("API Query... ", this);
            s.Start();
            string descedantCount = item.Axes.GetDescendants().Count().ToString();
            s.Stop();
            Log.Info("API Query took " + s.ElapsedMilliseconds + " milliseconds count " + descedantCount.ToString(), this);
            */

            //System.Diagnostics.Stopwatch s2 = new System.Diagnostics.Stopwatch();
            //s2.Start();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["master"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(ID) from Descendants where Ancestor='" + item.ID + "'");
            cmd.Connection = con;
            //Log.Info("DB Query.... ", this);
            int descedantCountDB = (int)cmd.ExecuteScalar();
            //s2.Stop();
            //Log.Info("DB Query took " + s2.ElapsedMilliseconds + " milliseconds.  count " + descedantCountDB.ToString(), this);
            con.Close();


            if (item.Children.Count > 0)
            {
                GutterIconDescriptor descriptor = new GutterIconDescriptor
                {
                    Icon = "http://dummyimage.com/16x16/F0E68C/000000.png&text=" + childCount,
                    Tooltip = "Subitem Count: " + childCount.ToString() + System.Environment.NewLine + "Descendant Count: " + descedantCountDB.ToString().ToString()
                };

                return descriptor;
            }
            else
            {
                GutterIconDescriptor descriptor = new GutterIconDescriptor { };
                return descriptor;
            }
        }
    }
}
