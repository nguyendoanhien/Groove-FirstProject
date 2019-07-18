import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
@Injectable({
    providedIn: 'root'
})
export class UserContactService {

    constructor(private _httpClient: HttpClient) { }

    getContacts(): Observable<any[]> {
        debugger;
        return this._httpClient.get<any[]>(environment.apiContactGetAllInformUrl);

    }
}
