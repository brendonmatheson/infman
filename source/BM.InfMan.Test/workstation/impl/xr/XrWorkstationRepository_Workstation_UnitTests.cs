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

namespace cc.bren.infman.workstation.impl.xr
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XrWorkstationRepository_Workstation_UnitTests :
        BaseWorkstationRepository_Workstation_UnitTests,
        WorkstationRepository_Workstation_UnitTests
    {
        private static TestContext_XmlFileStorage TC = new TestContext_XmlFileStorage();

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
            TC.Setup();
        }

        //
        // WorkstationList
        //

        [TestMethod]
        public void WorkstationList_ValidRequest_Succeeds()
        {
            base.WorkstationList_ValidRequest_Succeeds(TC.WorkstationRepository);
        }

        //
        // WorkstationUpdate
        //

        [TestMethod]
        public void WorkstationUpdate_ValidRequest_Success()
        {
            base.WorkstationUpdate_ValidRequest_Success(TC.WorkstationRepository);
        }
    }
}
