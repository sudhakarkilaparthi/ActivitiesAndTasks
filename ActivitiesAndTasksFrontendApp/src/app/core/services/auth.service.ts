import { inject, Injectable } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';
import { ApiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { LoginRequest, LoginResponse, GoogleLoginRequestOnlyToken } from '../models/auth.model';
import { StorageService } from './storage.service';
import { ApiBaseResponse } from '../models/common.model';
import { NotificationService } from './notifications';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private notify = inject(NotificationService);
  private api = inject(ApiService);
  private storage = inject(StorageService);

  login(payload: LoginRequest): Observable<ApiBaseResponse<LoginResponse>> {
    return this.api.post<ApiBaseResponse<LoginResponse>>(API_ENDPOINTS.AUTH.LOGIN, payload).pipe(
      tap((res) => {
        // console.log('Login response:', res);
        this.storage.setToken(res.data?.tokenInfo.token || '');
        this.storage.setTokenExpiryTime(res.data?.tokenInfo.expiresAt || '');
        this.storage.setUserDetails(res.data?.userInfo as any);

        //console.log('ISO time:', new Date().toISOString());
      }),
      catchError((error) => {
        this.notify.add('error', error?.error?.message || 'Login failed');
        // debugger;
        // console.error('Login error:', error);
        throw error;
      })
    );
  }

  register(payload: any): Observable<ApiBaseResponse<LoginResponse>> {
    return this.api.post<ApiBaseResponse<LoginResponse>>(API_ENDPOINTS.AUTH.REGISTER, payload).pipe(
      tap((res) => {
        this.storage.setToken(res.data?.tokenInfo.token || '');
      })
    );
  }

  loginWithGoogle(
    payload: GoogleLoginRequestOnlyToken
  ): Observable<ApiBaseResponse<LoginResponse>> {
    return this.api
      .post<ApiBaseResponse<LoginResponse>>(API_ENDPOINTS.AUTH.GOOGLE_LOGIN, payload)
      .pipe(
        tap((res) => {
          console.log('Google login response:', res);
          this.storage.setToken(res.data?.tokenInfo.token || '');
          this.storage.setTokenExpiryTime(res.data?.tokenInfo.expiresAt || '');
          this.storage.setUserDetails(res.data?.userInfo as any);
        }),
        catchError((error) => {
          console.error('Google login error:', error);
          throw error;
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
