import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class AppHelperService {

    constructor() {}

    ExtractUrl(s: string): Array<any> {
        const urlRegex = /(https?:\/\/[^ ]*)/g;
        const urlList = s.match(urlRegex);
        let unique: any[];
        if (urlList != null)
            unique = urlList.filter(this.onlyUnique);
        return unique;
    }

    onlyUnique(value, index, self) {
        return self.indexOf(value) === index;
    }

    detectUrl(s: string) {
        const urlRegex = /(https?:\/\/[^ ]*)/g;
        const res = s.replace(urlRegex, `<a href='$1'>$1</a>`);
        return res;
    }
}