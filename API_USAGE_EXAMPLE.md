# Hướng dẫn sử dụng API Order với JWT Authentication

## Thay đổi đã thực hiện

### 1. **OrdersController** - Tự động lấy userId từ JWT token
- Khi tạo order (POST) hoặc cập nhật order (PUT), userId được tự động lấy từ JWT token
- Không cần truyền userId trong request body nữa
- Nếu không có JWT token hoặc token không hợp lệ, API sẽ trả về 401 Unauthorized

### 2. **CreateOrderRequestModel** - Loại bỏ UserId
- Đã xóa trường `UserId` khỏi request model
- Chỉ cần truyền `PaymentId` và `OrderDetails`

### 3. **OrderService** - Nhận userId từ controller
- Method `CreateAsync` và `UpdateAsync` nhận thêm tham số `userId`
- Service sử dụng userId từ JWT token thay vì từ request

## Cách sử dụng API

### Bước 1: Đăng nhập để lấy JWT token
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "testuser",
  "password": "password123"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "userId": "user-guid-here",
    "username": "testuser",
    "email": "test@example.com",
    "role": "User",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiresAt": "2024-01-01T12:00:00Z"
  },
  "error": null
}
```

### Bước 2: Tạo Order với JWT token
```http
POST /api/orders
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "paymentId": "payment-guid-here",
  "orderDetails": [
    {
      "productId": "product-guid-1",
      "quantity": 2
    },
    {
      "productId": "product-guid-2", 
      "quantity": 1
    }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "orderId": "order-guid-here",
    "userId": "user-guid-here", // Tự động lấy từ JWT token
    "orderDate": "2024-01-01T10:00:00Z",
    "totalAmount": 150.00,
    "paymentId": "payment-guid-here",
    "orderDetails": [
      {
        "orderDetailId": "detail-guid-1",
        "productId": "product-guid-1",
        "productName": "Cà phê đen",
        "quantity": 2,
        "unitPrice": 50.00,
        "total": 100.00
      },
      {
        "orderDetailId": "detail-guid-2",
        "productId": "product-guid-2",
        "productName": "Bánh ngọt",
        "quantity": 1,
        "unitPrice": 50.00,
        "total": 50.00
      }
    ]
  },
  "error": null
}
```

### Bước 3: Cập nhật Order
```http
PUT /api/orders/order-guid-here
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "paymentId": "new-payment-guid",
  "orderDetails": [
    {
      "productId": "product-guid-1",
      "quantity": 3
    }
  ]
}
```

## Lợi ích của thay đổi

### 1. **Bảo mật cao hơn**
- UserId được lấy từ JWT token, không thể giả mạo
- Ngăn chặn việc tạo order cho user khác

### 2. **Đơn giản hóa API**
- Client không cần truyền userId
- Giảm thiểu lỗi do truyền sai userId

### 3. **Tự động hóa**
- Hệ thống tự động xác định user từ token
- Không cần validation userId trong request

## Xử lý lỗi

### 1. **Không có JWT token**
```json
{
  "success": false,
  "data": null,
  "error": {
    "message": "Unauthorized"
  }
}
```

### 2. **JWT token không hợp lệ**
```json
{
  "success": false,
  "data": null,
  "error": {
    "message": "Unauthorized"
  }
}
```

### 3. **Không thể xác định user từ token**
```json
{
  "success": false,
  "data": null,
  "error": {
    "message": "Không thể xác định người dùng."
  }
}
```

## Test Cases cần cập nhật

### TC029: Create Order - Success
- **Input**: JWT token + order data (không có userId)
- **Expected**: Status 201, order với userId từ token

### TC030: Create Order - No Token
- **Input**: Order data không có Authorization header
- **Expected**: Status 401, "Unauthorized" message

### TC031: Create Order - Invalid Token
- **Input**: Invalid JWT token + order data
- **Expected**: Status 401, "Unauthorized" message

### TC032: Update Order - Success
- **Input**: JWT token + order ID + updated data
- **Expected**: Status 200, updated order với userId từ token

### TC033: Update Order - No Token
- **Input**: Order ID + data không có Authorization header
- **Expected**: Status 401, "Unauthorized" message
