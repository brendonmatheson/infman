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
    using cc.bren.infman.workstation;
    using System;

    public class UserInterfaceServiceImpl : UserInterfaceService
    {
        public void WorkstationList(
            WorkstationRepository workstationRepository)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            WorkstationListViewModel vm = new WorkstationListViewModel(
                workstationRepository,
                this);
            WorkstationListWindow w = new WorkstationListWindow();
            w.DataContext = vm;
            w.ShowDialog();
        }

        public void WorkstationAdd(WorkstationRepository workstationRepository)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            WorkstationPropertiesViewModel vm = WorkstationPropertiesViewModel.ForAdd(
                workstationRepository);
            WorkstationPropertiesWindow w = new WorkstationPropertiesWindow();
            w.DataContext = vm;
            w.ShowDialog();
        }

        public void WorkstationEdit(
            WorkstationRepository workstationRepository,
            Guid workstationId)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }
            if (workstationId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "workstationId"); }

            WorkstationPropertiesViewModel vm = WorkstationPropertiesViewModel.ForEdit(
                workstationRepository,
                workstationId);
            WorkstationPropertiesWindow w = new WorkstationPropertiesWindow();
            w.DataContext = vm;
            w.ShowDialog();
        }
    }
}
