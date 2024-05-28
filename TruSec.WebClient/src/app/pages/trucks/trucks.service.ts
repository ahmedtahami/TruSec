import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Truck } from '../../models/Truck';


@Injectable({
    providedIn: 'root'
})
export class TrucksService {

    private readonly ENDPOINT_NAME = "trucks";
    constructor(private http: HttpClient) { }

    getTruckById(id: number): Observable<Truck> {
        return this.http.get<Truck>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
    }

    getAllTrucks(): Observable<Truck[]> {
        return this.http.get<Truck[]>(`${environment.apiBase}${this.ENDPOINT_NAME}`);
    }

    addTruck(truck: Truck): Observable<Truck> {
        return this.http.post<Truck>(`${environment.apiBase}${this.ENDPOINT_NAME}`, truck);
    }

    updateTruck(id: number, truck: Truck): Observable<void> {
        return this.http.put<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`, truck);
    }

    deleteTruck(id: number): Observable<void> {
        return this.http.delete<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
    }
}
