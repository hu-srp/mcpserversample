# McpServerSample

一个基于 .NET 10.0 的 MCP (Model Context Protocol) Server 示例项目，提供天气查询、随机数生成等工具功能。

## 项目简介

本项目展示了如何使用 ASP.NET Core 构建一个符合 MCP 协议的服务器，支持通过 Streamable  HTTP 传输协议提供工具调用能力。项目包含完整的 API Key 鉴权机制、日志记录和错误处理。

## 功能特性

- ✅ MCP 协议支持 - 完整实现 Model Context Protocol 标准
- 🔐 API Key 鉴权 - 基于 Bearer Token 的安全认证机制
- 🚀 SSE 传输 - 支持 Streamable HTTP 和 SSE 传输协议
- 🌤️ 天气查询 - 实时天气信息和天气预报
- 🔢 随机数生成 - 指定范围内的随机数生成

## 项目结构

```
McpServerSample/
├── Config/                 # 配置管理
│   └── ApiKeyManager.cs    # API Key 管理器
├── Dto/                    # 数据传输对象
│   └── WeatherResponse.cs   # 天气响应模型
├── Middleware/             # 中间件
│   └── ApiKeyMiddleware.cs # API Key 鉴权中间件
├── Tools/                  # MCP 工具
│   ├── RandomNumberTool.cs # 随机数生成工具
│   └── WeatherTool.cs      # 天气查询工具
├── Program.cs              # 应用程序入口
├── appsettings.json        # 应用配置
└── McpServerSample.csproj  # 项目文件
```

## 配置说明

### API Key 配置

在 `appsettings.json` 中添加 API Key 配置：

```json
{
  "ApiKey": [
    "your-api-key-1",
    "your-api-key-2"
  ]
}
```

## MCP 工具说明

### 1. 随机数生成器

**工具名称**: `get_random_number`

**描述**: 生成指定范围内的随机数

**参数**:
- `min` (int): 最小值（包含）
- `max` (int): 最大值（不包含）

### 2. 天气查询

**工具名称**: `get_weather`

**描述**: 查询指定城市的实时天气信息

**参数**:
- `city` (string): 城市名称（如：Beijing, Shanghai, Zhengzhou）

**返回信息**:
- 天气状况
- 温度（摄氏度/华氏度）
- 体感温度
- 湿度
- 风速和风向
- 能见度
- 紫外线指数
- 当地时间

### 3. 天气预报

**工具名称**: `get_weather_forecast`

**描述**: 获取指定城市的天气预报

**参数**:
- `city` (string): 城市名称
- `days` (int): 预报天数（1-3，默认为 3）

**返回信息**:
- 每日最高/最低/平均温度
- 天气状况
- 紫外线指数
- 日出日落时间

## 鉴权机制

所有 MCP 请求都需要在 HTTP Header 中包含有效的 API Key：

```
Authorization: Bearer your-api-key
```

如果没有提供 API Key 或 API Key 无效，服务器将返回 401 Unauthorized 状态码。

## 测试方法

### 使用 MCP 客户端测试

将此 MCP Server 配置到支持 MCP 的客户端（如 Claude Desktop、Cursor 等），配置文件示例：

```json
{
  "mcpServers": {
    "mcp-server-sample": {
      "url": "http://localhost:3001/mcp",
      "headers": {
        "Authorization": "Bearer API_KEY"
      }
    }
  }
}
```

## 故障排除

### 常见问题

**Q: API Key 鉴权失败**
- A: 检查 `appsettings.json` 中的 API Key 配置是否正确

**Q: 天气查询返回错误**
- A: 检查网络连接和 wttr.in 服务是否可用

**Q: MCP 客户端无法连接**
- A: 确认服务器正在运行，检查防火墙设置

**注意**: 本项目仅用于学习和演示目的，生产环境使用请进行充分的安全测试和性能优化。
