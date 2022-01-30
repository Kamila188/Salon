using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Магазин_Красавицы.Utilities;

namespace Магазин_Красавицы.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProduct.xaml
    /// </summary>
    public partial class AddEditProduct : Page
    {
        public AddEditProduct()
        {
            InitializeComponent();
        }

        #region Работа встроенного веб-браузера без появления диалогового окна с ошибками

        //BrowserPDF.Navigated += (sender, args) => {
        //    HideScriptErrors((WebBrowser)sender, true);
        //};
        //BrowserPDF.Navigate(@"https://google.com/");

        //public void HideScriptErrors(WebBrowser wb, bool Hide)
        //{
        //    FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
        //    if (fiComWebBrowser == null) return;
        //    object objComWebBrowser = fiComWebBrowser.GetValue(wb);
        //    if (objComWebBrowser == null) return;
        //    objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        //}

        #endregion
    }
}
