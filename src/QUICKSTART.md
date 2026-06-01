# RentalCar 車輛租用系統 - 快速開始指南

## 📦 安裝與執行

### 方法一：使用 Visual Studio

1. 開啟 `src/RentalCar.sln`
2. 將 `RentalCar.Web` 設定為啟動專案（右鍵點擊專案 → 設定為啟動專案）
3. 按 F5 或點擊「開始偵錯」

### 方法二：使用命令列

```powershell
# 1. 進入專案目錄
cd src

# 2. 還原套件
dotnet restore

# 3. 建置專案
dotnet build

# 4. 執行 Web 應用程式
cd RentalCar.Web
dotnet run

# 5. 開啟瀏覽器訪問
# https://localhost:5001
# 或
# http://localhost:5000
```

## 🎯 測試帳號

由於使用 InMemory 儲存，每次重啟應用程式資料會清空。請先註冊新帳號：

1. 點擊「註冊」
2. 填寫資訊（範例）：
   - 用戶名稱：`testuser`
   - Email：`test@example.com`
   - 密碼：`Password123`
3. 註冊成功後登入

## 📱 功能導覽

### 1. 首頁
- 查看系統介紹
- 瀏覽車型與價格表
- 快速進入註冊/登入

### 2. 會員註冊
- 路徑：`/Account/Register`
- 填寫用戶名稱、Email、密碼
- 系統會驗證重複性

### 3. 會員登入
- 路徑：`/Account/Login`
- 使用註冊的帳號登入
- 登入後可使用租車功能

### 4. 車輛租用
- 路徑：`/Rental/Index`（需登入）
- 選擇車型（5 種選項）
- 設定租用期間
- 系統自動計算租金
- 確認完成預約

### 5. 租用成功
- 路徑：`/Rental/Success/{id}`
- 顯示預約編號與詳情
- 可選擇繼續租車或返回首頁

## 🔍 專案結構說明

```
src/
├── RentalCar.Domain/           ← 領域層（純商業邏輯）
├── RentalCar.Application/      ← 應用層（Use Cases）
├── RentalCar.Infrastructure/   ← 基礎設施層（Repository）
├── RentalCar.Web/              ← Web 層（MVC + Bootstrap）
└── RentalCar.sln               ← Solution 檔案
```

## ⚙️ 設定說明

### appsettings.json
目前使用預設設定，無需額外配置。

### Session 設定
- 閒置時間：30 分鐘
- 儲存方式：記憶體（DistributedMemoryCache）

### 資料儲存
- 目前使用 InMemory Repository
- 應用程式重啟後資料會清空
- 適合開發與展示用途

## 🛠️ 開發工具

### 推薦的 VS Code 擴充套件
- C# Dev Kit
- C# Extensions
- NuGet Gallery
- ASP.NET Core Snippets

### 推薦的 Visual Studio 工作負載
- ASP.NET 與網頁程式開發
- .NET 桌面開發

## 🐛 常見問題

### Q: 編譯錯誤「找不到 .NET 10 SDK」
A: 請安裝 .NET 10 SDK（或修改專案檔的 `TargetFramework` 為 `net9.0` 或 `net8.0`）

### Q: 登入後無法租車
A: 確認 Session 正常運作，檢查瀏覽器是否允許 Cookie

### Q: 租用時顯示「車型不存在」
A: 確認已選擇車型，檢查 VehicleType 值物件定義

### Q: 畫面樣式異常
A: 確認網路連線正常（Bootstrap 5.1 從 CDN 載入）

## 📚 延伸學習

想深入了解專案架構，請參考：

1. [CD_01_車輛租用系統.md](../CD_01_車輛租用系統.md) - Class Diagram
2. [SD_01_車輛租用系統.md](../SD_01_車輛租用系統.md) - Sequence Diagram
3. [UC_01_車輛租用系統.md](../UC_01_車輛租用系統.md) - Use Case Diagram
4. [.github/copilot-instructions.md](../.github/copilot-instructions.md) - 架構規範

## 🚀 下一步

完成基本測試後，可以嘗試：

1. 新增單元測試專案
2. 替換為真實的資料庫（EF Core）
3. 實作 JWT 驗證
4. 加入車輛庫存管理
5. 實作訂單查詢功能

---

如有任何問題，請參考專案 README.md 或架構文件。
