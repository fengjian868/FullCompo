# 启动修复与用户数据目录结构设计

日期：2026-06-19

## 背景

当前版本在首次运行时出现以下问题：

1. 欢迎页没有弹出。
2. 托盘图标显示为空白。
3. 点击托盘“编辑模式”后整个进程崩溃退出。
4. 用户希望用户数据目录结构参考 ClassIsland：根目录简洁，数据集中到 `data` 文件夹下。

## 目标

- 修复欢迎页、托盘图标、编辑模式崩溃三个启动/运行问题。
- 将用户数据目录改为 ClassIsland 风格：`%AppData%\FullCompo\data\`。
- 增加全局异常日志，方便后续排查问题。

## 方案选择

选择 **B：目标修复 + 诊断日志**。

## 详细设计

### 1. 用户数据目录结构

最终运行时目录结构：

```
%AppData%\FullCompo\
├── data\
│   ├── Logs\
│   ├── settings.json
│   └── panels.json
└── app-1.0.x-x\          ← Velopack 版本目录（已存在）
```

说明：

- `ConfigService.GetConfigDirectory()` 返回 `%AppData%\FullCompo\data`。
- 不兼容旧配置。如果检测到旧目录下的 `settings.json`/`panels.json`，直接忽略，按首次运行处理。
- 只有 `Logs` 文件夹，其余功能（Backups、Cache、Config、Plugins、Profiles、Temp）在当前版本中不存在，暂时不创建空文件夹。

### 2. 欢迎页修复

修改 `WelcomeWindow.axaml`：

- 增加 `Topmost="True"`。
- 增加 `WindowStartupLocation="CenterScreen"`。

修改 `App.axaml.cs`：

- 在创建和显示 `WelcomeWindow` 时分别加 `try-catch`。
- 失败时将异常写入 `data\Logs\app.log`。
- 失败时尝试弹出一个简单的系统消息框提示用户。
- 即使欢迎页失败，也继续尝试创建桌面组件，避免程序完全不可用。

### 3. 托盘图标修复

重写 `App.LoadTrayIcon()`，按以下顺序尝试：

1. 使用 `logo.ico` 的 stream 直接构造 `WindowIcon`。
2. 失败则尝试 `logo.png` 构造 `Bitmap` 再构造 `WindowIcon`。
3. 再失败则在内存中生成一个带 “F” 字母的彩色位图作为兜底图标，确保托盘区域始终可见。

所有失败都写入 `data\Logs\app.log`。

### 4. 编辑模式防崩溃

修改 `PanelService`：

- `CreateOrUpdateWidgets()`、`EnterEditMode()`、`ExitEditMode()` 加 `try-catch`。
- 异常时写日志，不向上抛出。

修改 `App.ToggleEditMode()`：

- 整个切换逻辑包在 `try-catch` 中。
- 异常时写日志并提示用户，进程不退出。

修改 `DesktopSurfaceWindow`：

- `Opened` 事件和 `LoadWidgets()` 中的异常被捕获，避免未处理异常导致进程退出。

### 5. 全局异常日志

在 `Program.cs` 中注册：

- `AppDomain.CurrentDomain.UnhandledException`
- `TaskScheduler.UnobservedTaskException`

将未处理异常写入 `data\Logs\crash-yyyyMMdd-HHmmss.log`，并包含堆栈信息。

## 文件变更

- `FullCompo.Core/Services/ConfigService.cs`
- `FullCompo.App/App.axaml.cs`
- `FullCompo.App/Program.cs`
- `FullCompo.App/Services/PanelService.cs`
- `FullCompo.App/Views/DesktopSurfaceWindow.axaml.cs`
- `FullCompo.App/Views/WelcomeWindow.axaml`

## 回滚/兼容策略

- 不兼容旧配置目录。用户删除 `%AppData%\FullCompo\` 后重新运行，即视为首次运行。
-  Velopack 版本目录结构不变。
