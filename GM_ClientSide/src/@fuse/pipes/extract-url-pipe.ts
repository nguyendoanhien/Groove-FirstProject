import { Pipe, PipeTransform } from '@angular/core';
import { FuseUtils } from '@fuse/utils';
import { FacebookService } from 'ngx-facebook';
import { AppHelperService } from 'app/core/utilities/app-helper.service';

@Pipe({ name: 'extractUrl' })
export class ExtractUrlPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */
    constructor(
        public _appHelperService: AppHelperService,
        private fbk: FacebookService) {

    }

    transform(message: string): any {
        return this._appHelperService.ExtractUrl(message);
    }
}
