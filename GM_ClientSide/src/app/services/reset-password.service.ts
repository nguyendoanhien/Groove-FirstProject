import { Injectable } from '@angular/core';
import { ResetPassword } from 'app/models/ResetPassword';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class ResetPasswordService {

    constructor(private _http: HttpClient) {

    }

    resetPassword(resetPassword: ResetPassword) {
        return this._http.post<ResetPassword>(`https://localhost:44383/indentity/clientaccount`, resetPassword);
    }

    
}
