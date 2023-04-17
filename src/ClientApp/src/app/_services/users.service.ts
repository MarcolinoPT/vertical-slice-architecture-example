import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Users, User } from '@app/_models';

@Injectable()
export class UsersService {
  private baseUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }

  getAll() {
    return this.http.get<Users>(`${this.baseUrl}/users`);
  }

  delete(id: string) {
    return this.http.delete(`${this.baseUrl}/users/${id}`);
  }

  update(id: string, params: any) {
    return this.http.put<User>(`${this.baseUrl}/users/${id}`, params);
  }

  create(params: any) {
    return this.http.post<User>(`${this.baseUrl}/users`, params);
  }
}
