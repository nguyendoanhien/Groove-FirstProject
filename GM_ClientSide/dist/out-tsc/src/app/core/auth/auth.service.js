import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
let AuthService = class AuthService {
    getToken() {
        return localStorage.getItem('token') === null ? '' : localStorage.getItem('token');
    }
    setToken(token) {
        localStorage.setItem('token', token);
    }
    clearToken() {
        localStorage.removeItem('token');
    }
    isAuthenticated() {
        const token = this.getToken();
        if (token === null || token.length === 0) {
            return false;
        }
        else {
            return true;
        }
    }
};
AuthService = tslib_1.__decorate([
    Injectable()
], AuthService);
export { AuthService };
//# sourceMappingURL=auth.service.js.map