﻿/*
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cc.bren.infman.spec.impl.xr
{
    [TestClass]
    public class XrSpecRepository_HostSpec_UnitTests :
        BaseSpecRepository_HostSpec_UnitTests,
        SpecRepository_HostSpec_UnitTests
    {
        private static TestContext_XmlFileStorage TC = new TestContext_XmlFileStorage();

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
            TC.Setup();
        }

        //
        // HostSpecSingle
        //

        [TestMethod]
        public void HostSpecSingle_ValidRequest_Succeeds()
        {

        }

        [TestMethod]
        public void HostSpecSingle_NullFilter_Throws()
        {

        }

        //
        // HostSpecList
        //

        [TestMethod]
        public void HostSpecList_FilterAll_Succeeds()
        {
            base.HostSpecList_FilterAll_Succeeds(TC.SpecRepository);
        }

        //
        // HostSpecInsert
        //

        [TestMethod]
        public void HostSpecInsert_ValidRequest_Succeeds()
        {
            base.HostSpecInsert_ValidRequest_Succeeds(TC.SpecRepository);
        }
    }
}
