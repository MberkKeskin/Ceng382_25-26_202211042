Sayın Hocam Lütfen Database bağlantısı olmazsa şu stringi kullanın:

`appsettings.json` dosyasındaki bağlantı dizesi:

```json
"ConnectionStrings": {
  "SchoolDbConnection": "Server=localhost\\SQLEXPRESS;Database=SchoolDbContext;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
} 

lab da söylemiştim bunu ekleyebilirsiniz demiştiniz.


