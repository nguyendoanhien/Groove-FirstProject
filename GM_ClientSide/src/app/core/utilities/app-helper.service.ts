import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AppHelperService {

    constructor() { }

    ExtractUrl(s: string): Array<any> {
        var urlRegex = /(https?:\/\/[^ ]*)/g;
        var urlList = s.match(urlRegex);
        var unique: any[];
        if (urlList != null)
            unique = urlList.filter(this.onlyUnique);
        return unique;
    }
    onlyUnique(value, index, self) {
        return self.indexOf(value) === index;
    }
    detectUrl(s: string) {
        var urlRegex = /(https?:\/\/[^ ]*)/g;
        var res = s.replace(urlRegex, `<a href='$1'>$1</a>`);
        return res;
    }
}
