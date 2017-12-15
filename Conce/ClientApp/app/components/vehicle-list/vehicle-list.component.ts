import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair } from "../../models/vehicle";
import { VehicleService } from "../../services/vehicle.service";

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
    private readonly PAGE_SIZE = 3;

    queryResult: any = {};
    makes: any[];
    models: any[];

    query: any = {
        pageSize: this.PAGE_SIZE
    };
    columns = [
        { title: '#'},
        { title: 'Nome de contato', key: 'contactName', isSortable: true},
        { title: 'Marca', key: 'make', isSortable: true },
        { title: 'Modelo', key: 'model', isSortable: true },
        { }
    ];

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        this.vehicleService.getMakes()
            .subscribe(makes => this.makes = makes);
        this.populateVehicles();
    }
    onFilterChange() {
        this.query.page = 1;
        this.populateVehicles();
    }
    onMakeChange() {
        this.query.modelId = null;
        this.populateModels();
        this.onFilterChange();
    }
    resetFilter() {
        this.query = {
            page: 1,
            pageSize: this.PAGE_SIZE
        };
        this.populateVehicles();
    }
    sortBy(columnName) {
        if (this.query.sortBy === columnName) {
            this.query.isSortAscending = !this.query.isSortAscending;
        } else {
            this.query.sortBy = columnName;
            this.query.isSortAscending = true
        }
        this.populateVehicles();
    }
    onPageChanged(page) {
        this.query.page = page;
        this.populateVehicles();
    }
    private populateVehicles() {
        this.vehicleService.getVehicles(this.query)
            .subscribe(result => this.queryResult = result);
    }
    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.query.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }
}
