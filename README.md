# [Project Showcase](https://www.youtube.com/watch?v=xXqnGWuNDjA)

## Hikaye:
  - SOLID prensiplerine uygun bir şekilde Rick & Morty çizgi dizisinin açık API aracılığı ile çizgi karakterleri tanıtan bir web sayfası hazırlanmalıdır.
  - .Net Core teknolojileri kullanılarak Web servisi hazırlanıp, DB (MSSql ya da SQLite) ortamına kaydedilmelidir.
  - [API Link](https://rickandmortyapi.com/)

## İstekler:
  - https://rickandmortyapi.com/api/episode API araclığı ile gelen tüm bölümlere ait veriler
ana sayfada pagination(sayfalama) şeklinde listelenmelidir.
  - Her bölüme ait tüm karakter ve özelliklerini gösteren kartlar aynı şekilde
pagination(sayfalama) yapılarak listelenmelidir.
  - Her bir listeleme için (Bölümler, karakterler vb. ) herhangi bir karakter ismi veya
özelliklerine göre search(arama) işlemini de yapabiliyor olmalıyım.
  - Herhangi bir bölümün üzerine tıklandığında o bölüme ait API ye istek atılmalı ve bölüme
ait gelen bilgiler web sayfası üzerinde gösterilmelidir. Örnek API için
https://rickandmortyapi.com/api/episode/8 bağlantısını kullanabilirsiniz.
  - Açılan bölüm sayfasında bölümde bulunan karakterler listelenmeli ve karaktere
tıklandığında karakter ile ilgili bilgiler API aracılığı ile alarak ekrana dökülmelidir. Örnek
API için https://rickandmortyapi.com/api/character/1 bağlantısını kullanabilirsiniz.
  - Sayfa tasarımları tamamen uygulama geliştiriciye bırakılmıştır.
  - Listeleme sırasında Favori Karakter seçimi yapılabilmelidir.
  - Maksimum 10 karakter favori olarak eklenebilir. Favori karakter sayısı 10’u geçtiğinde
kullanıcıya “Favori karakter ekleme sayısını aştınız. Başka bir karakteri favorilerden
çıkarmalısınız.” mesajı gösterilmelidir.
  - Favori karakterleri localStorage kullanarak state yönetimi işlemleri de
yapılmalıdır.
  - Favori karakterlerin listelendiği Favori Karakterler sayfası olmalıdır. Bu sayfada Sil
butonu yer almalıdır. Silme işlemi yapılmak istendiğinde kullanıcıya “... isimli karakteri
favorilerden kaldırmak istediğinize emin misiniz?” sorusu sorulmalıdır. Evet seçeneği
seçildiğinde karakter listeden silinerek güncel liste ekranda gösterilmelidir. Hayır
seçeneği seçildiğinde herhangi bir işlem yapılmasına gerek yoktur.
  - MSSql db kullanılarak https://rickandmortyapi.com/api altındaki Episode ve
Karakter yapısının benzeri oluşturulmalıdır.
