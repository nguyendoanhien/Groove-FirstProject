import { Injectable } from '@angular/core';
import { ResetPassword } from 'app/models/ResetPassword';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'environments/environment';

const resetPasswordUrl = environment.authResetPasswordUrl;
const forgotPasswordUrl = environment.authForgotPasswordUrl;


@Injectable({
    providedIn: 'root'
})
export class ResetPasswordService {
    isForgot: BehaviorSubject<boolean>;
    constructor(private _http: HttpClient) {

    }

    resetPassword(resetPassword: ResetPassword): Observable<any> {
        console.log(resetPassword);
        const httpOptions = {
            headers: new HttpHeaders({
                'Accept': 'text/html, application/xhtml+xml, */*',
                'Content-Type': 'application/json'
            }),
            responseType: 'text' as 'json'
        };
        return this._http.post<ResetPassword>(`${resetPasswordUrl}`, resetPassword, httpOptions);
    }

    forgotPassword(email: string): Observable<any> {
        console.log(email);
        return this._http.post<any>(`${forgotPasswordUrl}?email=${email}`, email);
    }

}
