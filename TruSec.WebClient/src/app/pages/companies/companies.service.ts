import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Company } from '../../models/Company';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {

  private readonly ENDPOINT_NAME = "companies";
  constructor(private http: HttpClient) { }

  getCompanyById(id: number): Observable<Company> {
    return this.http.get<Company>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`);
  }

  getAllCompanies(): Observable<Company[]> {
    return this.http.get<Company[]>(`${environment.apiBase}${this.ENDPOINT_NAME}`);
  }

  addCompany(company: Company): Observable<Company> {
    return this.http.post<Company>(`${environment.apiBase}${this.ENDPOINT_NAME}`, company);
  }

  updateCompany(id: number, company: Company): Observable<void> {
    return this.http.put<void>(`${environment.apiBase}${this.ENDPOINT_NAME}/${id}`, company);
  }

  deleteCompany(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiBase}/${this.ENDPOINT_NAME}/${id}`);
  }
}
