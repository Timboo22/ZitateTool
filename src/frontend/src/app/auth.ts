import {Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {tap} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private apiUrl = 'http://localhost:5202/auth/login';

  currentUserToken = signal<string | null>(localStorage.getItem('token'));

  constructor(private http: HttpClient) {}

  login(username: string, password: string) {
    return this.http.post<{token: string}>(this.apiUrl, { username, password }).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        this.currentUserToken.set(response.token);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserToken.set(null);
  }
}
