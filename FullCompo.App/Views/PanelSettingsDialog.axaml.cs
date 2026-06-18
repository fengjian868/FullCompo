using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.App.Views;

public partial class PanelSettingsDialog : Window
{
    private readonly PanelConfig _config;

    public PanelSettingsDialog(PanelConfig config)
    {
        _config = config;
        InitializeComponent();
        BindControls();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void BindControls()
    {
        var nameBox = this.FindControl<TextBox>("NameBox");
        var dockModeBox = this.FindControl<ComboBox>("DockModeBox");
        var columnsBox = this.FindControl<NumericUpDown>("ColumnsBox");
        var cellWidthBox = this.FindControl<NumericUpDown>("CellWidthBox");
        var cellHeightBox = this.FindControl<NumericUpDown>("CellHeightBox");
        var spacingBox = this.FindControl<NumericUpDown>("SpacingBox");
        var marginLeftBox = this.FindControl<NumericUpDown>("MarginLeftBox");
        var marginTopBox = this.FindControl<NumericUpDown>("MarginTopBox");
        var marginRightBox = this.FindControl<NumericUpDown>("MarginRightBox");
        var marginBottomBox = this.FindControl<NumericUpDown>("MarginBottomBox");
        var saveButton = this.FindControl<Button>("SaveButton");
        var cancelButton = this.FindControl<Button>("CancelButton");

        if (nameBox != null) nameBox.Text = _config.Name;
        if (dockModeBox != null)
        {
            dockModeBox.ItemsSource = Enum.GetValues<PanelDockMode>();
            dockModeBox.SelectedItem = _config.DockMode;
        }
        if (columnsBox != null) columnsBox.Value = _config.Columns;
        if (cellWidthBox != null) cellWidthBox.Value = (decimal?)_config.CellWidth;
        if (cellHeightBox != null) cellHeightBox.Value = (decimal?)_config.CellHeight;
        if (spacingBox != null) spacingBox.Value = (decimal?)_config.Spacing;
        if (marginLeftBox != null) marginLeftBox.Value = (decimal?)_config.MarginLeft;
        if (marginTopBox != null) marginTopBox.Value = (decimal?)_config.MarginTop;
        if (marginRightBox != null) marginRightBox.Value = (decimal?)_config.MarginRight;
        if (marginBottomBox != null) marginBottomBox.Value = (decimal?)_config.MarginBottom;

        if (saveButton != null) saveButton.Click += (_, _) => Save();
        if (cancelButton != null) cancelButton.Click += (_, _) => Close();
    }

    private void Save()
    {
        var nameBox = this.FindControl<TextBox>("NameBox");
        var dockModeBox = this.FindControl<ComboBox>("DockModeBox");
        var columnsBox = this.FindControl<NumericUpDown>("ColumnsBox");
        var cellWidthBox = this.FindControl<NumericUpDown>("CellWidthBox");
        var cellHeightBox = this.FindControl<NumericUpDown>("CellHeightBox");
        var spacingBox = this.FindControl<NumericUpDown>("SpacingBox");
        var marginLeftBox = this.FindControl<NumericUpDown>("MarginLeftBox");
        var marginTopBox = this.FindControl<NumericUpDown>("MarginTopBox");
        var marginRightBox = this.FindControl<NumericUpDown>("MarginRightBox");
        var marginBottomBox = this.FindControl<NumericUpDown>("MarginBottomBox");

        if (nameBox != null) _config.Name = nameBox.Text ?? "面板";
        if (dockModeBox?.SelectedItem is PanelDockMode dockMode) _config.DockMode = dockMode;
        if (columnsBox != null) _config.Columns = (int)(columnsBox.Value ?? 4);
        if (cellWidthBox != null) _config.CellWidth = (double)(cellWidthBox.Value ?? 80);
        if (cellHeightBox != null) _config.CellHeight = (double)(cellHeightBox.Value ?? 80);
        if (spacingBox != null) _config.Spacing = (double)(spacingBox.Value ?? 8);
        if (marginLeftBox != null) _config.MarginLeft = (double)(marginLeftBox.Value ?? 0);
        if (marginTopBox != null) _config.MarginTop = (double)(marginTopBox.Value ?? 0);
        if (marginRightBox != null) _config.MarginRight = (double)(marginRightBox.Value ?? 16);
        if (marginBottomBox != null) _config.MarginBottom = (double)(marginBottomBox.Value ?? 0);

        Close();
    }
}
