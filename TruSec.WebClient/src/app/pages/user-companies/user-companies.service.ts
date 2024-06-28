import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserCompany } from '../../models/UserCompany';


@Injectable({
    providedIn: 'root'
})
export class UserCompanyService {

    private readonly ENDPOINT_NAME = "usercompanies";
    constructor(private http: HttpClient) { }

    getUserCompanyById(id: number): Observable<UserCompany> {
        return this.http.get<UserCompany>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
    }

    getAllUserCompanies(): Observable<UserCompany[]> {
        return this.http.get<UserCompany[]>(`${environment.apiBase}${this.ENDPOINT_NAME}`);
    }

    addUserCompany(userCompany: UserCompany): Observable<UserCompany> {
        return this.http.post<UserCompany>(`${environment.apiBase}${this.ENDPOINT_NAME}`, userCompany);
    }

    updateUserCompany(id: number, userCompany: UserCompany): Observable<void> {
        return this.http.put<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`, userCompany);
    }

    deleteUserCompany(id: number): Observable<void> {
        return this.http.delete<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
    }
}
