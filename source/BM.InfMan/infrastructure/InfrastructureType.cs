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
namespace cc.bren.infman.infrastructure
{
    using System;

    public class InfrastructureType
    {
        public static InfrastructureType VmwareEsxi = new InfrastructureType("VMW", "VMWare ESXi");
/*
        public static InfrastructureType VirtualBox = new InfrastructureType("VBX", "VirtualBox");
        public static InfrastructureType AmazonWebServices = new InfrastructureType("AWS", "Amazon Web Services");
        public static InfrastructureType Azure = new InfrastructureType("AZR", "Azure");
*/

        public InfrastructureType(
            string infrastructureTypeCode,
            string name)
        {
            if (infrastructureTypeCode == null) { throw new ArgumentNullException("infrastructureTypeCode"); }
            if (name == null) { throw new ArgumentNullException("name"); }

            this.InfrastructureTypeCode = infrastructureTypeCode;
            this.Name = name;
        }

        public string InfrastructureTypeCode { get; private set; }

        public string Name { get; private set; }

        public void Apply(
            Action vmwareEsxi)
        {
            if (vmwareEsxi == null) { throw new ArgumentNullException("vmwareEsxi"); }

            if (this.InfrastructureTypeCode == InfrastructureType.VmwareEsxi.InfrastructureTypeCode)
            {
                vmwareEsxi();
            }

            else
            {
                throw new Exception("unhandled case: " + this.InfrastructureTypeCode);
            }
        }

        public T Apply<T>(
            Func<T> vmwareEsxi)
        {
            if (vmwareEsxi == null) { throw new ArgumentNullException("vmwareEsxi"); }

            T result = default(T);

            if (this.InfrastructureTypeCode == InfrastructureType.VmwareEsxi.InfrastructureTypeCode)
            {
                result = vmwareEsxi();
            }

            else
            {
                throw new Exception("unhandled case: " + this.InfrastructureTypeCode);
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            InfrastructureType objT = (InfrastructureType)obj;

            return this.InfrastructureTypeCode == objT.InfrastructureTypeCode;
        }

        public static InfrastructureType ForCode(
            string infrastructureTypeCode)
        {
            if (infrastructureTypeCode == null) { throw new ArgumentNullException("infrastructureTypeCode"); }

            InfrastructureType result = null;

            if (infrastructureTypeCode == VmwareEsxi.InfrastructureTypeCode)
            {
                result = InfrastructureType.VmwareEsxi;
            }

            else
            {
                throw new Exception("unhandled case: " + infrastructureTypeCode);
            }

            return result;
        }
    }
}
