Dưới đây là phân tích đề bài và bản README phác thảo giúp bạn triển khai Lab 2 – PRN232: ASP.NET Core REST API:  

***

# PRN232 – Lab 2: Coffee Store Management System

## Overview
Hệ thống Coffee Store được xây dựng theo mô hình **3-layer architecture**:  
- **API (PRN232.Lab2.CoffeeStore.API)**: Cung cấp REST API endpoints.  
- **Services (PRN232.Lab2.CoffeeStore.Services)**: Xử lý logic nghiệp vụ, mapping giữa các model.  
- **Repositories (PRN232.Lab2.CoffeeStore.Repositories)**: Tương tác với database bằng **GenericRepository** và **UnitOfWork**.  

Mục tiêu: Ứng dụng ASP.NET Core với kiến trúc module hóa, dễ bảo trì, dễ mở rộng, có hỗ trợ xác thực bằng JWT, content negotiation, và bộ API nâng cao.

***

## Requirements to Implement  

### Project Structure
- **TÁCH 3 PROJECT RIÊNG BIỆT**: API, Service, Repository.  
- **ÁP DỤNG KIẾN TRÚC 3 LAYER RÕ RÀNG**.  

### Authentication & Authorization
- **DÙNG JWT ACCESS TOKEN + REFRESH TOKEN**.  
- **CÁC ENDPOINT CRUD PHẢI BẢO VỆ = AUTHENTICATION & AUTHORIZATION**.  
- API cần có: **login, register, refresh token, logout**.  

### Content Negotiation  
- CHO PHÉP CLIENT REQUEST JSON HOẶC XML QUA `Accept` HEADER.  

### Repository & Unit of Work  
- GenericRepository cho CRUD cơ bản.  
- UnitOfWork để quản lý transaction và commit thay đổi.  

### Service Layer  
- Mapping model giữa các layer:  
  - **EntityModel**: DB entities (Repository layer).  
  - **BusinessModel**: đối tượng dùng trong Service (logic).  
  - **RequestModel/ResponseModel**: chuyên dùng để nhận input và trả output (API layer).  

### Entities (Database Tables)  
- **Category**: `CategoryId, Name, Description, CreatedDate`  
- **Product**: `ProductId, Name, Description, Price, CategoryId, IsActive`  
- **Order**: `OrderId, UserId, OrderDate, Status, PaymentId`  
- **OrderDetail**: `OrderDetailId, OrderId, ProductId, Quantity, UnitPrice`  
- **Payment**: `PaymentId, OrderId, Amount, PaymentDate, PaymentMethod`  
- **MỖI BẢNG CẦN ÍT NHẤT 20 HÀNG MẪU**.  

### Advanced API Endpoints (GET)  
- **SEARCH**: Lọc theo từ khóa.  
- **SORT**: Sắp xếp theo nhiều trường (asc, desc).  
- **PAGING**: Có `page` và `pageSize`.  
- **FIELD SELECTION**: Query param `select=id,name,...` để chọn trường trả về.  

### Postman Collection  
- PHẢI TEST TOÀN BỘ CHỨC NĂNG VỚI POSTMAN.  
- **Authentication**: login -> lấy token -> request protected endpoints.  
- **Paging/Sorting/Searching/Field Selection**: test đầy đủ use cases.  
- **HttpStatusCode**: verify (200, 201, 400, 401, 403, 404, …).  
- **JSON Schema Validation**: validate response với schema.  
- **Edge Cases**: page vượt quá dữ liệu, empty result, token sai/hết hạn, validation error.  
- **VERIFY DỮ LIỆU THỰC TẾ**: vd. tên sản phẩm, tổng đơn hàng.  

***

## Grading Criteria (10 points)  
- **3-Project Architecture**: 1 điểm.  
- **JWT Authentication (access + refresh token)**: 1 điểm.  
- **Content Negotiation (JSON/XML)**: 1 điểm.  
- **Repository + UnitOfWork Patterns**: 1 điểm.  
- **Advanced GET (search, sort, paging, field selection)**: 1 điểm.  
- **Model Structure (Entity/Business/Request/Response)**: 1 điểm.  
- **DB có ≥20 rows mỗi bảng**: 1 điểm.  
- **Postman Scripts đầy đủ**: 2 điểm.  
- **Code Quality, Error Handling, Commenting**: 1 điểm.  

***

## Important Notes
- PHẢI DUY TRÌ KIẾN TRÚC 3 LAYER.  
- PHẢI SỬ DỤNG JWT ACCESS TOKEN + REFRESH TOKEN.  
- TẤT CẢ CRUD ENDPOINTS PHẢI ĐƯỢC BẢO VỆ.  
- MỖI BẢNG DB CẦN ÍT NHẤT 20 ROWS ĐỂ TEST.  
- PHẢI VIẾT POSTMAN SCRIPT COVER TOÀN BỘ TEST CASE YÊU CẦU.  
- CODE CẦN CÓ COMMENT, CLEAR STRUCTURE, FOLLOW BEST PRACTICES.  

***

Bạn có muốn mình viết chi tiết **mẫu Postman Test Scripts** (ví dụ JSON cho test login, CRUD, paging/sorting) ngay trong README, hay chỉ để hướng dẫn chung?