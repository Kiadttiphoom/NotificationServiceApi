<div align="center">

# 🔔 NotificationServiceApi

### A Simple and Secure API for Sending Telegram Notifications

**Built with .NET 8.0 | ASP.NET Core | HttpClient**

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue.svg)](https://docs.microsoft.com/aspnet/core)

</div>

---

## 📋 Overview

**NotificationServiceApi** เป็น API ขนาดเล็กที่ออกแบบมาเพื่อทำหน้าที่เป็น Gateway ในการส่งการแจ้งเตือนไปยัง Telegram โดยเน้นความปลอดภัยด้วยระบบ API Key และความรวดเร็วเนื่องจากไม่มีการเชื่อมต่อกับฐานข้อมูล (Stateless API)

## ✨ Features

- 🔐 **API Key Authentication**: ระบบความปลอดภัยดักกรอง Request ด้วย `X-API-KEY`
- 📨 **Telegram Integration**: รองรับการส่งข้อความเข้า Telegram Group/Chat ทันทีผ่าน Telegram Bot API
- 🛠️ **Clean Architecture**: โครงสร้างโปรเจกต์แยกส่วนชัดเจน ง่ายต่อการขยายผล
- 📑 **Swagger UI**: มาพร้อมกับระบบ Document และการทดสอบ API ในตัว
- 🛡️ **Consistent Response**: รูปแบบการตอบกลับ JSON ที่เป็นมาตรฐานเดียวกันทั้งหมด

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Telegram Bot Token (ได้จาก [@BotFather](https://t.me/botfather))

### Installation

1. Clone โปรเจกต์ไปยังเครื่องของคุณ:
   ```bash
   git clone https://github.com/your-username/NotificationServiceApi.git
   ```
2. ไปที่โฟลเดอร์โปรเจกต์:
   ```bash
   cd NotificationServiceApi
   ```
3. Restore และ Build โปรเจกต์:
   ```bash
   dotnet restore
   dotnet build
   ```

## ⚙️ Configuration

แอปพลิเคชันใช้ `appsettings.json` เป็นแม่แบบ และใช้ `appsettings.Development.json` สำหรับเก็บค่าจริงในเครื่องพัฒนา

### appsettings.json (Template)
```json
{
  "NotificationSettings": {
    "TelegramToken": ""
  },
  "Authentication": {
    "ApiKey": ""
  }
}
```

> [!IMPORTANT]
> **ห้าม Push ไฟล์ที่มี Token หรือ ApiKey จริงขึ้น Git** ควรใช้ระบบ Environment Variables หรือไฟล์ที่ระบุใน `.gitignore` สำหรับเครื่อง Production

## 🛣️ API Endpoints

### 1. Send Notification
ส่งข้อความแจ้งเตือนไปยัง Telegram

- **URL:** `/api/Notification/send`
- **Method:** `POST`
- **Headers:** 
  - `X-API-KEY`: `Your_Secret_Key`
  - `Content-Type`: `application/json`

**Request Body:**
```json
{
  "chatId": "your-chat-id",
  "message": "Hello from Notification Service!"
}
```

**Success Response (200 OK):**
```json
{
  "success": true,
  "statusCode": 200,
  "message": "Notification sent successfully",
  "data": {
    "sentAt": "2026-05-08T07:37:25.481109Z",
    "chatId": "your-chat-id"
  }
}
```

## 🧪 Testing

คุณสามารถทดสอบ API ผ่าน Swagger UI ได้ที่:
`https://localhost:xxxx/swagger/index.html`

1. กดปุ่ม **Authorize**
2. ใส่ API Key ของคุณ (เช่น `your-api-key`)
3. เลือกเมนู `/api/Notification/send` และกด **Try it out**

---

<div align="center">
Developed with ❤️ for Modern Web Applications
</div>
