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
    using cc.bren.infman.infrastructure;
    using cc.bren.infman.spec;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MainViewModel
    {
        private SpecRepository _specRepository;
        private InfrastructureRepository _infrastructureRepository;

        public MainViewModel(
            SpecRepository specRepository,
            InfrastructureRepository infrastructureRepository)
        {
            if (specRepository == null) { throw new ArgumentNullException("specRepository"); }
            if (infrastructureRepository == null) { throw new ArgumentNullException("infrastructureRepository"); }

            _specRepository = specRepository;
            _infrastructureRepository = infrastructureRepository;

            this.HostSpecs = new ObservableCollection<HostSpecViewModel>();
            this.Infrastructures = new ObservableCollection<InfrastructureListItemViewModel>();

            this.Refresh();
        }

        public ObservableCollection<HostSpecViewModel> HostSpecs { get; private set; }

        public ObservableCollection<InfrastructureListItemViewModel> Infrastructures { get; private set; }

        private void Refresh()
        {
            // HostSpecs
            IList<HostSpecEntity> hostSpecs = _specRepository.HostSpecList(HostSpecFilter.All());
            this.HostSpecs.Clear();
            this.HostSpecs.AddRange(hostSpecs, item => item.ToViewModel());

            // Infrsatructures
            IList<InfrastructureEntity> infrastructures = _infrastructureRepository.InfrastructureList();
            this.Infrastructures.Clear();
            this.Infrastructures.AddRange(infrastructures, item => item.ToListItemViewModel());
        }
    }
}
