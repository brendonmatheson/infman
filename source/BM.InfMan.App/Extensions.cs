/*
    Infrastructure Manager
	http://bren.cc/infman

	Copyright (c) 2016, Brendon Matheson

    This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public
    License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any
    later version.

    This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
    warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
    details.

    You should have received a copy of the GNU General Public License along with this program; if not, write to the
    Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

namespace cc.bren.infman
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class Extensions
    {
        public static ObservableCollection<T> AddRange<T>(
            this ObservableCollection<T> col,
            IEnumerable<T> items)
        {
            if (col == null) { throw new ArgumentNullException("col"); }
            if (items == null) { throw new ArgumentNullException("items"); }

            foreach(T item in items)
            {
                col.Add(item);
            }

            return col;
        }

        public static ObservableCollection<T> AddRange<S, T>(
            this ObservableCollection<T> col,
            IEnumerable<S> items,
            Func<S, T> map)
        {
            if (col == null) { throw new ArgumentNullException("col"); }
            if (items == null) { throw new ArgumentNullException("items"); }
            if (map == null) { throw new ArgumentNullException("map"); }

            return col.AddRange(items.Select(map));
        }
    }
}
