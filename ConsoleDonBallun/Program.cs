using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Web;
using System.Net;

namespace ConsoleDonBallun
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlFile;

            Console.WriteLine("Введите артикулы через запятую");
            string articul = Console.ReadLine();

            Console.WriteLine(articul);

            String value = articul;
            Char delimiter = ',';
            String[] substrings = value.Split(delimiter);
            foreach (var substring in substrings)
            {
                Console.WriteLine(substring);
                // string str = new string(substring);
                String parsArt = substring;





                XPathDocument xml = new XPathDocument("https://www.donballon.ru/bitrix/catalog_export/yandex_donballon_b2b.php");
                XPathNavigator nav = xml.CreateNavigator();
                foreach (XPathNavigator n in nav.Select("/yml_catalog/shop/offers/offer"))
                {

                    if (n.SelectSingleNode("vendorCode").Value == parsArt)
                    {
                        //Console.WriteLine("совпадение");
                        urlFile = "дб" + n.SelectSingleNode("vendorCode").Value + ".jpg";
                        if (GetUrlFile(n.SelectSingleNode("picture").Value, urlFile))
                            //если файл скачан выведем его на консоль
                            Console.WriteLine("FIle " + urlFile + " complite ");
                        else
                            //если файл не скачен сообщаем об ошибке
                            Console.WriteLine("FIle " + urlFile + " no serch ");
                    }
                }
            }
            Console.WriteLine("Завершено. Нажмите любую клавишу для закрытия");
            Console.Read();
        }

        private static int compare(char v1, string v2)
        {
            throw new NotImplementedException();
        }

        static bool GetUrlFile(
        string address, //ссылка на файл 
        string FileNme) //имя файла как будем сохранять
        {
            //создаем обьект для работы с сайтами
            WebClient client = new WebClient();
            //сообщаем что авторизация не нужна 
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            try
            {
                //пробуем скачать файл 
                client.DownloadFile(address, FileNme);
                //если получилось возвращаем правду
                return true;
            }
            catch
            {
                //если не получилось то возвращаем ложь 
                return false;
            }
        }
    }
}

