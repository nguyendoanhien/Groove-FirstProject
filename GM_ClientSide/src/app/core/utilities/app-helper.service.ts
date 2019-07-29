import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AppHelperService {

    constructor() { }

    ExtractUrl(s: string): Array<any> {
        let stars = [];
        var urlRegex = /(https?:\/\/[^ ]*)/g;
        var urlList = s.match(urlRegex);
        stars = urlList;
        return stars;
    }
}
