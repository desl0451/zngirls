using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;

namespace www_zngirls_com_g
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            RibbonControl RibbonControl = new RibbonControl();
            this.Controls.Add(RibbonControl);
            // Assign the image collection that will provide images for bar items.
            RibbonControl.Images = imageCollection1;

            // Create a Ribbon page.
            RibbonPage page1 = new RibbonPage("Home");
            // Create a Ribbon page group.
            RibbonPageGroup group1 = new RibbonPageGroup("File");
            // Create another Ribbon page group.
            RibbonPageGroup group2 = new RibbonPageGroup("File 2");

            // Create a button item using the CreateButton method.
            // The created item is automatically added to the item collection of the RibbonControl.
            BarButtonItem itemOpen = RibbonControl.Items.CreateButton("Open...");
            itemOpen.ImageIndex = 7;
            //itemOpen.ItemClick += new ItemClickEventHandler(itemOpen_ItemClick);

            // Create a button item using its constructor.
            // The constructor automatically adds the created item to the RibbonControl's item collection.
            BarButtonItem itemClose = new BarButtonItem(RibbonControl.Manager, "Close");
            itemClose.ImageIndex = 12;
            //itemClose.ItemClick += new ItemClickEventHandler(itemClose_ItemClick);

            // Create a button item using the default constructor.
            BarButtonItem itemPrint = new BarButtonItem();
            // Manually add the created item to the item collection of the RibbonControl.
            RibbonControl.Items.Add(itemPrint);
            itemPrint.Caption = "Print";
            itemPrint.ImageIndex = 9;
           // itemPrint.ItemClick += new ItemClickEventHandler(itemPrint_ItemClick);

            // Add the created items to the group using the AddRange method. 
            // This method will create bar item links for the items and then add the links to the group.
            group1.ItemLinks.AddRange(new BarItem[] { itemOpen, itemClose, itemPrint });
            // Add the Open bar item to the second group.
            group2.ItemLinks.Add(itemOpen);
            // Add the created groups to the page.
            page1.Groups.Add(group1);
            page1.Groups.Add(group2);
            // Add the page to the RibbonControl.
            RibbonControl.Pages.Add(page1);


        }
    }
}