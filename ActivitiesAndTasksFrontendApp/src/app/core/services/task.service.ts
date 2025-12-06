import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { TaskItem } from '../models/task.model';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  constructor(private api: ApiService) {}

  getAll(): Observable<TaskItem[]> {
    return this.api.get<TaskItem[]>(API_ENDPOINTS.TASKS.GET_ALL);
  }

  getById(id: number): Observable<TaskItem> {
    return this.api.get<TaskItem>(API_ENDPOINTS.TASKS.GET_BY_ID(id));
  }

  create(task: Partial<TaskItem>): Observable<TaskItem> {
    return this.api.post<TaskItem>(API_ENDPOINTS.TASKS.CREATE, task);
  }

  update(id: number, task: Partial<TaskItem>): Observable<TaskItem> {
    return this.api.put<TaskItem>(API_ENDPOINTS.TASKS.UPDATE(id), task);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(API_ENDPOINTS.TASKS.DELETE(id));
  }
}
