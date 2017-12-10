import { PaginationComponent } from './../pagination/pagination.component';
import { VehicleService } from './../../services/vehicle.service';
import { Vehicle } from './../../models/vehicle';
import { Component, OnInit } from '@angular/core';
import { KeyValuePair } from '../../models/keyValuePair';
import { filterQueryId } from '@angular/core/src/view/util';

@Component({
	selector: 'app-vehicle-list',
	templateUrl: './vehicle-list.component.html',
	styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit {
	queryResult: any = {};
	makes: KeyValuePair[];
	query: any = {
		pageSize: 3
	};
	columns = [
		{ title: 'Id' },
		{ title: 'Make', key: 'make', isSortable: true },
		{ title: 'Model', key: 'model', isSortable: true },
		{ title: 'Contact Name', key: 'contactName', isSortable: true },
		{ }
	];

	constructor(private vehicleService: VehicleService) { }

	ngOnInit() {
		this.vehicleService.getMakes()
			.subscribe(makes => this.makes = makes);
		this.populateVehicles();
	}

	private populateVehicles() {
		this.vehicleService.getVehicles(this.query)
			.subscribe(result => this.queryResult = result);
	}

	onFilterChange() {
		this.populateVehicles();
	}

	resetFilter() {
		this.query = {};
		this.onFilterChange();
	}

	sortBy(columnName: string) {
		if (this.query.sortBy === columnName) {
			this.query.isSortAscending = !this.query.isSortAscending;
		} else {
			this.query.sortBy = columnName;
			this.query.isSortAscending = true;
		}
		this.populateVehicles();
	}

	onPageChange(page: any) {
		this.query.page = page;
		this.populateVehicles();
	}
}