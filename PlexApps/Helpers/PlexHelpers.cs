using System.Collections.Generic;
using System.Linq;
using Plex.Api.Models;

namespace PlexApps.Helpers
{
    public static class PlexHelpers
    {
        public static Metadata PlexHasItem(List<Metadata> plexMovies, string title, string imdb, int year)
        {
            var plexHasItem = plexMovies.SingleOrDefault(x =>
                x.Title == title && x.ExternalProvider.ToUpper() == "IMDB" &&
                x.ExternalProviderId == imdb);
            if (plexHasItem == null)
                plexHasItem =
                    plexMovies.SingleOrDefault(x => x.Title == title && x.Year == year);
            if (plexHasItem == null)
                plexHasItem = plexMovies.SingleOrDefault(x =>
                    x.ExternalProvider.ToUpper() == "IMDB" && x.ExternalProviderId == imdb);
            return plexHasItem;
        }

        public static Metadata PlexWatchedItem(List<Metadata> plexMovies, string title, string imdb, int year)
        {
            var plexHasItem = plexMovies.SingleOrDefault(x =>
                x.Title == title && x.ExternalProvider.ToUpper() == "IMDB" &&
                x.ExternalProviderId == imdb && x.ViewCount > 0);
            if (plexHasItem == null)
                plexHasItem =
                    plexMovies.SingleOrDefault(x => x.Title == title && x.Year == year && x.ViewCount > 0);
            if (plexHasItem == null)
                plexHasItem = plexMovies.SingleOrDefault(x =>
                    x.ExternalProvider.ToUpper() == "IMDB" && x.ExternalProviderId == imdb && x.ViewCount > 0);
            return plexHasItem;
        }
    }
}