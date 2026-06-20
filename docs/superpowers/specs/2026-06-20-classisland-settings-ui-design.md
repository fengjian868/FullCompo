# ClassIsland 风格设置页 UI 设计

## 目标
将 `AppSettingsWindow` 1:1 还原为 ClassIsland 新设置窗的样式，同时只保留全面组件已有的功能，充分发挥 Avalonia / FluentAvalonia 框架能力。

## 整体布局

- 窗口类型：`FluentAvalonia.UI.Windowing.AppWindow`，启用 Mica/Acrylic 背景。
- 标题栏：自定义标题栏，左侧显示 App Logo + “应用设置”，右侧显示版本号 + 保存/取消按钮。
- 主体：左侧 `FluentAvalonia.UI.Controls.NavigationView`（`PaneDisplayMode="Left"`），右侧 `Frame` 页面容器。
- 导航项：带 `SymbolIcon` 图标。
  - 常规（Home）
  - 外观（Palette）
  - 天气（Cloud）
  - 关于（Info）

## 页面规划

每个页面为独立 `UserControl`，位于 `FullCompo.App/Views/SettingPages/`。

| 页面 | 对应文件 | 设置项 |
|------|----------|--------|
| 常规 | `GeneralSettingsPage.axaml` | 语言、开机自启、显示托盘图标、穿透点击、编辑模式快捷键 |
| 外观 | `AppearanceSettingsPage.axaml` | 主题、组件间距 |
| 天气 | `WeatherSettingsPage.axaml` | 城市、刷新间隔（1-3 分钟） |
| 关于 | `AboutSettingsPage.axaml` | Logo、版本、简介 |

## 设置项卡片

FluentAvalonia 没有内置 `SettingsCard`，本设计新增一个轻量控件 `FullCompo.App.Controls.SettingsCard`：

- `Header`：标题
- `Description`：描述
- `HeaderIcon`：图标（`IconElement`）
- `ActionContent`：右侧操作控件
- 视觉：圆角卡片、Hover 底色、左右布局

## 数据与 MVVM

- 新增 `AppSettingsWindowViewModel`：
  - 包装 `AppSettings` 的可绑定属性（`Language`、`ThemeId`、`RunOnStartup` 等）。
  - 暴露 `AvailableThemes`。
  - 提供 `SaveCommand`、`CancelCommand`。
- `AppSettingsWindow` 的 `DataContext` 设为 ViewModel。
- 各页面通过 `x:DataType` 编译绑定到 ViewModel。
- 保存流程：
  1. 将 ViewModel 写回 `ConfigService.AppSettings`。
  2. 调用 `_configService.Save()`。
  3. 调用 `_themeService.ApplyTheme(themeId)`。
  4. 调用 `_weatherService.UpdateInterval()` 与 `_weatherService.RefreshAsync()`。
  5. 关闭窗口。

## 动画与过渡

- `Frame.Navigate` 使用默认页面过渡动画（DrillIn / Slide）。
- 页面首次加载可附加淡入/位移动画。
- 按钮、卡片复用 FluentAvalonia 默认 reveal 与 acrylic 效果。

## 构建与交付

- 实现完成后执行 `dotnet build /workspace/FullCompo.sln`。
- 修复所有编译错误后提交并推送到当前分支。
