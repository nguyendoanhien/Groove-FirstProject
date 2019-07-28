import { Injectable } from "@angular/core";

@Injectable()
export class AuthService {
    getToken(): string {
        return localStorage.getItem("token") === null ? "" : localStorage.getItem("token");
    }

    setToken(token: string): void {
        localStorage.setItem("token", token);
    }

    clearToken(): void {
        localStorage.removeItem("token");
    }

    isAuthenticated(): boolean {
        const token = this.getToken();
        if (token === null || token.length === 0) {
            return false;
        } else {
            return true;
        }
    }
}