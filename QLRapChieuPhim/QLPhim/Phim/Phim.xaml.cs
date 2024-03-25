using QLRapChieuPhim.Classes;
using QLRapChieuPhim.QLPhim.Phim;
using QLRapChieuPhim.QLPhim.Phim.Edit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Windows.Threading;


namespace QLRapChieuPhim.QLPhim
{
    /// <summary>
    /// Interaction logic for Phim.xaml
    /// </summary>
    public partial class Phim : Window
    {
        Classes.DataProcessor dataProcessor = new DataProcessor(Login.cinemaID);

        public Phim()
        {
           
            dataProcessor = new DataProcessor(Login.cinemaID);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = dataProcessor.ReadData("SELECT * FROM tblPhim");
                dgPhim.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                // Lấy mã phim từ hàng được chọn
                string maPhim = selectedRow["maPhim"].ToString();
                // Hiện thị giao diện sửa thông tin của phim có mã là maPhim
                MessageBox.Show("Bạn đã chọn sửa phim có mã: " + maPhim);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa.");
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = dgPhim.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                // Lấy mã phim từ hàng được chọn
                string maPhim = selectedRow["maPhim"].ToString();
                // Hiện thị xác nhận và thực hiện xóa phim có mã là maPhim
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phim có mã: " + maPhim + " không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Thực hiện xóa phim ở đây
                    MessageBox.Show("Đã xóa phim có mã: " + maPhim);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
     


        }

        public override bool Equals(object obj)
        {
            return obj is Phim phim &&
                   base.Equals(obj) &&
                   EqualityComparer<Dispatcher>.Default.Equals(Dispatcher, phim.Dispatcher) &&
                   EqualityComparer<DependencyObjectType>.Default.Equals(DependencyObjectType, phim.DependencyObjectType) &&
                   IsSealed == phim.IsSealed &&
                   VisualChildrenCount == phim.VisualChildrenCount &&
                   EqualityComparer<DependencyObject>.Default.Equals(VisualParent, phim.VisualParent) &&
                   EqualityComparer<Transform>.Default.Equals(VisualTransform, phim.VisualTransform) &&
                   EqualityComparer<Effect>.Default.Equals(VisualEffect, phim.VisualEffect) &&
                   EqualityComparer<BitmapEffect>.Default.Equals(VisualBitmapEffect, phim.VisualBitmapEffect) &&
                   EqualityComparer<BitmapEffectInput>.Default.Equals(VisualBitmapEffectInput, phim.VisualBitmapEffectInput) &&
                   EqualityComparer<CacheMode>.Default.Equals(VisualCacheMode, phim.VisualCacheMode) &&
                   EqualityComparer<Rect?>.Default.Equals(VisualScrollableAreaClip, phim.VisualScrollableAreaClip) &&
                   EqualityComparer<Geometry>.Default.Equals(VisualClip, phim.VisualClip) &&
                   EqualityComparer<Vector>.Default.Equals(VisualOffset, phim.VisualOffset) &&
                   VisualOpacity == phim.VisualOpacity &&
                   VisualEdgeMode == phim.VisualEdgeMode &&
                   VisualBitmapScalingMode == phim.VisualBitmapScalingMode &&
                   VisualClearTypeHint == phim.VisualClearTypeHint &&
                   VisualTextRenderingMode == phim.VisualTextRenderingMode &&
                   VisualTextHintingMode == phim.VisualTextHintingMode &&
                   EqualityComparer<Brush>.Default.Equals(VisualOpacityMask, phim.VisualOpacityMask) &&
                   EqualityComparer<DoubleCollection>.Default.Equals(VisualXSnappingGuidelines, phim.VisualXSnappingGuidelines) &&
                   EqualityComparer<DoubleCollection>.Default.Equals(VisualYSnappingGuidelines, phim.VisualYSnappingGuidelines) &&
                   HasAnimatedProperties == phim.HasAnimatedProperties &&
                   EqualityComparer<InputBindingCollection>.Default.Equals(InputBindings, phim.InputBindings) &&
                   EqualityComparer<CommandBindingCollection>.Default.Equals(CommandBindings, phim.CommandBindings) &&
                   AllowDrop == phim.AllowDrop &&
                   EqualityComparer<StylusPlugInCollection>.Default.Equals(StylusPlugIns, phim.StylusPlugIns) &&
                   EqualityComparer<Size>.Default.Equals(DesiredSize, phim.DesiredSize) &&
                   IsMeasureValid == phim.IsMeasureValid &&
                   IsArrangeValid == phim.IsArrangeValid &&
                   EqualityComparer<Size>.Default.Equals(RenderSize, phim.RenderSize) &&
                   EqualityComparer<Transform>.Default.Equals(RenderTransform, phim.RenderTransform) &&
                   EqualityComparer<Point>.Default.Equals(RenderTransformOrigin, phim.RenderTransformOrigin) &&
                   IsMouseDirectlyOver == phim.IsMouseDirectlyOver &&
                   IsMouseOver == phim.IsMouseOver &&
                   IsStylusOver == phim.IsStylusOver &&
                   IsKeyboardFocusWithin == phim.IsKeyboardFocusWithin &&
                   IsMouseCaptured == phim.IsMouseCaptured &&
                   IsMouseCaptureWithin == phim.IsMouseCaptureWithin &&
                   IsStylusDirectlyOver == phim.IsStylusDirectlyOver &&
                   IsStylusCaptured == phim.IsStylusCaptured &&
                   IsStylusCaptureWithin == phim.IsStylusCaptureWithin &&
                   IsKeyboardFocused == phim.IsKeyboardFocused &&
                   IsInputMethodEnabled == phim.IsInputMethodEnabled &&
                   Opacity == phim.Opacity &&
                   EqualityComparer<Brush>.Default.Equals(OpacityMask, phim.OpacityMask) &&
                   EqualityComparer<BitmapEffect>.Default.Equals(BitmapEffect, phim.BitmapEffect) &&
                   EqualityComparer<Effect>.Default.Equals(Effect, phim.Effect) &&
                   EqualityComparer<BitmapEffectInput>.Default.Equals(BitmapEffectInput, phim.BitmapEffectInput) &&
                   EqualityComparer<CacheMode>.Default.Equals(CacheMode, phim.CacheMode) &&
                   Uid == phim.Uid &&
                   Visibility == phim.Visibility &&
                   ClipToBounds == phim.ClipToBounds &&
                   EqualityComparer<Geometry>.Default.Equals(Clip, phim.Clip) &&
                   SnapsToDevicePixels == phim.SnapsToDevicePixels &&
                   HasEffectiveKeyboardFocus == phim.HasEffectiveKeyboardFocus &&
                   IsFocused == phim.IsFocused &&
                   IsEnabled == phim.IsEnabled &&
                   IsEnabledCore == phim.IsEnabledCore &&
                   IsHitTestVisible == phim.IsHitTestVisible &&
                   IsVisible == phim.IsVisible &&
                   Focusable == phim.Focusable &&
                   PersistId == phim.PersistId &&
                   IsManipulationEnabled == phim.IsManipulationEnabled &&
                   AreAnyTouchesOver == phim.AreAnyTouchesOver &&
                   AreAnyTouchesDirectlyOver == phim.AreAnyTouchesDirectlyOver &&
                   AreAnyTouchesCapturedWithin == phim.AreAnyTouchesCapturedWithin &&
                   AreAnyTouchesCaptured == phim.AreAnyTouchesCaptured &&
                   EqualityComparer<IEnumerable<TouchDevice>>.Default.Equals(TouchesCaptured, phim.TouchesCaptured) &&
                   EqualityComparer<IEnumerable<TouchDevice>>.Default.Equals(TouchesCapturedWithin, phim.TouchesCapturedWithin) &&
                   EqualityComparer<IEnumerable<TouchDevice>>.Default.Equals(TouchesOver, phim.TouchesOver) &&
                   EqualityComparer<IEnumerable<TouchDevice>>.Default.Equals(TouchesDirectlyOver, phim.TouchesDirectlyOver) &&
                   EqualityComparer<Style>.Default.Equals(Style, phim.Style) &&
                   OverridesDefaultStyle == phim.OverridesDefaultStyle &&
                   UseLayoutRounding == phim.UseLayoutRounding &&
                   EqualityComparer<object>.Default.Equals(DefaultStyleKey, phim.DefaultStyleKey) &&
                   EqualityComparer<TriggerCollection>.Default.Equals(Triggers, phim.Triggers) &&
                   EqualityComparer<DependencyObject>.Default.Equals(TemplatedParent, phim.TemplatedParent) &&
                   VisualChildrenCount == phim.VisualChildrenCount &&
                   EqualityComparer<ResourceDictionary>.Default.Equals(Resources, phim.Resources) &&
                   InheritanceBehavior == phim.InheritanceBehavior &&
                   EqualityComparer<object>.Default.Equals(DataContext, phim.DataContext) &&
                   EqualityComparer<BindingGroup>.Default.Equals(BindingGroup, phim.BindingGroup) &&
                   EqualityComparer<XmlLanguage>.Default.Equals(Language, phim.Language) &&
                   Name == phim.Name &&
                   EqualityComparer<object>.Default.Equals(Tag, phim.Tag) &&
                   EqualityComparer<InputScope>.Default.Equals(InputScope, phim.InputScope) &&
                   ActualWidth == phim.ActualWidth &&
                   ActualHeight == phim.ActualHeight &&
                   EqualityComparer<Transform>.Default.Equals(LayoutTransform, phim.LayoutTransform) &&
                   Width == phim.Width &&
                   MinWidth == phim.MinWidth &&
                   MaxWidth == phim.MaxWidth &&
                   Height == phim.Height &&
                   MinHeight == phim.MinHeight &&
                   MaxHeight == phim.MaxHeight &&
                   FlowDirection == phim.FlowDirection &&
                   Margin.Equals(phim.Margin) &&
                   HorizontalAlignment == phim.HorizontalAlignment &&
                   VerticalAlignment == phim.VerticalAlignment &&
                   EqualityComparer<Style>.Default.Equals(FocusVisualStyle, phim.FocusVisualStyle) &&
                   EqualityComparer<Cursor>.Default.Equals(Cursor, phim.Cursor) &&
                   ForceCursor == phim.ForceCursor &&
                   IsInitialized == phim.IsInitialized &&
                   IsLoaded == phim.IsLoaded &&
                   EqualityComparer<object>.Default.Equals(ToolTip, phim.ToolTip) &&
                   EqualityComparer<ContextMenu>.Default.Equals(ContextMenu, phim.ContextMenu) &&
                   EqualityComparer<DependencyObject>.Default.Equals(Parent, phim.Parent) &&
                   EqualityComparer<IEnumerator>.Default.Equals(LogicalChildren, phim.LogicalChildren) &&
                   EqualityComparer<Brush>.Default.Equals(BorderBrush, phim.BorderBrush) &&
                   BorderThickness.Equals(phim.BorderThickness) &&
                   EqualityComparer<Brush>.Default.Equals(Background, phim.Background) &&
                   EqualityComparer<Brush>.Default.Equals(Foreground, phim.Foreground) &&
                   EqualityComparer<FontFamily>.Default.Equals(FontFamily, phim.FontFamily) &&
                   FontSize == phim.FontSize &&
                   EqualityComparer<FontStretch>.Default.Equals(FontStretch, phim.FontStretch) &&
                   EqualityComparer<FontStyle>.Default.Equals(FontStyle, phim.FontStyle) &&
                   EqualityComparer<FontWeight>.Default.Equals(FontWeight, phim.FontWeight) &&
                   HorizontalContentAlignment == phim.HorizontalContentAlignment &&
                   VerticalContentAlignment == phim.VerticalContentAlignment &&
                   TabIndex == phim.TabIndex &&
                   IsTabStop == phim.IsTabStop &&
                   Padding.Equals(phim.Padding) &&
                   EqualityComparer<ControlTemplate>.Default.Equals(Template, phim.Template) &&
                   HandlesScrolling == phim.HandlesScrolling &&
                   EqualityComparer<IEnumerator>.Default.Equals(LogicalChildren, phim.LogicalChildren) &&
                   EqualityComparer<object>.Default.Equals(Content, phim.Content) &&
                   HasContent == phim.HasContent &&
                   EqualityComparer<DataTemplate>.Default.Equals(ContentTemplate, phim.ContentTemplate) &&
                   EqualityComparer<DataTemplateSelector>.Default.Equals(ContentTemplateSelector, phim.ContentTemplateSelector) &&
                   ContentStringFormat == phim.ContentStringFormat &&
                   EqualityComparer<IEnumerator>.Default.Equals(LogicalChildren, phim.LogicalChildren) &&
                   EqualityComparer<TaskbarItemInfo>.Default.Equals(TaskbarItemInfo, phim.TaskbarItemInfo) &&
                   AllowsTransparency == phim.AllowsTransparency &&
                   Title == phim.Title &&
                   EqualityComparer<ImageSource>.Default.Equals(Icon, phim.Icon) &&
                   SizeToContent == phim.SizeToContent &&
                   Top == phim.Top &&
                   Left == phim.Left &&
                   EqualityComparer<Rect>.Default.Equals(RestoreBounds, phim.RestoreBounds) &&
                   WindowStartupLocation == phim.WindowStartupLocation &&
                   ShowInTaskbar == phim.ShowInTaskbar &&
                   IsActive == phim.IsActive &&
                   EqualityComparer<Window>.Default.Equals(Owner, phim.Owner) &&
                   EqualityComparer<WindowCollection>.Default.Equals(OwnedWindows, phim.OwnedWindows) &&
                   DialogResult == phim.DialogResult &&
                   WindowStyle == phim.WindowStyle &&
                   WindowState == phim.WindowState &&
                   ResizeMode == phim.ResizeMode &&
                   Topmost == phim.Topmost &&
                   ShowActivated == phim.ShowActivated &&
                   EqualityComparer<Grid>.Default.Equals(GrdBoder, phim.GrdBoder) &&
                   EqualityComparer<Grid>.Default.Equals(GrdMain, phim.GrdMain) &&
                   EqualityComparer<TextBox>.Default.Equals(txtFind, phim.txtFind) &&
                   EqualityComparer<Button>.Default.Equals(btnThem, phim.btnThem) &&
                   EqualityComparer<DataGrid>.Default.Equals(dgPhim, phim.dgPhim) &&
                   _contentLoaded == phim._contentLoaded &&
                   EqualityComparer<DataProcessor>.Default.Equals(dataProcessor, phim.dataProcessor);
        }
    }
}
