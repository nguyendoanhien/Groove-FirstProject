import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';

@Injectable({
    providedIn: 'root'
})
export class OpenGrapthService {

    constructor(private _httpClient: HttpClient) { }

    private getOgMetaTags(url: string): Observable<any> {
        return this._httpClient.get(environment.apiOpenGrapthUrl + `/${url}?app_id=${environment.openGrapth.appId}`);
    }
    public getOgImage(url: string): Observable<any> {
        return this.getOgMetaTags(url);
    }
}
