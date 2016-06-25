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
    using System.IO;

    public static class Extensions
    {
        public static DirectoryInfo Dir(
            this DirectoryInfo dir,
            string name)
        {
            if (dir == null) { throw new ArgumentNullException("dir"); }
            if (name == null) { throw new ArgumentNullException("name"); }

            return new DirectoryInfo(Path.Combine(
                dir.FullName,
                name));
        }

        public static FileInfo File(
            this DirectoryInfo dir,
            string name)
        {
            if (dir == null) { throw new ArgumentNullException("dir"); }

            return new FileInfo(Path.Combine(
                dir.FullName,
                name));
        }
    }
}
