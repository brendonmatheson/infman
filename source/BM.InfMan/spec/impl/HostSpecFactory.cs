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

namespace cc.bren.infman.spec.impl
{
    using System;

    public class HostSpecFactory : HostSpecEntity, HostSpecInsert
    {
        public static HostSpecEntity Entity(
            Guid hostSpecId,
            string name,
            long ramBytes)
        {
            return new HostSpecFactory(hostSpecId, name, ramBytes);
        }

        private HostSpecFactory(
            Guid hostSpecId,
            string name,
            long ramBytes)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            this.HostSpecId = hostSpecId;
            this.Name = name;
            this.RamBytes = ramBytes;
        }

        public static HostSpecInsert Insert(
            string name,
            long ramBytes)
        {
            return new HostSpecFactory(name, ramBytes);
        }

        public HostSpecFactory(
            string name,
            long ramBytes)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            this.Name = name;
            this.RamBytes = ramBytes;
        }

        public Guid HostSpecId { get; private set; }

        public string Name { get; private set; }

        public long RamBytes { get; private set; }
    }
}
