import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';

const apiContactURl = environment.apiContactUrl;
@Injectable({
    providedIn: 'root'
})
export class UserContactService {

    constructor(private _httpClient: HttpClient) { }

    getContacts(): Observable<any[]> {
        return this._httpClient.get<any[]>(environment.apiContactGetAllInformUrl);
    }
    getUnknownContacts(displayNameSearch?: string): Observable<any[]> {

        let queryPath = displayNameSearch === undefined ? "" : `?SearchKey=${displayNameSearch}`;

        return this._httpClient.get<any[]>(environment.apiContactGetAllUnknownInformUrl + queryPath);
    }

    changeNickNameContact(contact: any) {
        return this._httpClient.put(apiContactURl + `/${contact.id}`,contact).pipe();
    }
    SayHi(contact:any) {
        return this._httpClient.post(environment.apiConvUrl,contact).pipe(
           
        );
    }
}
