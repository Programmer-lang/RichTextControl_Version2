using DevExpress.Xpf.Core;
using RichTextEditor.Utilis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Utils;


namespace RichTextEditor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RichTextEditor: UserControl
    {

        #region properties

        public static bool bDisableSet { get; private set; } = false;
        public static bool bDisableTextChange { get; private set; } = false;

        public IEnumerable<double?> FontSizes { get; protected set; }
        #endregion

        #region BindableProperties

        #region RelatedToFont

        
        
        //public new FontFamily FontFamily
        //{
        //    get { return (FontFamily)GetValue(FontFamilyProperty); }
        //    set { SetValue(FontFamilyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for FontFamily.  This enables animation, styling, binding, etc...
        //public  new static readonly DependencyProperty FontFamilyProperty =
        //    DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(RichTextEditor), new PropertyMetadata(true, new PropertyChangedCallback(OnFontPropertyChanged)));

        //private static void OnFontPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var control = (RichTextEditor)d;

        //    control.richControl.FontFamily = (FontFamily)e.NewValue;
        //}



      
        #endregion

        #region RelatedToData

        public static readonly DependencyProperty TextProperty =
         DependencyProperty.Register("Text", typeof(string), typeof(RichTextEditor), new PropertyMetadata(new PropertyChangedCallback(OnTextChanged)));
      

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);

            }
        }

        public static readonly DependencyProperty RtfTextProperty =
         DependencyProperty.Register("RtfText", typeof(string), typeof(RichTextEditor), new PropertyMetadata(new PropertyChangedCallback(OnRTFTextChanged)));


        public string RtfText
        {
            get { return (string)GetValue(RtfTextProperty); }
            set { SetValue(RtfTextProperty, value); }
        }


        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RichTextEditor)d;

            if (!bDisableSet)
            {
                bDisableTextChange = true;
                RichTextBoxExtensions.SetText(control.richControl.Document, e.NewValue?.ToString());
                bDisableTextChange = false;
            }
        }

        private static void OnRTFTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RichTextEditor)d;

            if (!bDisableSet && (e.NewValue != null))
            {
                bDisableTextChange = true;

                RichTextBoxExtensions.SetRtfText(control.richControl.Document, e.NewValue?.ToString());
                bDisableTextChange = false;
            }
        }

        private void RtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!bDisableTextChange)
            {
                bDisableSet = true;
                Text = RichTextBoxExtensions.GetText(richControl.Document);
                RtfText = RichTextBoxExtensions.GetRtfText(richControl.Document);
                bDisableSet = false;
            }
        }


        #endregion

        #region RelatedToLayout

        public bool IsToolBarVisible
        {
            get { return (bool)GetValue(IsToolBarVisibleProperty); }
            set { SetValue(IsToolBarVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsToolBarVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsToolBarVisibleProperty =
            DependencyProperty.Register("IsToolBarVisible", typeof(bool), typeof(RichTextEditor), new PropertyMetadata(true, new PropertyChangedCallback(OnIsToolBarVisiblePropertyChanged)));


        private static void OnIsToolBarVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RichTextEditor)d;

            if ((bool)e.NewValue == true)
                control.toolBar.Visibility = Visibility.Visible;
            else
                control.toolBar.Visibility = Visibility.Collapsed;
        }

        public bool AllowEditing
        {
            get { return (bool)GetValue(AllowEditingProperty); }
            set { SetValue(AllowEditingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowEditing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowEditingProperty =
            DependencyProperty.Register("AllowEditing", typeof(bool), typeof(RichTextEditor), new PropertyMetadata(true, new PropertyChangedCallback(OnAllowEditingChanged)));


        private static void OnAllowEditingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RichTextEditor)d;

            if ((bool)e.NewValue == true)
            {
                control.richControl.IsReadOnly = false;
                control.richControl.IsEnabled = true;
            }
            else
            { 
                control.richControl.IsReadOnly = true;
                control.richControl.IsEnabled = false;
            }
        }

          
        
        #endregion

        #endregion
        public RichTextEditor()
        {
            FontSizes = new double?[] {null, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30,
                    32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144
                };

            InitializeComponent();

           // richControl.fon = new Font("Tahoma", 12, FontStyle.Bold);

            FontFamily = new FontFamily("Arial");
            FontSize = 12 * 1.3333334;



            Loaded += OnModuleLoaded;

            this.DataContext = this;

            var lang = System.Windows.Forms.InputLanguage.InstalledInputLanguages;
            System.Windows.Forms.InputLanguage inputLang = null;

            foreach (System.Windows.Forms.InputLanguage item in lang)
            {
                if(item.Culture.Name.Contains("ar"))
                {
                    inputLang = item;
                    break;
                }
            }

            if(inputLang != null)

                    System.Windows.Forms.InputLanguage.CurrentInputLanguage = inputLang;

            

           
        }

        //#region |Commands|
        //    private void BoldCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        //    {
        //        this.SelectionIsBold = (true == this.m_rbtbBold.IsChecked);
        //        //this.m_RTB.Focus();
        //    }

        //#endregion



        void OnModuleLoaded(object sender, RoutedEventArgs e)
        {
                
            

                richControl.SetFocus();

           // FontSize = FontSize * 1.3333334;


        }

        private void RichControl_MouseMove(object sender, MouseEventArgs e)
        {
            RichTextBox richTextBox = (sender as RichControl);

           var tables= richTextBox?.Document?.Blocks?.Where(x => x is Table)?.ToList();

            //if(Mouse.Cursor == newCursor)



            //if (tables != null)
            //{
            //    foreach (var table in tables)
            //    {
            //        if (table.ro)
            //        //table.hit
            //        {
            //            //    Point p = e.GetPosition(this);
            //            //    Point q = Mouse.GetPosition(table);

            //            table.Cursor = Helper.newCursor;

            //            //Console.WriteLine("p x:{0} y:{1}", p.X, p.Y);
            //            //Console.WriteLine("q x:{0} y:{1}", q.X, q.Y);
            //        }
            //    }
            //}


        }

        //private void RichControl_MouseEnte(object sender, MouseEventArgs e)
        //{

        //}
    }

    public class EditWidthConverter : MarkupExtension, IValueConverter
    {
        public double EditWidth { get; set; }
        public double TouchScaleFactor { get; set; }

        public EditWidthConverter() { TouchScaleFactor = 2d; }
        public override object ProvideValue(IServiceProvider serviceProvider) { return this; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var walker = value as ThemeTreeWalker;
            if (walker != null && (walker.IsTouch || walker.ThemeName == Theme.TouchlineDarkName))
                return EditWidth * TouchScaleFactor;
            return EditWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }

      
    }
}
