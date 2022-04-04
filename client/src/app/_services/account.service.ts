import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

// We usually using service to make http requests
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:5001/api/";

  // inject http service
  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model)
  }
}
