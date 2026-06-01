---
description: "根據 RentalCar 系統設計原則生成 UML 類別圖（Mermaid）。Use when: 設計類別結構、繪製 class diagram、建立領域模型、系統架構設計"
name: "RentalCar 類別圖生成器"
argument-hint: "描述要設計的功能或場景..."
agent: "agent"
---

# RentalCar 系統類別圖生成器

根據以下**強制設計原則**生成 Mermaid 類別圖：

## 設計原則（必須遵守）

1. **帳號管理**：所有的申請帳號相關功能，必須由 `Account` 類別來負責
2. **租用系統**：租用車輛的領域邏輯，必須由 `RentalCarSystem` 類別來負責
3. **租金計算**：租金相關邏輯須有獨立類別 `RentalFee` 負責
4. **車輛管理**：車輛資訊須由 `Vehicle` 類別來表達