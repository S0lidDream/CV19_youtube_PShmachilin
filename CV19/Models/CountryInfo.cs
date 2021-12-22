using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {

        private Point? _Location;

        public override Point Location
        {
            get
            {
                if (_Location != null)
                    return (Point)_Location;

                if (ProvinceCounts is null) return default;

                var averege_x = ProvinceCounts.Average(p => p.Location.X);
                var average_y = ProvinceCounts.Average(p => p.Location.Y);

                return (Point)(_Location = new Point(averege_x, average_y));
            }
            set => _Location = value;
        }

        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }

        private IEnumerable<ConfirmedCount> _Counts;
        public override IEnumerable<ConfirmedCount> Counts
        {
            get
            {
                if (_Counts != null) return _Counts;
                var points_count = ProvinceCounts.FirstOrDefault()?.Counts?.Count() ?? 0;
                if (points_count == 0) return Enumerable.Empty<ConfirmedCount>();

                var province_points = ProvinceCounts.Select(p => p.Counts.ToArray()).ToArray();

                var points = new ConfirmedCount[points_count];
                foreach(var province in province_points)
                {
                    for(int i=0; i< points_count; i++)
                    {
                        if(points[i].Date == default)
                        {
                            points[i] = province[i];
                        }
                        else
                        {
                            points[i].Count += province[i].Count;
                        }
                    }
                }
                return _Counts = points;
            }
            set => _Counts = value;

        }
    }

}
