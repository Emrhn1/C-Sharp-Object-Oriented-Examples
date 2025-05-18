using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
   public class DocumentManager
    {
        List<Document> _documents = new List<Document>();

        public void AddDocument(Document document)
        {
            _documents.Add(document);
        }
        public Document FindByISBN(string isbn)
        {
            // Belgeler listesinde gezin
            foreach (var document in _documents)
            {
                if (document.Isbn == isbn)
                {
                    return document; // Eşleşen belgeyi döndür
                }
            }

            // Eğer belge bulunamazsa hata fırlat
            throw new Exception($"Belge bulunamadı: ISBN {isbn}");
        }
        public List<Document> FindByTitlePhrase(string phrase)
        {
            // Eşleşen belgeleri tutmak için bir liste oluştur
            List<Document> matchingDocuments = new List<Document>();

            // Belgeler listesinde gezin
            foreach (var document in _documents)
            {
                // Başlıkta ifadeyi kontrol et (büyük/küçük harf duyarlılığı için ToLower kullanıldı)
                if (document.title != null && document.title.ToLower().Contains(phrase.ToLower()))
                {
                    matchingDocuments.Add(document);
                }
            }

            return matchingDocuments; // Eşleşen belgeleri döndür
        }
        public List<Magazine> FindMagazinesByFrequency(FrequencyType frequency)
        {
            // Eşleşen dergileri tutmak için bir liste oluştur
            List<Magazine> matchingMagazines = new List<Magazine>();

            // Belgeler listesinde gezin
            foreach (var document in _documents)
            {
                // Belgenin bir dergi olup olmadığını kontrol et ve sıklığını karşılaştır
                if (document is Magazine magazine && magazine.Frequency == frequency)
                {
                    matchingMagazines.Add(magazine); // Eşleşen dergiyi listeye ekle
                }
            }

            return matchingMagazines; // Eşleşen dergilerin listesini döndür
        }
    }
}
