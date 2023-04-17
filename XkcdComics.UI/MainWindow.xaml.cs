namespace XkcdComics.UI;

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using XkcdComics.Library;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int maxId = 0;
    private int currId = 0;

    public MainWindow()
    {
        InitializeComponent();
        ApiHelper.InitializeClient();
    }

    private async Task Init()
    {
        maxId = (await new ComicProcessor().LoadComic()).Num;
        currId = maxId;
    }

    private async void prevButton_Click(object sender, RoutedEventArgs e)
    {
        if (currId == 1)
        {
            return;
        }

        await LoadComic(--currId);
    }

    private async void nextButton_Click(object sender, RoutedEventArgs e)
    {
        if (currId == maxId)
        {
            return;
        }

        await LoadComic(++currId);
    }

    private async Task LoadComic(int id = 0)
    {
        var comic = await new ComicProcessor().LoadComic(id);
        var uri = new Uri(comic.Img, UriKind.Absolute);
        comicImage.Source = new BitmapImage(uri);
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await Init();
        await LoadComic();
    }
}
