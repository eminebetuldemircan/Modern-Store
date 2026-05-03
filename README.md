# Modern Store - Çekirdek Yapı (Core Layer)

Bu katman projemin tam kalbi diyebilirim. Burada sadece iş mantığına (Business Logic) yer verdim ve hiçbir dış bağımlılık kullanmadım.

### Neler Var?
*   **Domain:** Uygulamanın temel taşları olan modelleri (Entities) burada tanımladım. Product, Category, Order gibi nesnelerim burada yaşıyor.
*   **Application:** Bu modellerin nasıl davranacağını belirleyen kurallar burada. DTO'lar (Data Transfer Objects), Arayüzler (Interfaces) ve CQRS yapısı ile karmaşıklığı yönettiğim Usecase'ler bu katmanda yer alıyor.

Kısacası burası, projemin en saf ve en önemli katmanı. Dış dünyadan (veritabanı, arayüz vb.) tamamen bağımsız bir yapıda.
