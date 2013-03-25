using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using MtgExplorer.Entities;
using MtgExplorer.Generators;
using MtgExplorer.Mtg;

namespace MtgExplorer.Gatherer
{
    public class CardImageRipper : ScraperBase
    {
        public static void ExtractAllCardImagesFromGatherer()
        {
            foreach (SetNode set in SetGenerator.Sets)
            {
                Task task = ExtractCardImagesFromGathererAsync(set);
                Task.WaitAll(task);
            }
        }

        public static void ExtractCardImagesFromGatherer(SetNode set)
        {
            Task task = ExtractCardImagesFromGathererAsync(set);
            Task.WaitAll(task);
        }

        public static async Task ExtractCardImagesFromGathererAsync(SetNode set)
        {
            Console.WriteLine("Downloading Artwork for Set {0}", set.Name);
            List<CardLinkInformation> cards = LoadCardsForSet(set);
            foreach (CardLinkInformation card in cards)
            {
                Console.WriteLine("\t{0}", card.Name);
                BitmapEncoder encoder = await GetEncoder(card);

                string filePath = Paths.GetCardImagePath(set, card.MultiverseId);
                Paths.EnsureFilePathExists(filePath);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    encoder.Save(filestream);
                }
            }
        }

        private static async Task<BitmapEncoder> GetEncoder(CardLinkInformation card)
        {
            using (var client = new HttpClient())
            {
                byte[] response = await client.GetByteArrayAsync(card.ImageUri);
                BitmapImage image = LoadImage(response);
                var encoder = new PngBitmapEncoder();
                BitmapFrame frame = BitmapFrame.Create(image);
                encoder.Frames.Add(frame);
                return encoder;
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var stream = new MemoryStream(imageData))
            {
                stream.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = stream;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}