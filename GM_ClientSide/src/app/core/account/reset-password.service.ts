import { Injectable } from '@angular/core';
import { ResetPassword } from 'app/models/ResetPassword';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class ResetPasswordService {
    isForgot : BehaviorSubject<boolean>;
    constructor(private _http: HttpClient) {

    }
    
    resetPassword(resetPassword: ResetPassword): Observable<any> {
        console.log(resetPassword);
        const httpOptions = {
            headers: new HttpHeaders({
                'Accept': 'text/html, application/xhtml+xml, */*',
                'Content-Type': 'application/json'
            }),
            responseType: "text" as 'json'
        };
        return this._http.post<ResetPassword>(`https://localhost:44383/identity/clientaccount/resetpassword`, resetPassword, httpOptions);
    }

    forgotPassword(email: string): Observable<any> {
        console.log(email);
        return this._http.post<any>(`https://localhost:44383/identity/clientaccount/forgotpassword?email=${email}`, email);
    }

}
