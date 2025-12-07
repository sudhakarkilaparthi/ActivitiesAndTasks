import { Injectable } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';
import { ApiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { LoginRequest, LoginResponse } from '../models/auth.model';
import { StorageService } from './storage.service';
import { ApiBaseResponse } from '../models/common.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private api: ApiService, private storage: StorageService) {}

  login(payload: LoginRequest): Observable<ApiBaseResponse<LoginResponse>> {
    // debugger;
    return this.api.post<ApiBaseResponse<LoginResponse>>(API_ENDPOINTS.AUTH.LOGIN, payload).pipe(
      tap((res) => {
        console.log('Login response:', res);
        this.storage.setToken(res.data?.token || '');
      }),
      catchError((error) => {
        // debugger;
        console.error('Login error:', error);
        throw error;
      })
    );
  }

  register(payload: any): Observable<LoginResponse> {
    return this.api.post<LoginResponse>(API_ENDPOINTS.AUTH.REGISTER, payload).pipe(
      tap((res) => {
        this.storage.setToken(res.token);
      })
    );
  }

  logout(): void {
    this.storage.clearAll();
  }

  isLoggedIn(): boolean {
    return this.storage.isLoggedIn();
  }
}
