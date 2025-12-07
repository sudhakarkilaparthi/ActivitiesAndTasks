import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { User } from '../models/user.model';
import { ApiBaseResponse } from '../models/common.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private api: ApiService) {}

  getAll(): Observable<ApiBaseResponse<User[]>> {
    return this.api.get<ApiBaseResponse<User[]>>(API_ENDPOINTS.USERS.GET_ALL);
  }

  getById(id: number): Observable<User> {
    return this.api.get<User>(API_ENDPOINTS.USERS.GET_BY_ID(id));
  }
}
