import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { LoginRequest, LoginResponse } from '../models/auth.model';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private api: ApiService, private storage: StorageService) {}

  login(payload: LoginRequest): Observable<LoginResponse> {
    return this.api.post<LoginResponse>(API_ENDPOINTS.AUTH.LOGIN, payload).pipe(
      tap((res) => {
        this.storage.setToken(res.token);
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
    this.storage.clearToken();
  }

  isLoggedIn(): boolean {
    return this.storage.isLoggedIn();
  }
}
