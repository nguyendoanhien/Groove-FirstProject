import { Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from "../auth/auth.service";
import { environment } from "../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { RegisterModel } from "app/account/register/register.model";
import { BehaviorSubject, Observable } from "rxjs";
import { MailConfirmModel } from "app/account/mail-confirmer/mail-confirmer.model";
const registerUrl = environment.authRegisterUrl;
const confirmEmailUrl = environment.authConfirmEmailUrl;

const httpOptions = {
    headers: new HttpHeaders({
        'Accept': "text/html, application/xhtml+xml, */*",
        'Content-Type': "application/json"
    }),
    responseType: "text" as "json"
};

@Injectable()
export class RegisterService {
    mailToSendRegister: BehaviorSubject<any>;

    constructor(private router: Router, private authService: AuthService, private http: HttpClient) {
        this.mailToSendRegister = new BehaviorSubject(null);
    }

    register(model: RegisterModel): Observable<any> {
        return this.http.post(registerUrl, model);
    }

    confirmEmail(model: MailConfirmModel): Observable<any> {
        return this.http.post(confirmEmailUrl, model, httpOptions);
    }
}