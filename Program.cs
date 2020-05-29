using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildInvertedIndex_LINQ
{  /* Обратный индекс — это структура данных, часто использующаяся в задачах полнотекстового поиска нужного документа
    в большой базе документов.

По своей сути обратный индекс напоминает индекс в конце бумажных энциклопедий,
где для каждого ключевого слова указан список страниц, где оно встречается.

Вам требуется по списку документов построить обратный индекс.
Обратный индекс в нашем случае — это словарь ILookup<string, int>, ключом в котором является слово, 
а значениями — идентификаторы всех документов, содержащих это слово.*/
    class Program
    {
        public class Document
        {
            public int Id;
            public string Text;
        }
        public static ILookup<string, int> BuildInvertedIndex(Document[] documents)
        {
            return documents
               .SelectMany(w => Regex.Split(w.Text.ToLower(), @"\W+")
               .Distinct()
               .Where(x => x != "")
               .Select(x => Tuple.Create(x, w.Id)))
               .ToLookup(w => w.Item1, w => w.Item2);
        }
        public static void Main()
        {
            Document[] documents =
            {
        new Document {Id = 1, Text = "Hello world!"},
        new Document {Id = 2, Text = "World, world, world... Just words..."},
        new Document {Id = 3, Text = "Words — power"},
        new Document {Id = 4, Text = ""}
    };
            var index = BuildInvertedIndex(documents);
            SearchQuery("world", index);
            SearchQuery("words", index);
            SearchQuery("power", index);
            SearchQuery("cthulhu", index);
            SearchQuery("", index);
            /* Вывод программы
              SearchQuery('world') found documents: 1, 2
              SearchQuery('words') found documents: 2, 3
              SearchQuery('power') found documents: 3
              SearchQuery('cthulhu') found documents: 
              SearchQuery('') found documents:*/
        }
    }
}
