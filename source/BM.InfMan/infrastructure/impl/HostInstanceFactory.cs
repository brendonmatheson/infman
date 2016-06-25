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

using System;
namespace cc.bren.infman.infrastructure.impl
{
    using System;

    public class HostInstanceFactory :
        HostInstanceEntity,
        HostInstanceInsert
    {
        public static HostInstanceEntity Entity(
            Guid hostInstanceId,
            string name,
            Guid hostSpecId,
            Guid infrastructureId)
        {
            return new HostInstanceFactory(
                hostInstanceId,
                name,
                hostSpecId,
                infrastructureId);
        }

        /// <summary>
        /// Entity
        /// </summary>
        private HostInstanceFactory(
            Guid hostInstanceId,
            string name,
            Guid hostSpecId,
            Guid infrastructureId)
        {
            if (hostInstanceId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "hostInstanceId"); }
            if (name == null) { throw new ArgumentNullException("name"); }
            if (hostSpecId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "hostSpecId"); }
            if (infrastructureId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "infrastructureId"); }

            this.HostInstanceId = hostInstanceId;
            this.Name = name;
            this.HostSpecId = hostSpecId;
            this.InfrastructureId = infrastructureId;
        }

        public static HostInstanceInsert Insert(
            string name,
            Guid hostSpecId,
            Guid infrastructureId)
        {
            return new HostInstanceFactory(
                name,
                hostSpecId,
                infrastructureId);
        }

        private HostInstanceFactory(
            string name,
            Guid hostSpecId,
            Guid infrastructureId)
        {
            if (name == null) { throw new ArgumentNullException("name"); }
            if (hostSpecId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "hostSpecId"); }
            if (infrastructureId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "infrastructureId"); }

            this.Name = name;
            this.HostSpecId = hostSpecId;
            this.InfrastructureId = infrastructureId;
        }

        public Guid HostInstanceId { get; private set; }

        public string Name { get; private set; }

        public Guid HostSpecId { get; private set; }

        public Guid InfrastructureId { get; private set; }
    }
}
