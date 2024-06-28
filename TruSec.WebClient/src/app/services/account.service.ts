import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ResetPasswordDto } from "../models/ResetPasswordDto";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private readonly ENDPOINT_NAME = "account";
  constructor(private http: HttpClient) { }

  confirmEmail(userId: string, token: string): Observable<any> {
    const params = new HttpParams()
      .set('userId', userId)
      .set('token', token);
    return this.http.get(`${environment.apiBase}${this.ENDPOINT_NAME}/confirm-email`, { params });
  }

  resetPassword(resetPasswordDto: ResetPasswordDto): Observable<any> {
    return this.http.post(`${environment.apiBase}${this.ENDPOINT_NAME}/reset-password`, resetPasswordDto);
  }
}
