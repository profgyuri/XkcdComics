namespace XkcdComics.Library;
using System;
using System.Threading.Tasks;

public class ComicProcessor
{
    public async Task<ComicModel> LoadComic(int id = 0)
    {
        string url = id > 0 ? $"https://xkcd.com/{ id }/info.0.json" : $"https://xkcd.com/info.0.json";

        using var response = await ApiHelper.ApiClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
           throw new Exception(response.ReasonPhrase);
        }

        var comic = response.Content.ReadAsAsync<ComicModel>().Result;
        return comic;
    }
}
