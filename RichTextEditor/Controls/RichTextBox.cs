using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Commands;
using Microsoft.Win32;
using System.IO;
using System.Globalization;
using RichTextEditor.Utilis;
using SautinSoft.Document;
using Color = System.Windows.Media.Color;
using Run = System.Windows.Documents.Run;
using Paragraph = System.Windows.Documents.Paragraph;
using Block = System.Windows.Documents.Block;
using Inline = System.Windows.Documents.Inline;
using ListItem = System.Windows.Documents.ListItem;
using BlockCollection = System.Windows.Documents.BlockCollection;
using RichTextEditor;
using DevExpress.Xpf.Dialogs;
using System.Windows.Controls;
using System.Linq;
using System.Collections;

namespace Utils {
    public class RichControl : System.Windows.Controls.RichTextBox {
        #region dependency properties
        public static readonly DependencyProperty IsBoldProperty = DependencyProperty.Register("IsBold", typeof(bool?), typeof(RichControl), new PropertyMetadata(null, new PropertyChangedCallback(OnIsBoldPropertyChanged)));
        public static readonly DependencyProperty IsItalicProperty = DependencyProperty.Register("IsItalic", typeof(bool?), typeof(RichControl), new PropertyMetadata(null, new PropertyChangedCallback(OnIsItalicPropertyChanged)));
        public static readonly DependencyProperty IsUnderlineProperty = DependencyProperty.Register("IsUnderline", typeof(bool?), typeof(RichControl), new PropertyMetadata(null, new PropertyChangedCallback(OnIsUnderlinePropertyChanged)));
        public static readonly DependencyProperty SelectionTextProperty = DependencyProperty.Register("SelectionText", typeof(string), typeof(RichControl), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectionFontFamilyProperty = DependencyProperty.Register("SelectionFontFamily", typeof(string), typeof(RichControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectionFontFamilyPropertyChanged)));
        public static readonly DependencyProperty SelectionFontSizeProperty = DependencyProperty.Register("SelectionFontSize", typeof(double?), typeof(RichControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectionFontSizePropertyChanged)));
        public static readonly DependencyProperty SelectionTextColorProperty = DependencyProperty.Register("SelectionTextColor", typeof(Color), typeof(RichControl), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(OnSelectionTextColorPropertyChanged)));
        public static readonly DependencyProperty SelectionTextBackgroundColorProperty = DependencyProperty.Register("SelectionTextBackgroundColor", typeof(Color), typeof(RichControl), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(OnSelectionTextBackgroundColorPropertyChanged)));
        public static readonly DependencyProperty IsRightAlignmentProperty = DependencyProperty.Register("IsRightAlignment", typeof(bool), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsRightAlignmentPropertyChanged)));
        public static readonly DependencyProperty IsCenterAlignmentProperty = DependencyProperty.Register("IsCenterAlignment", typeof(bool), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCenterAlignmentPropertyChanged)));
        public static readonly DependencyProperty IsLeftAlignmentProperty = DependencyProperty.Register("IsLeftAlignment", typeof(bool), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsLeftAlignmentPropertyChanged)));
        public static readonly DependencyProperty IsJustifyAlignmentProperty = DependencyProperty.Register("IsJustifyAlignment", typeof(bool), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsJustifyAlignmentPropertyChanged)));

        public static readonly DependencyProperty IsLeftToRightProperty = DependencyProperty.Register("IsLeftToRight", typeof(bool), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsLeftToRightPropertyChanged)));
        public static readonly DependencyProperty IsRightToLeftProperty = DependencyProperty.Register("IsRightToLeft", typeof(bool), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsRightToLeftPropertyChanged)));



        public static readonly DependencyProperty IsEmptyProperty = DependencyProperty.Register("IsEmpty", typeof(bool), typeof(RichControl), new PropertyMetadata(true, new PropertyChangedCallback(OnIsEmptyPropertyChanged)));


        public static readonly DependencyProperty IsSelectionEmptyProperty = DependencyProperty.Register("IsSelectionEmpty", typeof(bool), typeof(RichControl), new PropertyMetadata(true));
      //  public static readonly DependencyProperty ListMarkerStyleProperty = DependencyProperty.Register("ListMarkerStyle", typeof(TextMarkerStyle), typeof(RichControl), new PropertyMetadata(TextMarkerStyle.None, new PropertyChangedCallback(OnListMarkerStylePropertyChanged)));
        public static readonly DependencyProperty NumberMarkerStyleProperty = DependencyProperty.Register("NumberMarkerStyle", typeof(TextMarkerStyle), typeof(RichControl), new PropertyMetadata(TextMarkerStyle.None, new PropertyChangedCallback(OnNumberMarkerStylePropertyChanged)));


        public static readonly DependencyProperty ContainerProperty = DependencyProperty.Register("Container", typeof(InlineUIContainer), typeof(RichControl), new PropertyMetadata(null, new PropertyChangedCallback(OnContainerPropertyChanged)));
        public static readonly DependencyProperty IsListProperty = DependencyProperty.Register("IsList", typeof(bool?), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsListPropertyChanged)));
        public static readonly DependencyProperty IsNumberedProperty = DependencyProperty.Register("IsNumbered", typeof(bool?), typeof(RichControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsNumberedPropertyChanged)));



        public static readonly DependencyProperty RowProperty = DependencyProperty.Register("Row", typeof(int), typeof(RichControl), new PropertyMetadata(1));
        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register("Column", typeof(int), typeof(RichControl), new PropertyMetadata(1));

        protected static void OnIsBoldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsBoldChanged();
        }
        protected static void OnIsItalicPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsItalicChanged();
        }
        protected static void OnIsUnderlinePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsUnderlineChanged();
        }
        protected static void OnSelectionFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnSelectionFontFamilyChanged();
        }
        protected static void OnSelectionFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnSelectionFontSizeChanged();
        }
        protected static void OnSelectionTextColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnSelectionTextColorChanged();
        }
        protected static void OnSelectionTextBackgroundColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnSelectionTextBackgroundColorChanged();
        }
        protected static void OnIsRightAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsRightAlignmentChanged();
        }
        protected static void OnIsLeftAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsLeftAlignmentChanged();
        }
        protected static void OnIsJustifyAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichControl)d).OnIsJustifyAlignmentChanged();
        }

        protected static void OnIsLeftToRightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichControl)d).OnIsLeftToRightChanged();
        }

        protected static void OnIsRightToLeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichControl)d).OnIsRightToLeftChanged();
        }
        protected static void OnIsCenterAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsCenterAlignmentChanged();
        }
        //protected static void OnListMarkerStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        //    ((RichControl)d).OnListMarkerStyleChanged();
        //}

        protected static void OnNumberMarkerStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichControl)d).OnNumberMarkerStyleChanged();
        }

        protected static void OnContainerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnContainerChanged();
        }
        protected static void OnIsEmptyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsEmptyChanged();
        }
        protected static void OnIsListPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichControl)d).OnIsListChanged();
        }


        protected static void OnIsNumberedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichControl)d).OnIsNumberedChanged();
        }


        public bool? IsBold {
            get { return (bool?)GetValue(IsBoldProperty); }
            set { SetValue(IsBoldProperty, value); }
        }
        public bool? IsItalic {
            get { return (bool?)GetValue(IsItalicProperty); }
            set { SetValue(IsItalicProperty, value); }
        }
        public bool? IsUnderline {
            get { return (bool?)GetValue(IsUnderlineProperty); }
            set { SetValue(IsUnderlineProperty, value); }
        }
        public string SelectionText {
            get { return (string)GetValue(SelectionTextProperty); }
            set { SetValue(SelectionTextProperty, value); }
        }
        public string SelectionFontFamily {
            get { return (string)GetValue(SelectionFontFamilyProperty); }
            set { SetValue(SelectionFontFamilyProperty, value); }
        }
        public double? SelectionFontSize {
            get { return (double?)GetValue(SelectionFontSizeProperty); }
            set { SetValue(SelectionFontSizeProperty, value); }
        }
        public Color SelectionTextColor {
            get { return (Color)GetValue(SelectionTextColorProperty); }
            set { SetValue(SelectionTextColorProperty, value); }
        }
        public Color SelectionTextBackgroundColor {
            get { return (Color)GetValue(SelectionTextBackgroundColorProperty); }
            set { SetValue(SelectionTextBackgroundColorProperty, value); }
        }
        public bool IsRightAlignment {
            get { return (bool)GetValue(IsRightAlignmentProperty); }
            set { SetValue(IsRightAlignmentProperty, value); }
        }

        public bool IsRightToLeft
        {
            get { return (bool)GetValue(IsRightToLeftProperty); }
            set { SetValue(IsRightToLeftProperty, value); }
        }

        public bool IsLeftToRight
        {
            get { return (bool)GetValue(IsLeftToRightProperty); }
            set { SetValue(IsLeftToRightProperty, value); }
        }


        public bool IsCenterAlignment {
            get { return (bool)GetValue(IsCenterAlignmentProperty); }
            set { SetValue(IsCenterAlignmentProperty, value); }
        }
        public bool IsLeftAlignment {
            get { return (bool)GetValue(IsLeftAlignmentProperty); }
            set { SetValue(IsLeftAlignmentProperty, value); }
        }
        public bool IsJustifyAlignment
        {
            get { return (bool)GetValue(IsJustifyAlignmentProperty); }
            set { SetValue(IsJustifyAlignmentProperty, value); }
        }
        public bool IsEmpty {
            get { return (bool)GetValue(IsEmptyProperty); }
            set { SetValue(IsEmptyProperty, value); }
        }
        public bool IsSelectionEmpty {
            get { return (bool)GetValue(IsSelectionEmptyProperty); }
            set { SetValue(IsSelectionEmptyProperty, value); }
        }
        //public TextMarkerStyle ListMarkerStyle {
        //    get { return (TextMarkerStyle)GetValue(ListMarkerStyleProperty); }
        //    set { SetValue(ListMarkerStyleProperty, value); }
        //}

        public TextMarkerStyle NumberMarkerStyle
        {
            get { return (TextMarkerStyle)GetValue(NumberMarkerStyleProperty); }
            set { SetValue(NumberMarkerStyleProperty, value); }
        }

        public InlineUIContainer Container {
            get { return (InlineUIContainer)GetValue(ContainerProperty); }
            set { SetValue(ContainerProperty, value); }
        }
        public bool? IsList {
            get { return (bool?)GetValue(IsListProperty); }
            set { SetValue(IsListProperty, value); }
        }

        public bool? IsNumbered
        {
            get { return (bool?)GetValue(IsNumberedProperty); }
            set { SetValue(IsNumberedProperty, value); }
        }

        public int Row {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }
        public int Column {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }
        #endregion
        #region commands
        public ICommand SelectAllCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public ICommand OpenFileCommand { get; set; }
        public ICommand ExportFileCommand { get; set; }
        public ICommand InsertTableCommand { get; set; }

        public ICommand InsertRowAboveCommand { get; set; }
        public ICommand InsertRowBelowCommand { get; set; }
        public ICommand InsertColumnLeftCommand { get; set; }
        public ICommand InsertColumnRightCommand { get; set; }
        public ICommand DeleteRowsCommand { get; set; }
        public ICommand DeleteColumnsCommand { get; set; }

        public ICommand IndentIncreaseCommand { get; set; }
        public ICommand IndentDecreaseCommand { get; set; }
        public ICommand IncreaseFontSizeCommand { get; set; }
        public ICommand DecreaseFontSizeCommand { get; set; }

        public ICommand PrintCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand PasteCommand { get; set; }
        public ICommand CutCommand { get; set; }
        public ICommand ClearSelectionCommand { get; set; }
        #endregion

        public RichControl() {
            ClearSelectionCommand = new DelegateCommand(ClearSelectionCommandExecute, CanClearSelectionCommandExecute);
            ClearCommand = new DelegateCommand(ClearCommandExecute, CanClearCommandExecute);

            OpenFileCommand = new DelegateCommand(OpenFileCommandExecute, CanOpenFileCommandExecute);

            ExportFileCommand = new DelegateCommand(ExportFileCommandExecute, CanExportFileCommandExecute);

            InsertTableCommand = new DelegateCommand(InsertTableCommandExecute, CanInsertTableCommandExecute);

            InsertRowAboveCommand = new DelegateCommand(InsertRowAboveCommandExecute, CanInsertRowAboveCommandExecute);
            InsertRowBelowCommand = new DelegateCommand(InsertRowBelowCommandExecute, CanInsertRowBelowCommandExecute);
            InsertColumnLeftCommand = new DelegateCommand(InsertColumnLeftCommandExecute, CanInsertColumnLeftCommandExecute);
            InsertColumnRightCommand = new DelegateCommand(InsertColumnRightCommandExecute, CanInsertColumnRightCommandExecute);
            DeleteRowsCommand = new DelegateCommand(DeleteRowsCommandExecute, CanDeleteRowsCommandExecute);
            DeleteColumnsCommand = new DelegateCommand(DeleteColumnsCommandExecute, CanDeleteColumnsCommandExecute);


            IndentIncreaseCommand = new DelegateCommand(IndentIncreaseCommandExecute, CanIndentIncreaseCommandExecute);
            IndentDecreaseCommand = new DelegateCommand(IndentDecreaseCommandExecute, CanIndentDecreaseCommandExecute);
            IncreaseFontSizeCommand = new DelegateCommand(IncreaseFontSizeCommandExecute, CanIncreaseFontSizeCommandExecute);
            DecreaseFontSizeCommand = new DelegateCommand(DecreaseFontSizeCommandExecute, CanDecreaseFontSizeCommandExecute);

            



            PrintCommand = new DelegateCommand(PrintCommandExecute);
            CopyCommand = ApplicationCommands.Copy;
            PasteCommand = ApplicationCommands.Paste;
            CutCommand = ApplicationCommands.Cut;
            SelectAllCommand = ApplicationCommands.SelectAll;

            //SetValue(Paragraph.LineHeight, 3.0);

            //  FlowDirection = FlowDirection.LeftToRight;

            // ReadingDirectionCore = FlowDirection.RightToLeft;
            //TextAlignmentCore = TextAlignment.Right;

            //IsRightToLeft = true;

            //IsRightAlignment = true;
            Paragraph p = Document.Blocks.FirstBlock as Paragraph;
            p.LineHeight = 10;

            //TextPointer myTextPointer1 = Document.ContentStart;
            //TextPointer myTextPointer2 = Document.ContentEnd;

            ////// Programmatically change the selection in the RichTextBox.  
            //Selection.Select(myTextPointer1, myTextPointer2);

            //SetFocus();

        }


        public void SetFocus() {
            this.Focus();
            UpdateSelectionProperties();
        }
        public event EventHandler ContainerChanged;

        bool IsUpdating { get; set; }
        protected void OnIsBoldChanged() { if(!IsUpdating) IsBoldCore = IsBold; }
        protected void OnIsItalicChanged() { if(!IsUpdating) IsItalicCore = IsItalic; }
        protected void OnIsUnderlineChanged() { if(!IsUpdating) IsUnderlineCore = IsUnderline; }
        protected void OnSelectionFontFamilyChanged()
        {
            if (!IsUpdating)
            {

                this.Focus();

                SelectionFontFamilyCore = SelectionFontFamily;
                

            }
        }
        protected void OnSelectionFontSizeChanged()
        {
            if (!IsUpdating)
            {
                this.Focus();
                SelectionFontSizeCore = SelectionFontSize;

            }
        }
        protected void OnSelectionTextBackgroundColorChanged() { if(!IsUpdating) SelectionTextBackgroundColorCore = SelectionTextBackgroundColor; }
        protected void OnSelectionTextColorChanged() { if(!IsUpdating) SelectionTextColorCore = SelectionTextColor; }
        protected void OnIsRightAlignmentChanged() {
            if(IsUpdating) return;
            if(IsRightAlignment) {
                IsLeftAlignment = false;
                IsCenterAlignment = false;
                IsJustifyAlignment = false;
                TextAlignmentCore = TextAlignment.Right;
            }
        }
        protected void OnContainerChanged() {
            if(ContainerChanged != null)
                ContainerChanged(this, new EventArgs());
        }
        //protected void OnListMarkerStyleChanged() {
        //    if(!IsUpdating) {
        //        ListMarkerStyleCore = ListMarkerStyle;
        //        IsUpdating = true;
        //        IsList = ListMarkerStyle != TextMarkerStyle.None;
        //        IsUpdating = false;
        //    } else {
        //        IsList = ListMarkerStyle != TextMarkerStyle.None;
        //    }
        //}

        protected void OnNumberMarkerStyleChanged()
        {
            if (!IsUpdating)
            {
                NumberMarkerStyleCore = NumberMarkerStyle;
                IsUpdating = true;
                IsNumbered = NumberMarkerStyle == TextMarkerStyle.Decimal;

                IsList = NumberMarkerStyle == TextMarkerStyle.Disc;
                IsUpdating = false;
            }
            else
            {
                IsNumbered = NumberMarkerStyle == TextMarkerStyle.Decimal;
                IsList = NumberMarkerStyle == TextMarkerStyle.Disc;
            }
        }
        protected void OnIsListChanged() {
            if(IsUpdating) return;
            //if (IsNumbered == true)
            //    ListMarkerStyle = IsNumbered.Value ? TextMarkerStyle.Disc : TextMarkerStyle.Decimal;
            //else
                NumberMarkerStyle = IsList.Value ? TextMarkerStyle.Disc : TextMarkerStyle.None;
            //if (IsList == true)
            //{
            //   IsUpdating = true;
            //    NumberMarkerStyle = TextMarkerStyle.None;
            //   // IsNumbered = false;
            //    IsUpdating = false;
            //}

        }

        protected void OnIsNumberedChanged()
        {
            if (IsUpdating) return;

            //if (IsList == true)
            //    NumberMarkerStyle = IsNumbered.Value ? TextMarkerStyle.Decimal : TextMarkerStyle.Disc;
            //else
                NumberMarkerStyle = IsNumbered.Value ? TextMarkerStyle.Decimal : TextMarkerStyle.None;

            //if (IsNumbered == true)
            //{
            //    IsUpdating = true;
            //    IsList = false;
            //   // ListMarkerStyle = TextMarkerStyle.None;
            //    IsUpdating = false;
            //}
        }


        protected void OnIsEmptyChanged() {
            ((DelegateCommand)ClearCommand).RaiseCanExecuteChanged();
        }
        protected void OnIsLeftAlignmentChanged() {
            if(IsUpdating) return;
            if(IsLeftAlignment) {
                IsRightAlignment = false;
                IsCenterAlignment = false;
                IsJustifyAlignment = false;
                TextAlignmentCore = TextAlignment.Left;
            }
        }

        protected void OnIsJustifyAlignmentChanged()
        {
            if (IsUpdating) return;
            if (IsJustifyAlignment)
            {
                IsRightAlignment = false;
                IsCenterAlignment = false;
                IsLeftAlignment = false;
                TextAlignmentCore = TextAlignment.Justify;
            }
        }
        protected void OnIsCenterAlignmentChanged() {
            if(IsUpdating) return;
            if(IsCenterAlignment) {
                IsLeftAlignment = false;
                IsRightAlignment = false;
                IsJustifyAlignment = false;
                TextAlignmentCore = TextAlignment.Center;
            }
        }

        protected void OnIsLeftToRightChanged()
        {
            if (IsUpdating) return;
            if (IsLeftToRight)
            {
                IsRightToLeft = false;
               // ReadingDirectionCore = FlowDirection.LeftToRight;
                IsUpdating = true;
                TextAlignmentCore = TextAlignment.Left;
                IsLeftAlignment = true;

               // FlowDirection = FlowDirection.LeftToRight;
                Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.FlowDirectionProperty, FlowDirection.RightToLeft);
                 IsUpdating = false;
            }
        }

        protected void OnIsRightToLeftChanged()
        {
            if (IsUpdating) return;
            if (IsRightToLeft)
            {
                IsLeftToRight = false;
              //  ReadingDirectionCore = FlowDirection.RightToLeft;
                IsUpdating = true;
                TextAlignmentCore = TextAlignment.Right;
                IsRightAlignment = true;
                 IsUpdating = false;
                //FlowDirection = FlowDirection.RightToLeft;
                Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.FlowDirectionProperty, FlowDirection.LeftToRight);

            }
        }


        protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e) {
            


            base.OnKeyUp(e);
            UpdateSelectionProperties();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            if (e.Key == Key.Tab)
            {
                //var paragraph = this.CaretPosition.Paragraph;

                //var run = paragraph.Inlines.FirstInline as Run;

                //if (run != null)
                //{

                //    TableCell cell = paragraph.Parent as TableCell;

                //    if (cell != null)
                //    {

                //        if(tableRowGroup.Rows.IndexOf(tableRow))
                //    }
                //}

                TextPointer insertionPosition = Selection.Start;

                TableCell tableCell = Helper.GetTableCellAncestor(insertionPosition);
                if (tableCell == null)
                {
                    return;
                }


                TableRow tableRow = tableCell.Parent as TableRow;
                if (tableRow == null)
                {
                    return;
                }

                TableRowGroup tableRowGroup = tableRow.Parent as TableRowGroup;
                if (tableRowGroup == null)
                {
                    return;
                }
                //Table table = (Table)tableRowGroup.Parent;

                int rowIndex = tableRowGroup.Rows.IndexOf(tableRow);
                int columnIndex = tableRow.Cells.IndexOf(tableCell);

                if (columnIndex == tableRow.Cells.Count - 1 && rowIndex == tableRowGroup.Rows.Count - 1)
                {
                    InsertRowBelow();
                }


            }

            base.OnPreviewKeyDown(e);
        }
        protected override void OnSelectionChanged(RoutedEventArgs e) {
            base.OnSelectionChanged(e);
            UpdateSelectionProperties();
            UpdateCaretPosInfo();
        }
        protected void UpdateCaretPosInfo() {
            int actualLineCount = 0;
            CaretPosition.GetLineStartPosition(int.MinValue, out actualLineCount);
            Row = 1 - actualLineCount;
            Column = Math.Max(1, CaretPosition.GetLineStartPosition(0).GetOffsetToPosition(CaretPosition));
        }
        protected void UpdateSelectionProperties() {
            IsUpdating = true;
            IsSelectionEmpty = IsSelectionEmptyCore;
            NumberMarkerStyle = NumberMarkerStyleCore;

            if (FlowDirection == FlowDirection.LeftToRight)
            {
                IsRightAlignment = object.Equals(TextAlignmentCore, TextAlignment.Right);
                IsLeftAlignment = object.Equals(TextAlignmentCore, TextAlignment.Left);
                IsLeftToRight = object.Equals(TextAlignmentCore, TextAlignment.Left);
                IsRightToLeft = object.Equals(TextAlignmentCore, TextAlignment.Right);
            }
            else
            {
                if (NumberMarkerStyle == TextMarkerStyle.None)
                {
                    IsRightAlignment = object.Equals(TextAlignmentCore, TextAlignment.Left);
                    IsLeftAlignment = object.Equals(TextAlignmentCore, TextAlignment.Right);
                    IsLeftToRight = object.Equals(TextAlignmentCore, TextAlignment.Right);
                    IsRightToLeft = object.Equals(TextAlignmentCore, TextAlignment.Left);
                }
                else
                {
                    IsRightAlignment = object.Equals(TextAlignmentCore, TextAlignment.Right);
                    IsLeftAlignment = object.Equals(TextAlignmentCore, TextAlignment.Left);
                    IsLeftToRight = object.Equals(TextAlignmentCore, TextAlignment.Left);
                    IsRightToLeft = object.Equals(TextAlignmentCore, TextAlignment.Right);
                }
            }


            IsJustifyAlignment = object.Equals(TextAlignmentCore, TextAlignment.Justify);
            IsCenterAlignment = object.Equals(TextAlignmentCore, TextAlignment.Center);

            

            IsBold = IsBoldCore;
            IsItalic = IsItalicCore;
            IsUnderline = IsUnderlineCore;
            SelectionFontSize = SelectionFontSizeCore;
            SelectionFontFamily = SelectionFontFamilyCore;
            SelectionTextColor = SelectionTextColorCore;
            SelectionTextBackgroundColor = SelectionTextBackgroundColorCore;
            Container = GetUIElementUnderSelection();
          //  ListMarkerStyle = ListMarkerStyleCore;
            
            IsEmpty = IsEmptyCore;
            IsSelectionEmpty = IsSelectionEmptyCore;
            IsUpdating = false;
        }
        public void Clear() {
            ClearCommandExecute();
        }

        public void OpenFile()
        {
            OpenFileCommandExecute();
        }

        public void ExportFile()
        {
            ExportFileCommandExecute();
        }

        public void InsertTable()
        {
            InsertTableCommandExecute();
        }

        public void InsertRowAbove()
        {
            InsertRowAboveCommandExecute();
        }

        public void InsertRowBelow()
        {
            InsertRowBelowCommandExecute();
        }
        public void InsertColumnLeft()
        {
            InsertColumnLeftCommandExecute();
        }
        public void InsertColumnRight()
        {
            InsertColumnRightCommandExecute();
        }
        public void DeleteRowsTable()
        {
            DeleteRowsCommandExecute();
        }
        public void DeleteColumns()
        {
            DeleteColumnsCommandExecute();
        }

        public void IndentIncrease()
        {
            IndentIncreaseCommandExecute();
        }

        public void IndentDecrease()
        {
            IndentDecreaseCommandExecute();
        }

        public void IncreaseFontSize()
        {
            IncreaseFontSizeCommandExecute();
        }

        public void DecreaseFontSize()
        {
            DecreaseFontSizeCommandExecute();
        }

        #region commands implementation

        protected bool CanIndentIncreaseCommandExecute() { return true; }
        protected void IndentIncreaseCommandExecute()
        {

            //Paragraph p = Selection.Start.Paragraph;
            //if (p == null)
            //    return;
            EditingCommands.IncreaseIndentation.Execute(null, this);
        }

        protected bool CanIndentDecreaseCommandExecute() { return true; }
        protected void IndentDecreaseCommandExecute()
        {
            EditingCommands.DecreaseIndentation.Execute(null, this);
        }

        protected bool CanIncreaseFontSizeCommandExecute() { return true; }
        protected void IncreaseFontSizeCommandExecute()
        {
            EditingCommands.IncreaseFontSize.Execute(null, this);
            SelectionFontSize = SelectionFontSizeCore;
        }

        protected bool CanDecreaseFontSizeCommandExecute() { return true; }
        protected void DecreaseFontSizeCommandExecute()
        {
            EditingCommands.DecreaseFontSize.Execute(null, this);
            SelectionFontSize = SelectionFontSizeCore;
        }

        protected bool CanClearSelectionCommandExecute() { return !Selection.IsEmpty; }
        protected void ClearSelectionCommandExecute() {
            Selection.Select(Selection.Start, Selection.Start);            
        }
        protected bool CanClearCommandExecute() { return !IsEmpty; }
        protected void ClearCommandExecute() {
            (Document as FlowDocument).Blocks.Clear();
        }

        protected bool CanOpenFileCommandExecute() { return true; }
        protected void OpenFileCommandExecute()
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "Document files (*.doc)|*.docx";
            //var result = dlg.ShowDialog();
            //if (result.Value)
            //{
            //    TextRange t = new TextRange(Document.ContentStart, Document.ContentEnd);
            //    FileStream file = new FileStream(dlg.FileName, FileMode.Open);
            //    t.Load(file, System.Windows.DataFormats.Text);
            //}

            //Microsoft.Win32.OpenFileDialog OpenFiledlg = new Microsoft.Win32.OpenFileDialog();
            //OpenFiledlg.DefaultExt = "All files (*.*)|*.*";
            ////OpenFiledlg.Filter = "RTF Files(*.rtf)|*.rtf |All files (*.*)|*.*";
            //OpenFiledlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            //if (OpenFiledlg.ShowDialog() == true & OpenFiledlg.FileName.Length > 0)
            //{
            //    FileStream fileStream = new FileStream(OpenFiledlg.FileName, FileMode.Open);
            //    TextRange range = new TextRange(Document.ContentStart, Document.ContentEnd);
            //    range.Load(fileStream, System.Windows.DataFormats.Rtf);
            //}

            var openDialog = GetOpenDialog();
            if (openDialog.ShowDialog().Value)
            {
            //    foreach (var file in openDialog.FileNames)
            //    {
            //        Photos.Add(ImageHelper.GetJPGPhoto(file));
            //        CopyPhoto(file, GalleryDirectory + "\\" + Path.GetFileName(file));
            //    }

            //}

            //    OpenFileDialog openFileDialog = new OpenFileDialog();
            

            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //openFileDialog.Multiselect = false;
            //openFileDialog.Title = "open file";
            //openFileDialog.Filter = "Rich Text Format (*.rtf,*doc,*.docx)|*.rtf;*doc;*.docx";

            //if (true == openFileDialog.ShowDialog())
            //{
                try
                { 
                string fileName = openDialog.FileName;

                string ext = Path.GetExtension(fileName);

                    if (ext == ".rtf")
                        LoadRTF(fileName);
                    else
                        LoadDocFile(fileName);

                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("failed to open file", "open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }


        DXOpenFileDialog GetOpenDialog()
        {
            return new DXOpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Rich Text Format (*.rtf,*.docx)|*.rtf;*.docx",
                Title = "Choose file",
                Multiselect = false,
            };
        }


        private void LoadDocFile(string inpFile)
        {


           byte[] inpData = File.ReadAllBytes(inpFile);
            

            TextRange textRange = new TextRange(this.Document.ContentStart, this.Document.ContentEnd);

            using (MemoryStream msInp = new MemoryStream(inpData))
            {

                // Load a document.
                DocumentCore dc = DocumentCore.Load(msInp, new DocxLoadOptions());

                // Save the document to PDF format.
                using (MemoryStream outMs = new MemoryStream())
                {
                    dc.Save(outMs, new RtfSaveOptions());

                    textRange.Load(outMs, DataFormats.Rtf);


                }
            }
        }
        private void LoadRTF(string fileName)
        {
            Stream fileStream = null;
            string strDocument = String.Empty;

            if ((fileStream = File.Open(fileName, FileMode.Open)) != null)
            {
                using (fileStream)
                {
                    StreamReader streamReader = new StreamReader(fileStream);

                    streamReader.BaseStream.Position = 0;
                    strDocument = streamReader.ReadToEnd();




                    TextRange textRange = new TextRange(this.Document.ContentStart, this.Document.ContentEnd);

                    using (MemoryStream rtfMemoryStream = new MemoryStream())
                    {
                        using (StreamWriter rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                        {
                            rtfStreamWriter.Write(strDocument);
                            rtfStreamWriter.Flush();
                            rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                            //### Load the MemoryStream into TextRange ranging from start to end of RichTextBox.
                            textRange.Load(rtfMemoryStream, DataFormats.Rtf);
                        }
                    }
                }
            }
        }

        protected bool CanExportFileCommandExecute() { return true; }
        protected void ExportFileCommandExecute()
        {
            try
            {

                //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                //dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                //if (dlg.ShowDialog() == true)
                //{
                //    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                //    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                //    range.Save(fileStream, System.Windows.Forms.DataFormats.Rtf);
                //}

                SaveFileDialog savefile = new SaveFileDialog();
                //// set a default file name  
                //savefile.FileName = "unknown.doc";
                //// set filters - this can be done in properties as well  
                //savefile.Filter = "Document files (*.rtf)|*.rtf";
                //if (savefile.ShowDialog() == true)
                //{

                    var saveDialog = GetSaveDialog();
                    if (saveDialog.ShowDialog().Value)
                    {
                    // var file = saveDialog.FileName;

                    foreach (Block p in Document.Blocks)
                    {
                        if (p is Paragraph)
                        {
                            p.LineHeight = 25;
                        }
                        else
                        {
                            if (p is Table)
                            {
                                p.FlowDirection = FlowDirection.RightToLeft;
                                p.TextAlignment = TextAlignment.Right;
                            }
                        }
                    }
                   // SetValue(Paragraph.LineHeightProperty, 1.5);

                    TextRange t = new TextRange(Document.ContentStart, Document.ContentEnd);
                    FileStream file = new FileStream(saveDialog.FileName, FileMode.Create);
                    t.Save(file, System.Windows.DataFormats.Rtf);
                    file.Close();

                    // SetValue(Paragraph.LineHeightProperty, 1.0);

                    //Paragraph p = Document.Blocks.FirstBlock as Paragraph;
                    //p.LineHeight = 10;

                    foreach (Block p in Document.Blocks)
                    {
                        if (p is Paragraph)
                        {

                            p.LineHeight = 10;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        DXSaveFileDialog GetSaveDialog()
        {
            return new DXSaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                DefaultExt = "rtf",
                Title = "Save file",
            };
        }



        protected bool CanInsertTableCommandExecute() { return true; }
        protected void InsertTableCommandExecute()
        {
            TableCommands.OnInsertTable(this, null);
        }

        protected bool CanInsertRowAboveCommandExecute() { return true; }
        protected void InsertRowAboveCommandExecute()
        {
            TableCommands.OnInsertRowsAbove(this, null);
        }

        protected bool CanInsertRowBelowCommandExecute() { return true; }
        protected void InsertRowBelowCommandExecute()
        {
            TableCommands.OnInsertRowsBelow(this, null);
        }

        protected bool CanInsertColumnLeftCommandExecute() { return true; }
        protected void InsertColumnLeftCommandExecute()
        {
            TableCommands.OnInsertColumnsToLeft(this, null);
        }

        protected bool CanInsertColumnRightCommandExecute() { return true; }
        protected void InsertColumnRightCommandExecute()
        {
            TableCommands.OnInsertColumnsToRight(this, null);
        }

        protected bool CanDeleteRowsCommandExecute() { return true; }
        protected void DeleteRowsCommandExecute()
        {
            TableCommands.OnDeleteRows(this, null);
        }

        protected bool CanDeleteColumnsCommandExecute() { return true; }
        protected void DeleteColumnsCommandExecute()
        {
            TableCommands.OnDeleteColumns(this, null);
        }

        protected void PrintCommandExecute() {
            System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
            if(dialog.ShowDialog() != true) return;
            dialog.PrintVisual(this, string.Empty);
        }
        #endregion
        #region core properties
        public string SelectionFontFamilyCore {
            get {

                

                object value = Selection.GetPropertyValue(Paragraph.FontFamilyProperty);

                if (value == DependencyProperty.UnsetValue)
                    return string.Empty;
                else
                {
                    //if (!IsUpdating)
                    //{
                    //    if (SelectionFontFamily != null && value != SelectionFontFamily)
                    //        try
                    //        {
                    //            Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, SelectionFontFamily);

                    //        }
                    //        catch { }

                    //    return SelectionFontFamily;
                    //}

                    return value.ToString();
                }
            }
            set {
                if(value == null || value == SelectionFontFamilyCore) return;
                try {
                    if(value is string)
                        Selection.ApplyPropertyValue(Paragraph.FontFamilyProperty, new FontFamily(value));
                    else
                        Selection.ApplyPropertyValue(Paragraph.FontFamilyProperty, value);
                } catch { }
            }
        }
        public double? SelectionFontSizeCore {
            get {
                object value = Selection.GetPropertyValue(TextElement.FontSizeProperty);
                if(value == DependencyProperty.UnsetValue || value == null)
                    return null;
                else
                {

                   return Math.Round((double)value / 1.3333334, 2);

                }
                
            }
            set {
                if(value == null || value.Equals(SelectionFontSizeCore))
                    return;

                Selection.ApplyPropertyValue(TextElement.FontSizeProperty, Convert.ToDouble(value)* 1.3333334);
            }
        }
        public Color SelectionTextColorCore {
            set {
                if(object.Equals(value, SelectionTextColorCore))
                    return;
                Selection.ApplyPropertyValue(Run.ForegroundProperty, new SolidColorBrush(value));
            }
            get {
                SolidColorBrush brush = Selection.GetPropertyValue(Run.ForegroundProperty) as SolidColorBrush;
                if(brush == null)
                    return Colors.Black;
                return brush.Color;
            }
        }
        public Color SelectionTextBackgroundColorCore {
            set {

                if(value == SelectionTextBackgroundColorCore)
                    return;
                SetTextBackgroundColor(value);
            }
            get {
                SolidColorBrush brush = Selection.GetPropertyValue(Run.BackgroundProperty) as SolidColorBrush;
                if(brush == null)
                    return Colors.Black;
                return brush.Color;
            }
        }
        public void SetTextBackgroundColor(Color value) {
            Selection.ApplyPropertyValue(Run.BackgroundProperty, new SolidColorBrush(value));
        }
        protected TextAlignment TextAlignmentCore {
            get {
                object value = Selection.GetPropertyValue(Paragraph.TextAlignmentProperty);//Selection.GetPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty);
                return value == DependencyProperty.UnsetValue ? TextAlignment.Right : (TextAlignment)value;
            }
            set {
                 Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, value);

                //switch (value)
                //{
                //    case TextAlignment.Left:
                //        EditingCommands.AlignLeft.Execute(null, this);
                //        break;
                //    case TextAlignment.Right:
                //        EditingCommands.AlignRight.Execute(null, this);
                //        break;
                //    case TextAlignment.Center:
                //        EditingCommands.AlignCenter.Execute(null, this);
                //        break;

                //    case TextAlignment.Justify:
                //        EditingCommands.AlignJustify.Execute(null, this);
                //        break;
                //    default:
                //        EditingCommands.AlignRight.Execute(null, this);
                //        break;
                //}
                

            }
        }

        protected FlowDirection ReadingDirectionCore
        {
            get
            {
                object value = Selection.GetPropertyValue(Paragraph.FlowDirectionProperty);//Selection.GetPropertyValue(System.Windows.Documents.Paragraph.FlowDirectionProperty);
                return value == DependencyProperty.UnsetValue ? FlowDirection.RightToLeft : (FlowDirection)value;
            }
            set
            {
                //Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.FlowDirectionProperty, value);
            }
        }

        protected bool IsEmptyCore {
            get {
                foreach(Block b in Document.Blocks) {
                    if(!(b is Paragraph))
                        return false;

                    foreach(object o in ((Paragraph)b).Inlines) {
                        if(!(o is Run))
                            return false;
                        Run r = o as Run;
                        if(!string.IsNullOrEmpty(r.Text))
                            return false;
                    }
                }
               // FlowDirection = FlowDirection.RightToLeft;
                TextAlignmentCore = TextAlignment.Right;

                //IsRightAlignment = true;
                // ReadingDirectionCore = FlowDirection.RightToLeft;
                return true;
            }
        }
        protected bool IsSelectionEmptyCore {
            get {
                return Selection.IsEmpty;
            }
        }
        protected bool? IsBoldCore {
            get {
                object value = Selection.GetPropertyValue(TextElement.FontWeightProperty);
                return (value == DependencyProperty.UnsetValue) ? false : object.Equals(value, FontWeights.Bold);
            }
            set {
                Selection.ApplyPropertyValue(Run.FontWeightProperty, value.Value ? FontWeights.Bold : FontWeights.Normal);
            }
        }
        protected bool? IsItalicCore {
            get {
                object value = Selection.GetPropertyValue(Run.FontStyleProperty);
                return (value == DependencyProperty.UnsetValue) ? false : object.Equals(value, FontStyles.Italic);
            }
            set {
                Selection.ApplyPropertyValue(Run.FontStyleProperty, value.Value ? FontStyles.Italic : FontStyles.Normal);
            }
        }
        protected bool? IsUnderlineCore {
            get {


                var sel = Selection;
                var value = GetPropertyValue(sel, Paragraph.TextDecorationsProperty);
                TextDecorationCollection textDecorationCollection = value as TextDecorationCollection;

                if (textDecorationCollection != null)
                {
                    foreach (TextDecoration textDecoration in textDecorationCollection)
                    {
                        if (textDecoration.Location == TextDecorationLocation.Underline)
                        {
                            return true;
                        }
                        
                    }
                }



                return false; 

                //return (value == DependencyProperty.UnsetValue) ? false : value != null && value.Equals(TextDecorations.Underline); //((TextDecorationCollection)(value)).Count > 0;
            }
            set {
                Selection.ApplyPropertyValue(Run.TextDecorationsProperty, value.Value ? System.Windows.TextDecorations.Underline : null);
            }

        }

        private Object GetPropertyValue(TextRange textRange, DependencyProperty formattingProperty)
        {
            Object value = null;
            var pointer = textRange.Start;
            if (pointer is TextPointer)
            {
                Boolean needsContinue = true;
                DependencyObject element = ((TextPointer)pointer).Parent as TextElement;
                while (needsContinue && (element is Inline || element is Paragraph || element is TextBlock))
                {
                    value = element.GetValue(formattingProperty);
                    IEnumerable seq = value as IEnumerable;
                    needsContinue = (seq == null) ? value == null : seq.Cast<Object>().Count() == 0;
                    element = element is TextElement ? ((TextElement)element).Parent : null;
                }
            }
            return value;
        }


        protected TextMarkerStyle NumberMarkerStyleCore
        {
            get
            {
                //Paragraph startParagraph = _richTextBox.Selection.Start.Paragraph;
                //Paragraph endParagraph = _richTextBox.Selection.End.Paragraph;
                //if (startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
                //{
                //    TextMarkerStyle markerStyle = ((ListItem)startParagraph.Parent).List.MarkerStyle;
                //    if (markerStyle == TextMarkerStyle.Disc) //bullets
                //    {
                //        _btnBullets.IsChecked = true;
                //    }
                //    else if (markerStyle == TextMarkerStyle.Decimal) //numbers
                //    {
                //        _btnNumbers.IsChecked = true;
                //    }
                //}
                //else
                //{
                //    _btnBullets.IsChecked = false;
                //    _btnNumbers.IsChecked = false;
                //}

                Paragraph startParagraph = Selection.Start.Paragraph;
                Paragraph endParagraph = Selection.End.Paragraph;
                if (startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
                {
                    return ((ListItem)startParagraph.Parent).List.MarkerStyle;
                }
                return TextMarkerStyle.None;
            }
            set
            {
                Paragraph p = Selection.Start.Paragraph;
                
                //p.FlowDirection = FlowDirection.RightToLeft;
                if (p == null)
                    return;
                //if (value == TextMarkerStyle.None)
                //{
                //    if (p.Parent is ListItem)
                //    {
                //        EditingCommands.ToggleNumbering.Execute(null, this);
                //        p = Selection.Start.Paragraph;
                //        if (p.Parent is ListItem)
                //        {
                //            EditingCommands.ToggleNumbering.Execute(null, this);
                //        }
                //    }
                //    return;
                //}
                //if (!(p.Parent is ListItem))
                //{
                //    EditingCommands.ToggleNumbering.Execute(null, this);
                //    p = this.Selection.Start.Paragraph;
                //}

                //object DIRECTION = Selection.GetPropertyValue(Paragraph.FlowDirectionProperty);//Selection.GetPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty);
                //DIRECTION = DIRECTION == DependencyProperty.UnsetValue ? FlowDirection.RightToLeft : (FlowDirection)DIRECTION;

                //if((FlowDirection)DIRECTION == FlowDirection.RightToLeft)
                //    Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.FlowDirectionProperty, FlowDirection.LeftToRight);
                //else
                //    Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.FlowDirectionProperty, FlowDirection.RightToLeft);


                if (value == TextMarkerStyle.Disc)
                    EditingCommands.ToggleBullets.Execute(null, this);

                if (value == TextMarkerStyle.Decimal)
                    EditingCommands.ToggleNumbering.Execute(null, this);

                if (p == null || !(p.Parent is ListItem))
                    return;
                ((ListItem)p.Parent).List.MarkerStyle = value;
            }
        }

        //protected TextMarkerStyle ListMarkerStyleCore {
        //    get {
        //        Paragraph startParagraph = Selection.Start.Paragraph;
        //        Paragraph endParagraph = Selection.End.Paragraph;
        //        if(startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List)) {
        //            return ((ListItem)startParagraph.Parent).List.MarkerStyle;
        //        }
        //        return TextMarkerStyle.None;
        //    }
        //    set {
        //        Paragraph p = Selection.Start.Paragraph;
        //        if(p == null)
        //            return;
        //        //if (value == TextMarkerStyle.None)
        //        //{
        //        //    if (p.Parent is ListItem)
        //        //    {
        //        //        EditingCommands.ToggleBullets.Execute(null, this);
        //        //        p = Selection.Start.Paragraph;
        //        //        if (p.Parent is ListItem)
        //        //        {
        //        //            EditingCommands.ToggleBullets.Execute(null, this);
        //        //        }
        //        //    }
        //        //    return;
        //        //}
        //        //if (!(p.Parent is ListItem))
        //        //{
        //        //    EditingCommands.ToggleBullets.Execute(null, this);
        //        //    p = this.Selection.Start.Paragraph;
        //        //}
        //        EditingCommands.ToggleBullets.Execute(null, this);
        //        if (p == null || !(p.Parent is ListItem))
        //            return;
        //        ((ListItem)p.Parent).List.MarkerStyle = value;
        //    }
        //}
        #endregion

        public InlineUIContainer GetUIElementUnderSelection(BlockCollection blocks) {
            foreach(Block block in blocks) {
                Paragraph ph = block as Paragraph;
                if(ph != null) {
                    foreach(object obj in ph.Inlines) {
                        if(obj is Run)
                            continue;
                        InlineUIContainer cont = obj as InlineUIContainer;
                        if(cont != null && cont.ContentStart.CompareTo(Selection.Start) > 0 && cont.ContentStart.CompareTo(Selection.End) < 0) {
                            return cont;
                        }
                    }
                }
 else {
                    List lst = block as List;
                    if(lst != null) {
                        foreach(ListItem lstItem in lst.ListItems) {
                            InlineUIContainer retVal = GetUIElementUnderSelection(lstItem.Blocks);
                            if(retVal != null)
                                return retVal;
                        }
                    }
                }
            }
            return null;

        }
        public InlineUIContainer GetUIElementUnderSelection() {
            BlockCollection col = Document.Blocks;
            if(Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward) == null ||
                Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward).CompareTo(Selection.End) != 0)
                return null;
            return GetUIElementUnderSelection(col);
        }
        public void InsertContainer(InlineUIContainer container) {
            Selection.Text = "";
            if(Selection.End.Paragraph == null) {
                Selection.End.InsertTextInRun("");
            }
            if(Selection.End.Paragraph != null) {
                if(Selection.End.Parent is Run) {
                    string text = Selection.End.GetTextInRun(LogicalDirection.Forward);
                    Run newRun = new Run(text);
                    Selection.End.DeleteTextInRun(text.Length);
                    Selection.End.Paragraph.Inlines.InsertAfter((Run)Selection.End.Parent, newRun);
                    Selection.End.Paragraph.Inlines.InsertBefore(newRun, container);
                } else if(Selection.End.Parent is Paragraph) {
                    Selection.End.Paragraph.Inlines.Add(container);
                }
                Selection.Select(container.ElementStart, container.ElementEnd);
            }
        }
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
            IPopupControl popupMenu = BarManager.GetDXContextMenu(this);
            if(popupMenu != null && popupMenu.IsPopupOpen) {                
                e.Handled = true;
                return;
            }
            base.OnLostKeyboardFocus(e);
        }        
    }
}
