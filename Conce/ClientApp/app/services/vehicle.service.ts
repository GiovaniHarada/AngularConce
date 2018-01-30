import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import { SaveVehicle } from "../models/vehicle";

@Injectable()
export class VehicleService {
    private readonly vehiclesEndpoint = '/api/vehicles';
    constructor(private http: Http) { }
    getMakes() {
        return this.http.get('/api/makes')
            .map(res => res.json());
    }
    getFeatures() {
        return this.http.get('/api/features')
            .map(res => res.json());
    }
    getVehicles(filter) {
        return this.http.get(this.vehiclesEndpoint + '?' + this.toQueryString(filter))
            .map(res => res.json());
    }

    toQueryString(obj) {
        var parts:string[] = [];
        for (var property in obj) {
            var value = obj[property];
            if (value != null && value != undefined) {
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
            }
        }
        return parts.join('&');
    }
    getVehicle(id) {
        return this.http.get(this.vehiclesEndpoint + '/' + id)
        .map(res => res.json());
    }
    update(vehicle: SaveVehicle) {
        return this.http.put(this.vehiclesEndpoint + '/' + vehicle.id, vehicle)
            .map(res => res.json());
    }
    create(vehicle: SaveVehicle) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let authToken = localStorage.getItem('auth_token');
        headers.append('Authorization', `Bearer ${authToken}`);
        let options = new RequestOptions({ headers: headers });
        console.log(options);

        return this.http.post(this.vehiclesEndpoint, vehicle, options )
            .map(res => res.json());
    }
    delete(id) {
        return this.http.delete(this.vehiclesEndpoint + '/' + id)
            .map(res => res.json());
    }

}
