import { Injectable } from '@angular/core';
@Injectable()
export class AuthService {
    public getToken(): string {
        return localStorage.getItem('token') === null ? '' : localStorage.getItem('token');
    }

    public setToken(token: string) {
        localStorage.setItem('token', token);
    }

    public clearToken() {
        localStorage.removeItem('token');
    }

    public isAuthenticated(): boolean {
        const token = this.getToken();
        if (token === null || token.length === 0) {
            return false;
        } else {
            return true;
        }
    }
}
