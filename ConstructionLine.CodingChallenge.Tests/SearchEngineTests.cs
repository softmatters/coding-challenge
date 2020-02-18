using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;

        [OneTimeSetUp]
        public void Setup()
        {
            _shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "White - Medium", Size.Medium, Color.White),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
            };

            _searchEngine = new SearchEngine(_shirts);
        }

        [Test]
        public void SearchingShirtsWithNullOptionsThrowsException()
        {
            SearchOptions options = null;

            Assert.Throws<ArgumentException>(() => _searchEngine.Search(options));
        }

        [Test]
        public void SearchingShirtsWithNullColorOptionsThrowsException()
        {
            var options = new SearchOptions
            {
                Colors = null
            };

            Assert.Throws<ArgumentException>(() => _searchEngine.Search(options));
        }

        [Test]
        public void SearchingShirtsWithNullSizeOptionsThrowsException()
        {
            var options = new SearchOptions
            {
                Sizes = null
            };

            Assert.Throws<ArgumentException>(() => _searchEngine.Search(options));
        }

        [Test]
        public void SearchingShirtsWithSingleSize()
        {
            // arrange
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Large }
            };

            // act
            var results = _searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithSingleColor()
        {
            // arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Blue }
            };

            // act
            var results = _searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithSingleColorSingleSize()
        {
            // arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size> { Size.Large }
            };

            // act
            var results = _searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithSingleColorMultipleSizes()
        {
            // arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size> { Size.Large, Size.Medium }
            };

            // act
            var results = _searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithMultipleColorsSingleSize()
        {
            // arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.White },
                Sizes = new List<Size> { Size.Large }
            };

            // act
            var results = _searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithMultipleColorsMultipleSizes()
        {
            // arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black, Color.Blue },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            // act
            var results = _searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithoutSearchOptions()
        {
            // arrange
            var searchOptions = new SearchOptions();

            // act
            var results = _searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithZeroStock()
        {
            // arrange
            var shirts = new List<Shirt>();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow, Color.White },
                Sizes = new List<Size> { Size.Large, Size.Medium }
            };

            // act
            var results = searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchingShirtsWithZeroStockWithoutSearchOptions()
        {
            // arrange
            var shirts = new List<Shirt>();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();

            // act
            var results = searchEngine.Search(searchOptions);

            // assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }
    }
}