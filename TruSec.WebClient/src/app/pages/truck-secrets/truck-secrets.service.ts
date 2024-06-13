import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TruckSecret } from '../../models/TruckSecret';


@Injectable({
  providedIn: 'root'
})
export class TruckSecretService {

  private readonly ENDPOINT_NAME = "trucksecrets";
  constructor(private http: HttpClient) { }

  getTruckSecretById(id: number): Observable<TruckSecret> {
    return this.http.get<TruckSecret>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
  }

  getAllTruckSecrets(): Observable<TruckSecret[]> {
    return this.http.get<TruckSecret[]>(`${environment.apiBase}${this.ENDPOINT_NAME}`);
  }

  getTruckSecretsByTruck(truckId: number): Observable<TruckSecret[]> {
    debugger;
    return this.http.get<TruckSecret[]>(`${environment.apiBase}${this.ENDPOINT_NAME}/GetByTruck/${truckId}`);
  }

  addTruckSecret(truckSecret: TruckSecret): Observable<TruckSecret> {
    return this.http.post<TruckSecret>(`${environment.apiBase}${this.ENDPOINT_NAME}`, truckSecret);
  }

  updateTruckSecret(id: number, truckSecret: TruckSecret): Observable<void> {
    return this.http.put<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`, truckSecret);
  }

  deleteTruckSecret(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
  }
}
