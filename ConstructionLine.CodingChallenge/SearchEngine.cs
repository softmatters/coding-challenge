using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options?.Colors == null || options?.Sizes == null)
            {
                throw new ArgumentException("no search options provided");
            }

            // get all the shirts where color or size matches
            // with the search options
            var shirts = from shirt in _shirts
                         where (options.Colors.Count == 0 || options.Colors.Contains(shirt.Color)) &&
                               (options.Sizes.Count == 0 || options.Sizes.Contains(shirt.Size))
                         select shirt;

            // get the ColourCounts for all colors
            // including the ones that were not found
            // in the above search
            var colorCounts = from color in Color.All
                              select new ColorCount
                              {
                                  Color = color,
                                  Count = shirts.Count(shirt => shirt.Color == color)
                              };

            // get the SizeCounts for all sizes
            // including the ones that were not found
            // in the above search
            var sizeCounts = from size in Size.All
                             select new SizeCount
                             {
                                 Size = size,
                                 Count = shirts.Count(shirt => shirt.Size == size)
                             };

            return new SearchResults
            {
                Shirts = shirts.ToList(),
                ColorCounts = colorCounts.ToList(),
                SizeCounts = sizeCounts.ToList()
            };
        }
    }
}