import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TruckDataLog } from '../../models/TruckDataLog';


@Injectable({
  providedIn: 'root'
})
export class TruckDataLogService {

  private readonly ENDPOINT_NAME = "truckdatalogs";
  constructor(private http: HttpClient) { }

  getTruckDataLogById(id: number): Observable<TruckDataLog> {
    return this.http.get<TruckDataLog>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
  }

  getTruckDataLogByTruck(truckId: number): Observable<TruckDataLog[]> {
    return this.http.get<TruckDataLog[]>(`${environment.apiBase}${this.ENDPOINT_NAME}/GetByTruck/${truckId}`);
  }

  getAllTruckDataLogs(): Observable<TruckDataLog[]> {
    return this.http.get<TruckDataLog[]>(`${environment.apiBase}${this.ENDPOINT_NAME}`);
  }

  addTruckDataLog(truckDataLog: TruckDataLog): Observable<TruckDataLog> {
    return this.http.post<TruckDataLog>(`${environment.apiBase}${this.ENDPOINT_NAME}`, truckDataLog);
  }

  updateTruckDataLog(id: number, truckDataLog: TruckDataLog): Observable<void> {
    return this.http.put<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`, truckDataLog);
  }

  deleteTruckDataLog(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
  }
}
