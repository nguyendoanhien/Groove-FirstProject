import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class OpenGrapthService {

    constructor(private _httpClient: HttpClient) { }

    private getOgMetaTags(url: string): Observable<any> {
        return this._httpClient.get(environment.apiOpenGrapthUrl + `/${encodeURIComponent(url)}?app_id=${environment.openGrapth.appId}`);
    }
    public getOgImage(url: string) {
        return this.getOgMetaTags(url).pipe(map(data => { return data.openGraph.image.url as string }));
    }
}
