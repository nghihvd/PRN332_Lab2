# Kiểm tra CategoryId trong Product API

## Vấn đề đã được sửa

**Trước đây**: Khi tạo/cập nhật product với CategoryId không tồn tại → Trả về lỗi 500 (Internal Server Error)

**Bây giờ**: Khi tạo/cập nhật product với CategoryId không tồn tại → Trả về lỗi 400 (Bad Request) với message rõ ràng

## Các thay đổi đã thực hiện

### 1. **ProductService.cs**
- Thêm validation CategoryId trong `CreateAsync()` method
- Thêm validation CategoryId trong `UpdateAsync()` method
- Ném `KeyNotFoundException` với message tiếng Việt khi CategoryId không tồn tại

### 2. **ProductsController.cs**
- Thêm try-catch trong `Create()` method để xử lý `KeyNotFoundException`
- Trả về 400 Bad Request thay vì 500 Internal Server Error

## Ví dụ sử dụng

### ✅ **Tạo Product với CategoryId hợp lệ**
```http
POST /api/products
Authorization: Bearer <JWT_TOKEN>
Content-Type: application/json

{
  "name": "Cà phê đen",
  "description": "Cà phê đen thơm ngon",
  "price": 25000,
  "categoryId": "valid-category-guid",
  "isActive": true
}
```

**Response (200):**
```json
{
  "success": true,
  "data": {
    "productId": "product-guid",
    "name": "Cà phê đen",
    "description": "Cà phê đen thơm ngon",
    "price": 25000,
    "categoryId": "valid-category-guid",
    "categoryName": "Cà phê",
    "isActive": true,
    "createdDate": "2024-01-01T10:00:00Z"
  },
  "error": null
}
```

### ❌ **Tạo Product với CategoryId không tồn tại**
```http
POST /api/products
Authorization: Bearer <JWT_TOKEN>
Content-Type: application/json

{
  "name": "Cà phê đen",
  "description": "Cà phê đen thơm ngon",
  "price": 25000,
  "categoryId": "non-existent-category-guid",
  "isActive": true
}
```

**Response (400):**
```json
{
  "success": false,
  "data": null,
  "error": {
    "message": "Danh mục với ID non-existent-category-guid không tồn tại."
  }
}
```

### ✅ **Tạo Product không có CategoryId (null)**
```http
POST /api/products
Authorization: Bearer <JWT_TOKEN>
Content-Type: application/json

{
  "name": "Cà phê đen",
  "description": "Cà phê đen thơm ngon",
  "price": 25000,
  "categoryId": null,
  "isActive": true
}
```

**Response (200):**
```json
{
  "success": true,
  "data": {
    "productId": "product-guid",
    "name": "Cà phê đen",
    "description": "Cà phê đen thơm ngon",
    "price": 25000,
    "categoryId": null,
    "categoryName": null,
    "isActive": true,
    "createdDate": "2024-01-01T10:00:00Z"
  },
  "error": null
}
```

### ❌ **Cập nhật Product với CategoryId không tồn tại**
```http
PUT /api/products/product-guid
Authorization: Bearer <JWT_TOKEN>
Content-Type: application/json

{
  "name": "Cà phê đen cập nhật",
  "description": "Cà phê đen thơm ngon",
  "price": 30000,
  "categoryId": "non-existent-category-guid",
  "isActive": true
}
```

**Response (400):**
```json
{
  "success": false,
  "data": null,
  "error": {
    "message": "Danh mục với ID non-existent-category-guid không tồn tại."
  }
}
```

## Test Cases cần cập nhật

### TC013: Create Product - Invalid CategoryId
- **Input**: Product data với CategoryId không tồn tại
- **Expected**: Status 400, "Danh mục với ID {categoryId} không tồn tại." message
- **Test**: POST /api/products

### TC015: Update Product - Invalid CategoryId  
- **Input**: Product ID + data với CategoryId không tồn tại
- **Expected**: Status 400, "Danh mục với ID {categoryId} không tồn tại." message
- **Test**: PUT /api/products/{id}

## Lợi ích của thay đổi

1. **🔍 Validation rõ ràng**: Kiểm tra CategoryId trước khi tạo/cập nhật
2. **📝 Error message tiếng Việt**: Dễ hiểu cho người dùng
3. **🚫 Ngăn chặn lỗi**: Không tạo product với CategoryId không tồn tại
4. **🔄 Consistency**: Cùng một cách xử lý cho Create và Update
5. **✅ Null safety**: Cho phép CategoryId = null (product không có category)

## Code Changes Summary

```csharp
// ProductService.cs - CreateAsync
if (!string.IsNullOrEmpty(productCreateDto.CategoryId))
{
    var categoryRepository = _unitOfWork.GetRepository<Category>();
    var category = await categoryRepository.GetByIdAsync(productCreateDto.CategoryId);
    if (category == null)
    {
        throw new KeyNotFoundException($"Danh mục với ID {productCreateDto.CategoryId} không tồn tại.");
    }
}

// ProductsController.cs - Create
try
{
    var product = await _productService.CreateAsync(productCreateDto);
    return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
}
catch (KeyNotFoundException ex)
{
    return BadRequest(new { message = ex.Message });
}
```
