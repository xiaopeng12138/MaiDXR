# ＃MaiDXR
一个开源的VR街机模拟器框体

## 关于这个项目
- 本项目是https://github.com/xiaopeng12138/MaiMai-VR 的重置更新
- 支持DX版本或以上
- 模型几乎与DX框体比例一致
- 支持原生触摸输入和灯光输出
- 支持90hz或120hz捕获（Bitblt）（感谢@Thalesalex的推荐）
- 可调整的震动反馈
- 支持第三人称和平滑相机
- 4个可自定义的按钮(+1个选择按钮)
- 只有1p

## 灵感
- https://github.com/derole1/MaiMai-VR
- https://github.com/HelloKS/MaiMai-VR

## 使用的仓库
- https://github.com/HelloKS/MaiMai-VR
- https://github.com/hecomi/uWindowCapture
- https://github.com/Sucareto/Mai2Touch

## 编译要求
- 当前Unity版本：2020.3.30f1
- [InputSimulator](https://www.nuget.org/packages/InputSimulator) (需要解压缩nuget包然后找到.dll文件并把它放到assets文件夹中)
- [uWindowCapture](https://github.com/hecomi/uWindowCapture) (要在你的unity项目中导入它)

## 支持的平台
- 所有SteamVR设备
- 所有的Oculus设备
- 使用Oculus Link（原生和通过SteamVR）和ALVR（通过SteamVR）在Quest 2上测试过。手的位置是默认为Quest 2控制器调整的（其他控制器可能需要手动调整获得最佳体验）

## 声明
- 本项目为非营利性项目，部分资源来自互联网!
- 虽然存储库是在MIT许可下的，但不要在商业/盈利的情况下使用它!
- 如果可以的话，请支持你本地的机厅!

## 更改日志
请参见 Changes.md

## 如何使用
- 确保游戏本体可以正常游玩。(不要询问任何与游戏本身直接相关的问题！)
- 下载[MaiDXR的最新版本](https://github.com/xiaopeng12138/MaiDXR/releases)
- 下载并安装[com0com](https://storage.googleapis.com/google-code-archive-downloads/v2/code.google.com/powersdr-iq/setup_com0com_W7_x64_signed.exe)
- 配置com0com，绑定COM3和COM5（必须是这两个端口），COM4和COM6是可选的（绑定会让启动变快）
- 必须在com2com的两个端口上启用缓冲区选项（enable buffer），不然，MaiDXR会在显示完Unity标志后崩溃
- 在 xxxx.ini中禁用DummyTouchPanel
- 如果你想要灯效，请将COM21和COM51绑定（必须是这两个端口，并且不要禁用DummyLED！）
- 通过在xxxx.bat中添加[Unity Standalone Player command line arguments](https://docs.unity3d.com/Manual/PlayerCommandLineArguments.html)使本体在窗口模式下运行，并且确保没有任何黑边。1080p显示屏推荐的设置："xxxxx.exe -screen-fullscreen 0 -screen-width 1170 -screen-height 1050"
- 启动MaiDXR。
- 进一遍维护模式（按下测试键），然后退出维护模式。

## 设置
在MaiDXR窗口中按M键来开关移动。

编辑根目录下的Settings.json文件。在MaiDXR窗口中按F5刷新或重新选中MaiDXR的窗口也能刷新。

HandSize（手大小）, HandPosition（手位置偏移）, PlayerHigh（玩家高度）: 单位为厘米

CaptureFrameRate（捕获帧率）, TouchRefreshRate（触摸帧率）：单位为为帧

Capture1PlayerOnly（仅捕获1p）: "true" (捕获9:16长宽比) 或 "false" (默认) (两倍的9:16长宽比，也就是18:16)

CameraSmooth（摄像机平滑）：0.0 到 1.0, 1.0 = 无平滑度

CameraFOV（相机视野）：以度为单位

CameraPosition（相机位置偏移）：以米为单位

启用第三人称模式：将CameraSmooth设置为0，然后把头显移动到你相机想放在的位置。最后刷新MaiDXR窗口以锁定位置。通过启用以下设置可以在第三人称中显示你的头部位置

ShowHeadCube（显示头部方块）:  "true" 或 "false" (默认)

HapticDuration（手柄震动时长）: 以秒为单位

HapticAmplitude（手柄震动幅度）: 0.0 - 1.0, 1.0 = 最大振动

SelectButton，Button（选择按钮和按钮）（1-4：从上到下）：格式请看[VK代码](https://docs.microsoft.com/windows/win32/inputdev/virtual-key-codes)，有些键不需要在开头加上 "VK_"

## 预览
![Image Capture](https://github.com/xiaopeng12138/MaiDXR/blob/main/PreviewImage/MaiDXR_PreviewImage.png?raw=true)

## ToDo
- √ 添加用户可调整的设置（控制器位置等）
- √ 添加非VR窗口和摄像机的平滑度
- √ 添加按钮灯光
- √ 添加按钮振动
- √ 添加第三人称摄像机
- 添加2p
- √ 添加自定义按钮
- √ 支持只捕捉1p

非常感谢HelloKS和derole1 ！

如果你想添加任何功能，欢迎提交PR！我将尽快查看并光速打包新版本。
