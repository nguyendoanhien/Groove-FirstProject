import { Pipe, PipeTransform } from "@angular/core";
import { AppHelperService } from "app/core/utilities/app-helper.service";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";

@Pipe({ name: "detectUrl" })
export class DetectUrlPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {string} value
     * @param {any[]} args
     * @returns {string}
     */
    constructor(
        public _appHelperService: AppHelperService,
        private _sanitizer: DomSanitizer
    ) {

    }

    transform(value: string, args: any[] = []): SafeHtml {

        return this._sanitizer.bypassSecurityTrustHtml(this._appHelperService.detectUrl(value));
    }
}