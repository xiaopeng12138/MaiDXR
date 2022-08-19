# ＃MaiDXR
一个开源的VR街机模拟器框体
交流QQ群：261946477
交流Discord：Coming Soon

## 关于这个项目
- 本项目是https://github.com/xiaopeng12138/MaiMai-VR 的重置更新
- 支持DX版本或以上
- 模型几乎与DX框体比例一致
- 支持原生触摸输入和灯光输出
- 支持90hz或120hz捕获（Bitblt）（感谢@Thalesalex的推荐）
- 可调整的震动反馈
- 支持第三人称和平滑相机
- 3个可自定义的按钮

## 灵感
- https://github.com/derole1/MaiMai-VR
- https://github.com/HelloKS/MaiMai-VR

## 使用的仓库
- https://github.com/HelloKS/MaiMai-VR
- https://github.com/hecomi/uWindowCapture
- https://github.com/Sucareto/Mai2Touch

**特别感谢[@V17AMax](https://github.com/V17AMax)设计的Logo以及风格设计**

## 编译要求
- 当前Unity版本：2021.3.2f1

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
- 必须在com2com的两个端口上启用缓冲区选项（enable buffer），不然，MaiDXR会在显示完Logo后崩溃
- 在 mai2.ini中禁用DummyTouchPanel
- 如果你想要灯效，请将COM21和COM51绑定（必须是这两个端口，并且不要禁用DummyLED！）
- 通过在xxxx.bat中添加[Unity Standalone Player command line arguments](https://docs.unity3d.com/Manual/PlayerCommandLineArguments.html)使本体在窗口模式下运行，并且确保没有任何黑边。1080p显示屏推荐的设置："xxxxx.exe -screen-fullscreen 0 -screen-width 1170 -screen-height 1050"
- 启动MaiDXR后启动游戏本体。
- 如果你的触摸不起作用，请尝试以某种方式启用维护模式，然后退出维护模式。

## 设置
框体底部的绿色按钮是锁定按钮。 长按它将禁用所有不必要的按钮、手柄指针和配置面板。

你可以在设置面板或通过 config.json 调整所有设置。 config.json 的更改只会在 MaiDXR 重启后生效。

如果你想调整设置，请向后退一步。 当手柄离框体太近时，手柄指针会自动消失。

config.json 中的一些配置的参数只是下拉列表选项的顺序。

你可以使用控制器指针指向第三人称相机，并用手柄的抓取键将其放到你想要的位置。

## 预览
![Image Capture](https://github.com/xiaopeng12138/MaiDXR/blob/main/PreviewImage/MaiDXR_PreviewImage.png?raw=true)

## ToDo
- √ 添加用户可调整的设置（控制器位置等）
- √ 添加非VR窗口和摄像机的平滑度
- √ 添加按钮灯光
- √ 添加按钮振动
- √ 添加第三人称摄像机
- √ 添加2p
- √ 添加自定义按钮
- √ 支持只捕捉1p
- √ 添加游戏内设置面板
- 添加多人联机系统
- 添加可视化触摸反馈

非常感谢HelloKS，derole1和所有在BSAH的小伙伴们 ！

如果你想添加任何功能，欢迎提交PR！我将尽快查看并光速打包新版本。
