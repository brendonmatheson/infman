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

namespace cc.bren.infman.framework
{
    using System;

    public class PropertiesMode
    {
        public static readonly PropertiesMode Add = new PropertiesMode("A");
        public static readonly PropertiesMode Edit = new PropertiesMode("E");

        private string _key;

        private PropertiesMode(string key)
        {
            if (key == null) { throw new ArgumentNullException("key"); }

            _key = key;
        }

        public void Apply(
            Action add,
            Action edit)
        {
            if (add == null) { throw new ArgumentNullException("add"); }
            if (edit == null) { throw new ArgumentNullException("edit"); }

            if (_key == "A")
            {
                add();
            }
            else if (_key == "E")
            {
                edit();
            }
        }

        public T Apply<T>(
            Func<T> add,
            Func<T> edit)
        {
            if (add == null) { throw new ArgumentNullException("add"); }
            if (edit == null) { throw new ArgumentNullException("edit"); }

            T result = default(T);

            if (_key == "A")
            {
                result = add();
            }
            else if (_key == "E")
            {
                result = edit();
            }

            return result;
        }
    }
}
