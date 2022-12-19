# CFG TÜRTEME AĞAÇLARI

Bilgisayar bilimlerinde, dil tasarımı sırasında kullanılan bir gramer tipidir. Basitçe bir dilin kurallarını (dilbilgisini, grammer) tanımlamak için kullanılır.
 
Verilen CFG’ye göre Toplam dil ağacını üreterek; dili oluşturan kelimelerin tamamını (tekrarlı kelimeleri 1 defa yazacak) listeleyen programı yazınız. (“|” karakteri ayraç olarak kullanılacaktır. “- - >” karakteri sağa ok işareti olarak kullanılacaktır. “,” karakteri satır ayracı olarak kullanılacaktır)

Örneğin Σ={a, b, X}

CFG; S -> aa|bX|aXX,	X- > ab|b

İçin Üretilen Kelimeler şunlardır:
aa, bab, bb, aabab, aabb, abab, abb 

Tekrarlanan  kelimeler şunlardır: 
aabab, abab, aabb, abb 
