# MEBservis - Öğrenci Servisleri Araç Takip Sistemi

**MEBservis**, Milli Eğitim Bakanlığı tarafından taşımalı öğrenci servislerinde zorunlu tutulan araç takip sistemi için geliştirilmiş bir yazılımdır. Bu sistem, öğrenci taşıma hizmetlerini daha güvenli, şeffaf ve verimli hale getirmeyi amaçlayan bir backend çözümüdür. Aşağıda, sistemin temel özellikleri ve kullanılabilir API uç noktaları hakkında bilgi verilmiştir.

## Özellikler

- **Raporlama**: Belirli kullanıcılar ve tarihler için ayrıntılı raporlar oluşturur.
- **Canlı Takip**: Kullanıcıların konumlarını gerçek zamanlı olarak takip eder ve yönetir.
- **Kullanıcı Yönetimi**: Kullanıcıların kaydı, güncellenmesi, silinmesi ve kimlik doğrulama işlemlerini yönetir.

## Kullanılan Teknolojiler

- **.NET Core**: Modern ve ölçeklenebilir web uygulamaları için kullanılan framework.
- **Katmanlı Mimari**: Uygulama, SOLID prensiplerine uygun olarak yapılandırılmıştır.
- **Entity Framework Core**: Veritabanı işlemleri için kullanılan ORM aracı.
- **AutoMapper**: Nesne-DTO dönüşümlerini yönetmek için kullanılan popüler kütüphane.
- **JWT (JSON Web Token)**: Kimlik doğrulama ve yetkilendirme için kullanılmıştır.

## API Kontrolcüler

### 1. ReportController

Bu kontrolcü, kullanıcı ve tarihlere göre rapor verilerini almak için kullanılır.

- **GET `api/reports/by-date`**  
  Belirtilen kullanıcı ve tarihler için raporları döner.

  **Parametreler:**
  - `userId` (int): Kullanıcı ID'si.
  - `date` (DateTime): Rapor tarihi.

  **Yanıt:** Rapor verileri veya 404 Not Found.

- **GET `api/reports/session-id/{sessionId}`**  
  Belirtilen oturum ID'sine göre rapor verilerini döner.

  **Parametreler:**
  - `sessionId` (int): Oturum ID'si.

  **Yanıt:** Rapor verileri veya 404 Not Found.

### 2. TrackingController

Bu kontrolcü, kullanıcıların konum takibini yönetmek için kullanılır.

- **POST `api/trackings/start-tracking`**  
  Kullanıcı için bir takip oturumu başlatır veya günceller.

  **Parametreler:**
  - `trackingDataDto` (TrackingDataDto): Kullanıcı ve konum bilgilerini içeren DTO.

  **Yanıt:** İşlemin başarılı veya başarısız olduğunu belirten HTTP durumu.

- **POST `api/trackings/stop-tracking`**  
  Kullanıcı için aktif olan takip oturumunu durdurur.

  **Parametreler:**
  - `userId` (int): Kullanıcı ID'si.

  **Yanıt:** Takip durdurma işleminin sonucu veya hata mesajı.

### 3. UserController

Bu kontrolcü, kullanıcı yönetimini sağlar.

- **GET `api/users`**  
  Tüm kullanıcıları döner.

  **Yanıt:** Kullanıcı listesi veya 404 Not Found.

- **GET `api/users/{id}`**  
  Belirtilen ID'ye sahip kullanıcıyı döner.

  **Parametreler:**
  - `id` (int): Kullanıcı ID'si.

  **Yanıt:** Kullanıcı bilgileri veya 404 Not Found.

- **POST `api/users`**  
  Yeni bir kullanıcı ekler.

  **Parametreler:**
  - `userCreateDto` (UserCreateDto): Eklenecek kullanıcının bilgileri.

  **Yanıt:** Yeni kullanıcı ID'si veya 400 Bad Request.

- **PUT `api/users`**  
  Var olan bir kullanıcıyı günceller.

  **Parametreler:**
  - `userUpdateDto` (UserUpdateDto): Güncellenecek kullanıcı bilgileri.

  **Yanıt:** Güncelleme işlemi sonucu veya 404 Not Found.

- **POST `api/users/login`**  
  Kullanıcı girişi sağlar ve JWT token'ı döner.

  **Parametreler:**
  - `userLoginDto` (UserLoginDto): Giriş bilgilerini içeren DTO.

  **Yanıt:** JWT token veya hata mesajı.

- **POST `api/users/update-password`**  
  Kullanıcı şifresini günceller.

  **Parametreler:**
  - `updatePasswordDto` (UpdatePasswordDto): Eski ve yeni şifre bilgilerini içeren DTO.

  **Yanıt:** Güncelleme işlemi sonucu veya hata mesajı.


## Ekran Görüntüsü
![MEBservis](https://github.com/user-attachments/assets/236e8738-4eb4-4c87-857c-cf50a8a34db4)
