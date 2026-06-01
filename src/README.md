# RentalCar 車輛租用系統

這是一個遵循 **Hexagonal Architecture（六角架構）** 與 **Clean Architecture（整潔架構）** 原則的車輛租用系統，使用 .NET 10 與 C# 開發。

## 🏗️ 專案架構

專案採用四層式架構設計，明確分離關注點：

```
src/
├── RentalCar.Domain/           # 網域層（Domain Layer）
│   ├── Entities/               # 實體（Account, Rental, Vehicle）
│   ├── ValueObjects/           # 值物件（VehicleType, RentalPeriod, Money）
│   └── Exceptions/             # 領域例外
│
├── RentalCar.Application/      # 應用層（Application Layer）
│   ├── Abstractions/           # 抽象介面（Ports）
│   │   └── Repositories/       # Repository 介面
│   ├── UseCases/               # 使用案例（Commands & Handlers）
│   │   ├── Accounts/           # 帳號相關
│   │   └── Rentals/            # 租用相關
│   └── DTOs/                   # 資料傳輸物件
│
├── RentalCar.Infrastructure/   # 基礎設施層（Infrastructure Layer）
│   └── Repositories/           # Repository 實作（InMemory）
│
└── RentalCar.Web/              # 介面層（Web Layer）
    ├── Controllers/            # MVC 控制器
    ├── Views/                  # Razor 視圖（Bootstrap 5.1）
    └── Models/                 # ViewModel
```

## 📋 業務功能

### 1. 帳戶管理
- ✅ 會員註冊
- ✅ 會員登入
- ✅ Session 管理

### 2. 車輛租用
- ✅ 選擇車型（轎車、休旅車、貨車、跑車、電動車）
- ✅ 設定租用期間
- ✅ 自動計算租金
- ✅ 確認租用預約

## 🚀 快速開始

### 先決條件
- .NET 10 SDK
- Visual Studio 2024 或 Visual Studio Code

### 執行步驟

1. **還原相依套件**
```powershell
cd src
dotnet restore
```

2. **建置專案**
```powershell
dotnet build
```

3. **執行 Web 應用程式**
```powershell
cd RentalCar.Web
dotnet run
```

4. **開啟瀏覽器**
```
https://localhost:5001
```

## 🎯 架構原則遵循

### Hexagonal Architecture（六角架構）
- **Domain Layer**：純粹的業務邏輯，不依賴任何外部框架
- **Application Layer**：定義 Use Cases 與 Ports（介面）
- **Infrastructure Layer**：實作 Adapters（Repository 等）
- **Web Layer**：Composition Root，負責 DI 組態

### 依賴方向
```
Web → Application → Domain
Infrastructure → Application/Domain
```

✅ Domain 層完全獨立，不依賴其他層  
✅ Application 層只依賴 Domain 層與自己定義的介面  
✅ Infrastructure 層實作 Application 定義的介面  
✅ Web 層負責綁定 Ports 與 Adapters  

## 🎨 技術棧

- **框架**：.NET 10 / ASP.NET Core MVC
- **語言**：C# 13（使用 record, pattern matching, nullable 等現代特性）
- **前端**：Bootstrap 5.1 + Bootstrap Icons
- **資料存取**：InMemory Repository（便於測試與展示）
- **架構模式**：Hexagonal Architecture + CQRS

## 📊 車型與價格

| 車型 | 日租金 | 特色 |
|------|--------|------|
| 轎車 | NT$ 1,000 | 經濟實惠，都市通勤 |
| 休旅車 | NT$ 1,500 | 空間寬敞，家庭出遊 |
| 貨車 | NT$ 2,000 | 載貨量大，搬家運輸 |
| 跑車 | NT$ 3,000 | 高性能，駕馭樂趣 |
| 電動車 | NT$ 2,800 | 環保節能，科技體驗 |

## 🧪 設計特色

### Domain Layer
- ✅ 使用 Value Object 封裝領域概念（VehicleType, RentalPeriod, Money）
- ✅ Entity 使用靜態工廠方法保證不變式
- ✅ 領域邏輯集中在 Domain 層
- ✅ 無任何基礎設施相依

### Application Layer
- ✅ CQRS 模式（Command/Query 分離）
- ✅ 定義清晰的 Ports（介面）
- ✅ 使用 Handler 模式處理 Use Cases
- ✅ 職責單一，易於測試

### Infrastructure Layer
- ✅ InMemory Repository 實作
- ✅ 便於測試與展示
- ✅ 可輕鬆替換為 EF Core / Dapper 等

### Web Layer
- ✅ 響應式設計（Bootstrap 5.1）
- ✅ Session 管理
- ✅ 友善的使用者介面
- ✅ 清晰的錯誤提示

## 📖 使用流程

1. **註冊帳號** → 填寫用戶名稱、Email、密碼
2. **登入系統** → 使用註冊的帳號登入
3. **選擇車型** → 從 5 種車型中選擇
4. **設定期間** → 選擇租用開始與結束日期
5. **確認租用** → 系統自動計算租金並完成預約
6. **查看結果** → 顯示預約編號與詳細資訊

## 🔧 擴展建議

未來可增強的功能：
- [ ] 實作真實的資料庫（EF Core + SQL Server）
- [ ] 加入單元測試與整合測試
- [ ] 實作 JWT 驗證
- [ ] 新增車輛庫存管理
- [ ] 實作訂單歷史查詢
- [ ] 加入支付整合
- [ ] 實作通知系統（Email/SMS）

## 📝 授權

此專案為教學用途範例。

---

**開發理念**：Clean Code + SOLID Principles + Hexagonal Architecture  
**專案目標**：展示如何使用 .NET 10 與 C# 實作符合 Hexagonal Architecture 的車輛租用系統
