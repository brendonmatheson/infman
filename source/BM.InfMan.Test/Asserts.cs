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
namespace cc.bren.infman
{
    using cc.bren.infman.infrastructure;
    using cc.bren.infman.infrastructure.impl;
    using cc.bren.infman.spec;
    using cc.bren.infman.workstation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    public static class Asserts
    {

        //
        // HostSpec
        //

        public static void AssertHostSpecEntity(
            Guid expectedHostSpecId,
            string expectedName,
            long expectedRamBytes,
            HostSpecEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Assert.IsNotNull(actual, name);
            Assert.AreEqual(expectedHostSpecId, actual.HostSpecId, name + ".HostSpecId");

            Asserts.AssertHostSpecEntity(
                expectedName,
                expectedRamBytes,
                actual,
                name);
        }

        public static void AssertHostSpecEntity(
            string expectedName,
            long expectedRamBytes,
            HostSpecEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Assert.IsNotNull(actual, name);
            Assert.AreEqual(expectedName, actual.Name, name + ".Name");
            Assert.AreEqual(expectedRamBytes, actual.RamBytes, name + ".RamBytes");
        }

        //
        // Infrastructure
        //

        public static void AssertInfrastructureEntity(
            Guid expectedInfrastructureId,
            InfrastructureType expectedInfrastructureType,
            string expectedName,
            InfrastructureEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Assert.IsNotNull(actual, name);
            Assert.AreEqual(expectedInfrastructureId, actual.InfrastructureId, name + ".infrastructureId");

            Asserts.AssertInfrastructureEntity(
                expectedInfrastructureType,
                expectedName,
                actual,
                name);
        }

        public static void AssertInfrastructureEntity(
            InfrastructureType expectedInfrastructureType,
            string expectedName,
            InfrastructureEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Assert.IsNotNull(actual, name);
            Assert.AreEqual(expectedInfrastructureType, actual.InfrastructureType, name + ".infrastructureType");
            Assert.AreEqual(expectedName, actual.Name, name + ".name");
        }

        //
        // VmwareEsxi
        //

        public static void AssertVmwareEsxiEntity(
            Guid expectedInfrastructureId,
            string expectedName,
            string expectedIpAddress,
            InfrastructureEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Asserts.AssertInfrastructureEntity(
                expectedInfrastructureId,
                InfrastructureType.VmwareEsxi,
                expectedName,
                actual,
                name);

            Assert.IsInstanceOfType(actual, typeof(VmwareEsxiEntity), name + ".type");

            VmwareEsxiEntity actualT = (VmwareEsxiEntity)actual;

            Assert.AreEqual(expectedIpAddress, actualT.IpAddress, name + ".IpAddress");
        }

        public static void AssertVmwareEsxiEntity(
            string expectedName,
            string expectedIpAddress,
            InfrastructureEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Asserts.AssertInfrastructureEntity(
                InfrastructureType.VmwareEsxi,
                expectedName,
                actual,
                name);

            Assert.IsInstanceOfType(actual, typeof(VmwareEsxiEntity), name + ".type");

            VmwareEsxiEntity actualT = (VmwareEsxiEntity)actual;

            Assert.AreEqual(expectedIpAddress, actualT.IpAddress, name + ".IpAddress");
        }

        //
        // HostInstance
        //

        public static void AssertHostInstanceEntity(
            Guid expectedHostInstanceId,
            string expectedName,
            Guid expectedHostSpecId,
            Guid expectedInfrastructureId,
            HostInstanceEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Asserts.AssertHostInstanceEntity(
                expectedName,
                expectedHostSpecId,
                expectedInfrastructureId,
                actual,
                name);

            Assert.AreEqual(expectedHostInstanceId, actual.HostInstanceId, name + ".HostInstanceId");
        }

        public static void AssertHostInstanceEntity(
            string expectedName,
            Guid expectedHostSpecId,
            Guid expectedInfrastructureId,
            HostInstanceEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Assert.IsNotNull(actual, name);

            Assert.AreEqual(expectedName, actual.Name, name + ".Name");
            Assert.AreEqual(expectedHostSpecId, actual.HostSpecId, name + ".HostSpecId");
            Assert.AreEqual(expectedInfrastructureId, actual.InfrastructureId, name + ".InfrastructureId");
        }

        //
        // Workstation
        //

        public static void AssertWorkstation(
            Guid expectedWorkstationId,
            string expectedName,
            string expectedKeyPath,
            WorkstationEntity actual,
            string name)
        {
            Asserts.AssertWorkstation(
                expectedName,
                expectedKeyPath,
                actual,
                name);

            Assert.AreEqual(expectedWorkstationId, actual.WorkstationId, name + ".WorkstationId");
        }

        public static void AssertWorkstation(
            string expectedName,
            string expectedKeyPath,
            WorkstationEntity actual,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            Assert.IsNotNull(actual, name);
            Assert.AreEqual(expectedName, actual.Name, name + ".Name");
            Assert.AreEqual(expectedKeyPath, actual.KeyPath, name + ".KeyPath");
        }

    }
}
