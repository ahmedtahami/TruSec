import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApplicationUser } from '../../models/ApplicationUser';
import { ApplicationUserRegistrationDto } from '../../models/ApplicationUserRegistrationDto';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly ENDPOINT_NAME = "user";
  constructor(private http: HttpClient) { }

  getAll(): Observable<ApplicationUser[]> {
    return this.http.get<ApplicationUser[]>(`${environment.apiBase}${this.ENDPOINT_NAME}`);
  }

  addApplicationUser(ApplicationUser: ApplicationUserRegistrationDto): Observable<ApplicationUserRegistrationDto> {
    return this.http.post<ApplicationUserRegistrationDto>(`${environment.apiBase}${this.ENDPOINT_NAME}`, ApplicationUser);
  }
}
