using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor.Gutters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SharedSource.SitecoreJourney
{
    public class SubItemCount : GutterRenderer
    {
        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            Assert.ArgumentNotNull(item, "item");            
            string childCount = item.Children.Count.ToString();
            string descedantCount = item.Axes.GetDescendants().Count().ToString();
            if (item.Children.Count > 0)
            {
                GutterIconDescriptor descriptor = new GutterIconDescriptor
                {
                    Icon = "http://dummyimage.com/16x16/F0E68C/000000.png&text=" + childCount,
                    Tooltip = "Subitem Count: " + childCount.ToString() + System.Environment.NewLine + "Descendant Count: " + descedantCount.ToString()
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
