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

namespace cc.bren.infman.infrastructure.impl
{
    using System;

    public class VmwareEsxiFactory :
        InfrastructureFactory,
        VmwareEsxiEntity,
        VmwareEsxiInsert
    {
        public static VmwareEsxiEntity Entity(
            Guid infrastructureId,
            string name,
            string ipAddress)
        {
            return new VmwareEsxiFactory(
                infrastructureId,
                name,
                ipAddress);
        }

        public VmwareEsxiFactory(
            Guid infrastructureId,
            string name,
            string ipAddress) : base(
                infrastructureId,
                InfrastructureType.VmwareEsxi,
                name)
        {
            if (ipAddress == null) { throw new ArgumentNullException("ipAddress"); }

            this.IpAddress = ipAddress;
        }

        public static VmwareEsxiInsert Insert(
            string name,
            string ipAddress)
        {
            return new VmwareEsxiFactory(
                name,
                ipAddress);
        }

        public VmwareEsxiFactory(
            string name,
            string ipAddress) : base(
                InfrastructureType.VmwareEsxi,
                name)
        {
            if (ipAddress == null) { throw new ArgumentNullException("ipAddress"); }

            this.IpAddress = ipAddress;
        }

        public string IpAddress { get; private set; }
    }
}
