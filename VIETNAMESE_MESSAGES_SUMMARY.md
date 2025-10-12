# Tổng hợp thống nhất Messages tiếng Việt

## Các thay đổi đã thực hiện

### 1. **AuthService.cs**
- ✅ `"Username already exists."` → `"Tên người dùng đã tồn tại."`
- ✅ `"Email already exists."` → `"Email đã tồn tại."`
- ✅ `"Invalid username or password."` → `"Tên người dùng hoặc mật khẩu không đúng."`

### 2. **ProductService.cs**
- ✅ `"Product with ID {id} not found."` → `"Không tìm thấy sản phẩm với ID {id}."`
- ✅ `"Sản phẩm đã được sử dụng và không được xoá."` (đã có sẵn)

### 3. **OrdersController.cs**
- ✅ Thống nhất `Message` → `message` (chữ thường)
- ✅ Tất cả messages đã là tiếng Việt

### 4. **PaymentsController.cs**
- ✅ Thống nhất `Message` → `message` (chữ thường)
- ✅ Tất cả messages đã là tiếng Việt

### 5. **Program.cs**
- ✅ `"Unauthorized"` → `"Không có quyền truy cập"`
- ✅ `"Forbidden"` → `"Bị cấm truy cập"`

### 6. **ResponseWrappingMiddleware.cs**
- ✅ `"Unauthorized"` → `"Không có quyền truy cập"`
- ✅ `"Forbidden"` → `"Bị cấm truy cập"`
- ✅ `"Internal server error"` → `"Lỗi máy chủ nội bộ"`

## Danh sách Messages tiếng Việt đã thống nhất

### **Authentication Messages**
```json
{
  "success": false,
  "error": {
    "message": "Tên người dùng đã tồn tại."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Email đã tồn tại."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Tên người dùng hoặc mật khẩu không đúng."
  }
}
```

### **Product Messages**
```json
{
  "success": false,
  "error": {
    "message": "Không tìm thấy sản phẩm với ID {id}."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Sản phẩm đã được sử dụng và không được xoá."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Danh mục với ID {categoryId} không tồn tại."
  }
}
```

### **Order Messages**
```json
{
  "success": false,
  "error": {
    "message": "Không tìm thấy đơn hàng."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Không thể xác định người dùng."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Đã xảy ra lỗi khi tạo đơn hàng."
  }
}
```

### **Payment Messages**
```json
{
  "success": false,
  "error": {
    "message": "Không tìm thấy thanh toán."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Đã xảy ra lỗi khi tạo thanh toán."
  }
}
```

### **Category Messages**
```json
{
  "success": false,
  "error": {
    "message": "Không tìm thấy danh mục."
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Danh mục đã được sử dụng và không được xoá."
  }
}
```

### **System Messages**
```json
{
  "success": false,
  "error": {
    "message": "Không có quyền truy cập"
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Bị cấm truy cập"
  }
}
```

```json
{
  "success": false,
  "error": {
    "message": "Lỗi máy chủ nội bộ"
  }
}
```

## Quy tắc thống nhất

### 1. **Format chuẩn**
```json
{
  "success": false,
  "data": null,
  "error": {
    "message": "Thông báo lỗi tiếng Việt"
  }
}
```

### 2. **Property naming**
- ✅ Sử dụng `message` (chữ thường) thay vì `Message`
- ✅ Thống nhất trong tất cả controllers và services

### 3. **Ngôn ngữ**
- ✅ Tất cả messages đều là tiếng Việt
- ✅ Sử dụng thuật ngữ nhất quán
- ✅ Thông điệp rõ ràng, dễ hiểu

### 4. **Loại lỗi**
- **404 Not Found**: "Không tìm thấy {entity}"
- **400 Bad Request**: "Thông tin không hợp lệ" hoặc message cụ thể
- **401 Unauthorized**: "Không có quyền truy cập"
- **403 Forbidden**: "Bị cấm truy cập"
- **500 Internal Server Error**: "Lỗi máy chủ nội bộ"

## Test Cases cần cập nhật

Tất cả test cases hiện tại đã phù hợp với messages tiếng Việt mới. Không cần thay đổi gì thêm.

## Lợi ích

1. **🌐 Localization**: Hỗ trợ người dùng Việt Nam
2. **📝 Consistency**: Thống nhất format và ngôn ngữ
3. **🔍 Clarity**: Messages rõ ràng, dễ hiểu
4. **🛠️ Maintainability**: Dễ bảo trì và cập nhật
5. **👥 User Experience**: Trải nghiệm người dùng tốt hơn

## Kết luận

Tất cả error messages trong hệ thống Coffee Store API đã được thống nhất sang tiếng Việt với format chuẩn và nhất quán. Hệ thống giờ đây cung cấp trải nghiệm tốt hơn cho người dùng Việt Nam.
