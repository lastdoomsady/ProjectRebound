# ProjectRebound v2 Implementation Status

更新时间：2026-04-20

## 已完成

- 后端项目：`Backend/ProjectRebound.MatchServer`
  - `.NET 8` Minimal API。
  - EF Core SQLite 持久化。
  - 匿名登录：`POST /v1/auth/guest`。
  - UDP host probe：`POST /v1/host-probes` 和 confirm。
  - 房间创建、列表、详情、加入、离开、心跳、start、end。
  - 快速匹配 ticket：创建、查询、取消。
  - 后台 matchmaking loop。
  - 后台 lifecycle cleanup。
  - 旧 `/server/status` 兼容层。
  - 保留 host migration 和 relay 接口，返回 `501 NOT_IMPLEMENTED`。
  - SQLite `DateTimeOffset` 已转成 Unix milliseconds 存储，避免 SQLite provider 无法翻译 `ExpiresAt > now` 查询。

- 共享契约：`Shared/ProjectRebound.Contracts`
  - DTO 和 enum 已集中定义。

- Python GUI 原型：`Desktop/ProjectRebound.Browser.Python`
  - 标准库 `tkinter + urllib`，无需额外 pip 依赖。
  - 配置保存、匿名登录、刷新房间、创建房间、加入房间、快速匹配。
  - 复用现有 `-match=ip:port` 客户端直连。
  - 创建主机优先启动 `ProjectReboundServerWrapper.exe`。

- C++ 接线
  - `ServerWrapper` 支持 `-online=`, `-roomid=`, `-hosttoken=`, `-map=`, `-mode=`, `-difficulty=`, `-servername=`, `-serverregion=`, `-port=`。
  - `Payload` 支持 `-roomid=` 和 `-hosttoken=`。
  - `Payload` 有房间凭证时向 `/v1/rooms/{roomId}/heartbeat` 上报；否则继续 `/server/status`。

- 文档
  - `docs/backend-api-spec-v2.md`
  - `Desktop/ProjectRebound.Browser.Python/README.md`
  - 本状态文件。

## 当前主线

- 当前桌面浏览器主线是 Python 原型。
- WPF 项目仍在 `Desktop/ProjectRebound.Browser`，但不是当前推荐运行入口。

## 待验证 / 未完成

- 未做完整端到端游戏联机验收：需要真实游戏目录、wrapper、Payload 注入环境和至少两端客户端。
- 未做 NAT 打洞、Relay、主机迁移。
- 未做账号系统。
- 未做游戏内 `joinTicket` 校验。
- 未做数据库迁移；如果 schema 变化，当前建议删除旧 SQLite DB 后重建。
- 未做自动化 API 集成测试项目。

## 验证命令

最近一次检查结果：

- `dotnet build Backend\ProjectRebound.MatchServer\ProjectRebound.MatchServer.csproj --no-restore`：通过，0 warnings / 0 errors。
- `python -m py_compile Desktop\ProjectRebound.Browser.Python\project_rebound_browser.py`：通过。
- `MSBuild Payload\Payload.vcxproj /p:Configuration=Release /p:Platform=x64 /m`：通过，0 warnings / 0 errors。
- `MSBuild ServerWrapper\ProjectReboundServerWrapper.slnx /p:Configuration=Release /p:Platform=x64 /m`：通过，0 warnings / 0 errors。
- 后端 smoke：临时 SQLite + 临时端口启动成功，`/health` 和 `POST /v1/auth/guest` 通过；后台 matchmaking 未再出现 `could not be translated` / `Matchmaking tick failed`。

后端构建：

```powershell
dotnet build Backend\ProjectRebound.MatchServer\ProjectRebound.MatchServer.csproj --no-restore
```

Python GUI 语法检查：

```powershell
python -m py_compile Desktop\ProjectRebound.Browser.Python\project_rebound_browser.py
```

Payload 构建：

```powershell
& 'G:\FXXKING SOFTWARES\VSC\MSBuild\Current\Bin\MSBuild.exe' Payload\Payload.vcxproj /p:Configuration=Release /p:Platform=x64 /m
```

ServerWrapper 构建：

```powershell
& 'G:\FXXKING SOFTWARES\VSC\MSBuild\Current\Bin\MSBuild.exe' ServerWrapper\ProjectReboundServerWrapper\ProjectReboundServerWrapper.slnx /p:Configuration=Release /p:Platform=x64 /m
```
