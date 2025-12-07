import { Injectable } from '@angular/core';

const TOKEN_KEY = 'access_token';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  setToken(token: string): void {
    if (token != '' && token != null && token != undefined && token != 'undefined') {
      localStorage.setItem(TOKEN_KEY, token);
    }
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  clearToken(): void {
    localStorage.removeItem(TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
